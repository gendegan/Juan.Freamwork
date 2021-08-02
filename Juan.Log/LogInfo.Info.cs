using Juan.Log.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace Juan.Log
{
    internal partial class LogInfo
    {


        [JsonIgnore]
        public Application Application
        {
            get;
            set;
        }

        public string ApplicationCode
        {
            get;
            set;
        }

        public string ApplicationName
        {
            get
            {
                if (Application != null)
                {
                    return Application.ApplicationName;
                }
                return "";
            }

        }

        /// <summary>
        /// 写目录
        /// </summary>
        public string WritePath
        {
            get;
            set;
        }

    }
}
