using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Core
{
    public class EnumInfo
    {
        // Fields
        private string _description;
        private string _key;
        private int _value;

        // Properties
        public string Description
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
            }
        }

        public string Key
        {
            get
            {
                return this._key;
            }
            set
            {
                this._key = value;
            }
        }

        public int Value
        {
            get
            {
                return this._value;
            }
            set
            {
                this._value = value;
            }
        }
    }


}
