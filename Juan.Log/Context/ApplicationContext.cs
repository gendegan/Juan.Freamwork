using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Juan.Core;
using Juan.Data;
using Juan.Log.Entity;
namespace Juan.Log.Context
{
    /// <summary>
    /// 日志程序表上下文
    /// </summary>
    public partial class ApplicationContext : DataContext<Application, Int32>
    {




        public Application GetCacheApplication(string applicationCode)
        {




            Application objApplication = CacheHelper.Get<Application>("Log", applicationCode);

            if (objApplication != null)
            {
                return objApplication;
            }

            objApplication = Get(ReadOptions.Search("ApplicationCode=?ApplicationCode", "ApplicationCode".CreateParameter(applicationCode)));

            if (objApplication != null)
            {

                CacheHelper.Insert("Log", applicationCode, objApplication, DateTime.Now.AddMinutes(5));
            }
            return objApplication;


        }

        public void InitLogType()
        {
            List<LogStore> objInitLogStore = LogStore.DefalutStore();
            List<Application> objApplicationList = GetList(ReadOptions.Search());
            foreach (Application objApplication in objApplicationList)
            {
                List<LogStore> objLogStoreList = objApplication.GetLogStoreInfo();
                foreach (LogStore objLogStore in objInitLogStore)
                {
                    if (!objLogStoreList.Any(s => s.LogType == objLogStore.LogType))
                    {
                        objLogStoreList.Add(objLogStore);
                    }
                }

                objApplication.LogStoreInfo = objLogStoreList.JsonSerialize();

            }
            Update(objApplicationList, "LogStoreInfo");
        }
        public void ApplicationMove(int ApplicationID, int targetApplicationID)
        {

            Application objApplication = Get(ApplicationID);
            string IDPath = objApplication.IDPath;
            List<Application> objApplicationList = GetList(ReadOptions.Search("IDPath like '" + IDPath + "%'"));
            Application objtargetApplication = Get(targetApplicationID);
            string updateIDPath = objtargetApplication == null ? "," + IDPath.ToString() + "," : objtargetApplication.IDPath + ApplicationID.ToString() + ",";
            foreach (Application objItemApplication in objApplicationList)
            {
                objItemApplication.IDPath = objItemApplication.IDPath.Replace(IDPath, updateIDPath);
                if (objItemApplication.ApplicationID == ApplicationID)
                {
                    objItemApplication.ParentID = targetApplicationID;
                }
            }
            Update(objApplicationList, "IDPath", "ParentID");
        }

        public override int Update(Application value, params string[] fields)
        {


            if (Any(ReadOptions.Search("ApplicationID<>?ApplicationID and ApplicationCode=?ApplicationCode", "ApplicationID,ApplicationCode".CreateParameter

(value.ApplicationID, value.ApplicationCode))))
            {

                AssertHelper.InfoHintAssert("输入的程序代码已经存在");
            }
            return base.Update(value, fields);
        }


        public override int Add(Application value)
        {
            if (Any(ReadOptions.Search(" ApplicationCode=?ApplicationCode", "ApplicationCode".CreateParameter(value.ApplicationCode))))
            {
                AssertHelper.InfoHintAssert("输入的程序代码已经存在");
            }
            value.LogStoreInfo = LogStore.DefalutStore().JsonSerialize();

            string IDPath = GetField<string>("IDPath", value.ParentID);

            base.Add(value);
            value.SortIndex = value.ApplicationID;
            value.IDPath = IDPath + value.ApplicationID + ",";
            Update(value, "SortIndex", "IDPath");

            return value.ApplicationID;
        }

        public override int Delete(int id)
        {
            Application objApplication = Get(id);
            LogDataContext objLogDataContext = new LogDataContext();
            objLogDataContext.Delete(ReadOptions.Search("IDPath like '" + objApplication.IDPath + "%'"));
            Delete(ReadOptions.Search("IDPath like '" + objApplication.IDPath + "%'"));
            return objApplication.ParentID;

        }



    }
}
