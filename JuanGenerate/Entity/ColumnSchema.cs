using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JuanGenerate
{
    public partial class ColumnSchema
    {


        public ColumnSchema(TableSchema tableSchema)
        {
            TableSchema = tableSchema;
        }
        public TableSchema TableSchema
        {
            get;
            set;
        }

        public string ColumnType
        {
            get;
            set;
        }
        public bool IsPrimaryKey
        {
            get
            {
                return ColumnType == "PrimaryKey";

            }
        }
        public string DataType
        {
            get;
            set;
        }
        public bool IsIdentity
        {
            get;
            set;
        }
        public bool IsUnsigned
        {
            get;
            set;
        }
        public bool IsXmlField
        {
            get;
            set;
        }


        public string Description
        {
            get;
            set;
        }

        public string ErrorMessage
        {
            get;
            set;
        }
        public string OldErrorMessage
        {
            get;
            set;
        }
        public string OldDbIgnore
        {
            get;
            set;
        }


        public string OldFieldTitle
        {
            get;
            set;
        }

        public long Length
        {
            get;
            set;
        }
        public bool IsEmpty
        {
            get;
            set;
        }
        public string DbIgnore
        {
            get;
            set;
        }




        public string FieldName
        {
            get;
            set;
        }
        public string PropertyName
        {
            get
            {
                return FieldName.PropertyName();
            }
        }
        public string MemberName
        {
            get
            {
                return FieldName.MemberName();
            }

        }
        public string FieldTitle
        {
            get;
            set;
        }

        public bool IsCheck
        {
            get;
            set;
        }



    }

}
