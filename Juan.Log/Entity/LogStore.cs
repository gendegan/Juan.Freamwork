using Juan.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juan.Log.Entity
{
    public class LogStore
    {
        public LogStore()
        {
            WriterType = new List<LogWriterType>();
        }

        public string LogType
        {
            get;
            set;
        }

        public List<LogWriterType> WriterType
        {
            get;
            set;
        }

        public static List<LogStore> DefalutStore()
        {
            List<LogStore> objLogStoreList = new List<LogStore>();
            List<EnumInfo> objEnumInfoList = EnumHelper.GetEnumMembers(typeof(LogType));
            foreach (EnumInfo objEnumInfo in objEnumInfoList)
            {
                LogStore objLogStore = new LogStore();
                objLogStore.LogType = objEnumInfo.Key;
                objLogStore.WriterType.Add(LogWriterType.DataWriter);
                objLogStoreList.Add(objLogStore);
            }
            return objLogStoreList;

        }

        public static List<LogStore> CreateStore(List<LogWriterType> objLogWriterTypeList)
        {
            List<LogStore> objLogStoreList = new List<LogStore>();
            List<EnumInfo> objEnumInfoList = EnumHelper.GetEnumMembers(typeof(LogType));
            foreach (EnumInfo objEnumInfo in objEnumInfoList)
            {
                LogStore objLogStore = new LogStore();
                objLogStore.LogType = objEnumInfo.Key;
                objLogStore.WriterType = objLogWriterTypeList;
                objLogStoreList.Add(objLogStore);
            }
            return objLogStoreList;

        }
    }
}
