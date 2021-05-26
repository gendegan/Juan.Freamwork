using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Core
{
    public class InputNullException : ArgumentNullException
    {
        // Methods
        public InputNullException(string paramName) : base(paramName)
        {
        }

        public InputNullException(string paramName, string message = "") : base(paramName, message)
        {
        }
    }


}
