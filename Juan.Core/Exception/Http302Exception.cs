using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Juan.Core
{
    [Serializable]
    public class Http302Exception : HttpException
    {
        // Fields
        private string _RedirectUrl;

        // Methods
        public Http302Exception(string redirectUrl, string message = "临时移动,页面进行跳转") : base(0x12e, message)
        {
            this._RedirectUrl = redirectUrl;
        }

        // Properties
        public string RedirectUrl
        {
            get
            {
                return this._RedirectUrl;
            }
        }
    }



}
