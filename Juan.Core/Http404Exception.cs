using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Juan.Core
{
    [Serializable]
    public class Http404Exception : HttpException
    {
        // Methods
        public Http404Exception(string message = "对不起，此页面不存在") : base(0x194, message)
        {
        }
    }


}
