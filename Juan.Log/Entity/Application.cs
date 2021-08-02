using System;
using System.Collections;
using System.Runtime.Serialization;
using Juan.Core;
using System.Collections.Generic;

namespace Juan.Log.Entity
{
    /// <summary>
    /// 日志程序表 实体层
    /// </summary>
    public partial class Application : IData
    {

        public List<LogStore> GetLogStoreInfo()
        {

            try
            {
                if (String.IsNullOrWhiteSpace(LogStoreInfo))
                {
                    return LogStore.DefalutStore();
                }
                else
                {
                    return LogStoreInfo.JsonDeserialize<List<LogStore>>();
                }
            }
            catch (Exception objExp)
            {
                EventLogHelper.WriterLog("日志节点:" + ApplicationCode + "存储转换异常", LogStoreInfo, objExp);
                return LogStore.DefalutStore();
            }


        }
        public List<NoticeCheck> GetNoticeCheckInfo()
        {

            try
            {
                if (String.IsNullOrWhiteSpace(NoticeCheckInfo))
                {
                    return new List<NoticeCheck>();
                }
                else
                {
                    return NoticeCheckInfo.JsonDeserialize<List<NoticeCheck>>();
                }
            }
            catch (Exception objExp)
            {
                EventLogHelper.WriterLog("日志节点:" + ApplicationCode + "通知检查转换异常", LogStoreInfo, objExp);
                return new List<NoticeCheck>();
            }


        }
    }
}
