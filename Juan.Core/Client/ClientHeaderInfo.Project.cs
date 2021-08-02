using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juan.Core
{
    public partial class ClientHeaderInfo
    {




        /// <summary>
        /// 项目标识
        /// </summary>
        public int ProjectID
        {
            get;
            set;
        }
        /// <summary>
        /// 产品标识
        /// </summary>
        public int ProductID
        {
            get;
            set;
        }

        /// <summary>
        /// 渠道标识
        /// </summary>
        public int CHID
        {
            get;
            set;
        }
        /// <summary>
        /// 渠道代码
        /// </summary>
        public string CHCode
        {
            get;
            set;
        }
        /// <summary>
        /// 版本标识
        /// </summary>
        public int VerID
        {
            get;
            set;
        }
        /// <summary>
        /// 版本代码
        /// </summary>
        public string VerCode
        {
            get;
            set;
        }
        /// <summary>
        /// SdkVer
        /// </summary>
        public string SdkVer
        {
            get;
            set;
        }
        /// <summary>
        /// SdkVerID
        /// </summary>
        public int  SdkVerID
        {
            get;
            set;
        }

    }
}
