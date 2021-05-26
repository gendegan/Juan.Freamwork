using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Juan.Core
{
    [Serializable]
    public class Http500Exception : HttpException
    {
        // Methods
        public Http500Exception(string message = "") : base(500, message)
        {
        }
    }





}
