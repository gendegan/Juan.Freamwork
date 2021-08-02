using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class RequestQueryHandler : SortedDictionary<string, string>
    {

        /// <summary>
        /// 
        /// </summary>
        public RequestQueryHandler()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public new void Add(string key, string value)
        {
            Add(key, value, false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="isCheckValue"></param>
        public void Add(string key, string value, bool isCheckValue)
        {
            if (this.ContainsKey(key))
            {
                this.Remove(key);
            }
            if (isCheckValue)
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    base.Add(key, value);
                }
            }
            else
            {
                base.Add(key, value);
            }
        }
    }


}
