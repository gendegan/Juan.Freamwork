using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Core
{
    public sealed class EnumAttribute : Attribute
    {
        // Fields
        private string _Description;
        private int _Value;

        // Methods
        public EnumAttribute(string description)
        {
            this._Value = -2147483648;
            this._Description = description;
        }

        public EnumAttribute(int value, string description)
        {
            this._Value = -2147483648;
            this._Description = description;
            this._Value = value;
        }

        // Properties
        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                this._Description = value;
            }
        }

        public int Value
        {
            get
            {
                return this._Value;
            }
            set
            {
                this._Value = value;
            }
        }
    }


}
