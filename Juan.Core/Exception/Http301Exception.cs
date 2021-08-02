using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Juan.Core
{
    [Serializable]
    public class Http301Exception : HttpException
    {
        // Fields
        private string _RedirectUrl;

        // Methods
        public Http301Exception(string redirectUrl) : base(0x12d, "页面进行跳转")
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
