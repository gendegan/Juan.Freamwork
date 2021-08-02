using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juan.Core
{
    /// <summary>
    /// 头部信息
    /// </summary>
    public partial class ClientHeaderInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public ClientHeaderInfo()
        {

            OSType = 0;
            OSVer = "";
            DeviceType = 0;
            DeviceModel = "";
            DeviceLang = "";
            AppLang = "";
            Net = 0;
            Mac = "";
            Screen = "";
            BSSID = "";
            Serial = "";
            OpenID = "";
            IMEI = "";
            AndroidId = "";
            JbFlag = 0;
            IDFA = "";
            IDFV = "";
            RTime = "";
            TokenType = 0;
            Token = "";
            SimIDFA = "";
            ProductID = 0;
            CHID = 0;
            IP = "";
            VerCode = "";
            CHCode = "";
            SdkVer = "";
            LoginSignature = "";
            AccountIDSignature = "";
            SessionID = "";
            ClientID = "";
            AnalysisDeviceID = "";
            LoginCode = "";
            SyncSignature = "";
            VipSignature = "";
            VipRightsID = "";
            IsDebug = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        public int IsDebug
        {
            get;
            set;
        }
        /// <summary>
        /// 系统类型1ios 2安卓3wp4window5linunx
        /// </summary>
        public int OSType
        {
            get;
            set;
        }

        /// <summary>
        /// 系统版本号
        /// </summary>
        public string OSVer
        {
            get;
            set;
        }
        /// <summary>
        /// 设备类型0 PC ,1Phone, 2Pad,3 TV,4手表,5眼镜
        /// </summary>
        public int DeviceType
        {
            get;
            set;
        }
        /// <summary>
        /// 0站点1iPhone2iPad11Android21微信小程序22快应用23百度小程序
        /// </summary>
        public int PlatForm
        {
            get;
            set;
        }
        /// <summary>
        /// 设备类型
        /// </summary>
        public string DeviceModel
        {
            get;
            set;
        }
        /// <summary>
        /// DeviceLang
        /// </summary>
        public string DeviceLang
        {
            get;
            set;
        }
        /// <summary>
        /// 程序语言
        /// </summary>
        public string AppLang
        {
            get;
            set;
        }
        /// <summary>
        /// 网络类型0未知1:WiFi，2.2G，3.3G，4.4G，5.5g,6其它
        /// </summary>
        public int Net
        {
            get;
            set;
        }


        /// <summary>
        ///Mac地址
        /// </summary>
        public string Mac
        {
            get;
            set;
        }
        /// <summary>
        /// 屏幕分辨率
        /// </summary>
        public string Screen
        {
            get;
            set;
        }
        /// <summary>
        /// 路由地址
        /// </summary>
        public string BSSID
        {
            get;
            set;
        }
        /// <summary>
        /// 设备序列号
        /// </summary>
        public string Serial
        {
            get;
            set;
        }
        /// <summary>
        /// 程序唯一标识
        /// </summary>
        public string OpenID
        {
            get;
            set;
        }
        /// <summary>
        /// 设备唯一标识
        /// </summary>
        public string IMEI
        {
            get;
            set;
        }
        /// <summary>
        /// AndroidId
        /// </summary>
        public string AndroidId
        {
            get;
            set;
        }
        /// <summary>
        /// 是否越狱
        /// </summary>
        public int JbFlag
        {
            get;
            set;

        }
        /// <summary>
        /// IP地址
        /// </summary>
        public string IP
        {
            get;
            set;
        }
        /// <summary>
        /// 客户端标识
        /// </summary>
        public string ClientID
        {
            get;
            set;
        }
        /// <summary>
        /// 经分系统标识
        /// </summary>
        public string AnalysisDeviceID
        {
            get;
            set;
        }


        /// <summary>
        /// 创建设备标识
        /// </summary>
        /// <returns></returns>
        public long CreateDeviceID()
        {
            if (this.OSType == 1)
            {
                if (this.IDFA.IsNoNullOrWhiteSpace() && this.IDFA != "00000000-0000-0000-0000-000000000000")
                {
                    return this.IDFA.MD5EncryptInt64Abs();
                }

                else if (this.OpenID.IsNoNullOrWhiteSpace())
                {
                    return this.OpenID.MD5EncryptInt64Abs();

                }
                else if (this.SimIDFA.IsNoNullOrWhiteSpace())
                {
                    return this.SimIDFA.MD5EncryptInt64Abs();
                }
                else if (this.IDFV.IsNoNullOrWhiteSpace())
                {
                    return this.IDFV.MD5EncryptInt64Abs();

                }
                else if (this.Token.IsNoNullOrWhiteSpace())
                {
                    return this.Token.MD5EncryptInt64Abs();

                }
                else if (this.AccountID > 0)
                {
                    return this.AccountID;
                }
                else
                {
                    return 0;
                }


            }
            else
            {

                if (this.IMEI.IsNoNullOrWhiteSpace() && this.IMEI != "000000000000000" && this.IMEI != "00000000")
                {
                    return this.IMEI.MD5EncryptInt64Abs();
                }
                else if (this.OpenID.IsNoNullOrWhiteSpace())
                {
                    return this.OpenID.MD5EncryptInt64Abs();
                }
                else if (this.Token.IsNoNullOrWhiteSpace())
                {
                    return this.Token.MD5EncryptInt64Abs();
                }
                else if (this.Mac.IsNoNullOrWhiteSpace())
                {
                    return this.Mac.MD5EncryptInt64Abs();
                }
                else if (this.AccountID > 0)
                {
                    return this.AccountID;
                }
                else
                {
                    return 0;
                }

            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string CreateDeviceValue()
        {
            if (this.OSType == 1)
            {
                if (this.IDFA.IsNoNullOrWhiteSpace() && this.IDFA != "00000000-0000-0000-0000-000000000000")
                {
                    return this.IDFA;
                }

                else if (this.OpenID.IsNoNullOrWhiteSpace())
                {
                    return this.OpenID;

                }
                else if (this.SimIDFA.IsNoNullOrWhiteSpace())
                {
                    return this.SimIDFA;
                }
                else if (this.IDFV.IsNoNullOrWhiteSpace())
                {
                    return this.IDFV;

                }
                else if (this.Token.IsNoNullOrWhiteSpace())
                {
                    return this.Token;

                }
                else if (this.AccountID > 0)
                {
                    return this.AccountID.ToString();
                }
                else
                {
                    return "";
                }


            }
            else
            {

                if (this.IMEI.IsNoNullOrWhiteSpace() && this.IMEI != "000000000000000" && this.IMEI != "00000000")
                {
                    return this.IMEI;
                }
                else if (this.OpenID.IsNoNullOrWhiteSpace())
                {
                    return this.OpenID;
                }
                else if (this.Token.IsNoNullOrWhiteSpace())
                {
                    return this.Token;
                }
                else if (this.Mac.IsNoNullOrWhiteSpace())
                {
                    return this.Mac;
                }
                else if (this.AccountID > 0)
                {
                    return this.AccountID.ToString();
                }
                else
                {
                    return "";
                }

            }
        }


        ///// <summary>
        ///// 设备
        ///// </summary>
        //public void SetThreadLocalValue()
        //{
        //    ThreadLocalHelper.SetValue("Client_ProductID", this.ProductID);
        //    ThreadLocalHelper.SetValue("Client_PlatForm", this.PlatForm);
        //    ThreadLocalHelper.SetValue("Client_ProjectID", this.ProjectID);
        //    ThreadLocalHelper.SetValue("Client_OSType", this.OSType);
        //    ThreadLocalHelper.SetValue("Client_CHCode", this.CHCode);
        //    SetInitLogValue();
        //}
        /// <summary>
        /// 设置日志初始化
        /// </summary>
        public void SetInitLogValue()
        {
            LogHelper.SetInitValue(this.AccountID.ToString(), this.CreateDeviceID().ToString(), this.CreateDeviceValue());

        }

    }
}
