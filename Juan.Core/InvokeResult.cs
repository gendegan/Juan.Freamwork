using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Core
{
    [Serializable]
    public class InvokeResult
    {

        public InvokeResult()
        {
            this.ResultCode = "0";
            this.ResultMessage = "调用成功";
        }

        // Properties
        public object Data { get; set; }

        public string ResultCode { get; set; }

        public string ResultMessage { get; set; }
    }


}
