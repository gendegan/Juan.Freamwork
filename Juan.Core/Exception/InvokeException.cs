using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Core
{
    public class InvokeException : Exception
    {

        // Methods
        public InvokeException(InvokeResult objInvokeResult) : base(string.Format("结果代码:{0},结果信息:{1}", objInvokeResult.ResultCode, objInvokeResult.ResultMessage))
        {
            this.InvokeResult = objInvokeResult;
        }

        // Properties
        public InvokeResult InvokeResult { get; set; }
    }


}
