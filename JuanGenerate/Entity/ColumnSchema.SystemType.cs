using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JuanGenerate
{
    public partial class ColumnSchema
    {
        public string SystemType
        {
            get
            {
                string result = DataType;



                switch (DataType)
                {
                    case "bigint":
                        return IsUnsigned ? "UInt64" : "Int64";
                    case "binary":
                        return "Object";
                    case "bit":
                        return "Boolean";
                    case "char":
                        if (Length == 36)
                        {
                            return "Guid";
                        }
                        else
                        {
                            return "String";
                        }
                    case "datetime":
                        return "DateTime";
                    case "decimal":
                        return "Decimal";
                    case "float":
                        return "Single";
                    case "double":
                        return "Double";
                    case "image":
                        return "Object";
                    case "int":
                        return IsUnsigned ? "UInt32" : "Int32";
                    case "money":
                        return "Decimal";
                    case "nchar":
                    case "varchar":
                    case "ntext":
                    case "text":
                    case "longtext":
                    case "mediumtext":
                    case "varchar2":
                    case "nvarchar":
                        return "String";
                    case "tinyblob":
                    case "blob":
                    case "longblob":
                    case "mediumblob":
                        return "Byte[]";
                    case "real":
                        return "Single";
                    case "smalldatetime":
                        return "DateTime";
                    case "smallint":
                        return IsUnsigned ? "UInt16" : "Int16";
                    case "smallmoney":
                        return "Decimal";

                    case "timestamp":
                        return "DateTime";
                    case "tinyint":
                        return "Byte";
                    case "udt"://自定义的数据类型
                        return "Object";
                    case "uniqueidentifier":
                        return "Object";
                    case "varbinary":
                        return "Object";
                    case "variant":
                        return "Object";
                    case "xml":
                        return "Object";
                    case "DATE":
                        return "DateTime";
                    case "number":
                        return "int";
                    case "INTEGER":
                        return "int";
                    case "CLOB":
                        return "String";
                    case "enum":
                        return "String";
                    default:
                        return result;

                }

            }
        }

        public string SolrSystemType
        {
            get
            {
                string result = DataType;

                switch (DataType)
                {
                    case "bigint":
                        return "long";
                    case "binary":
                        return "int";
                    case "bit":
                        return "int";
                    case "datetime":
                        return "date";
                    case "decimal":
                        return "float";
                    case "float":
                        return "float";
                    case "double":
                        return "float";
                    case "image":
                        return "string";
                    case "int":
                        return "int";
                    case "money":
                        return "float";
                    case "char":
                    case "text":
                    case "nchar":
                    case "ntext":
                    case "longtext":
                    case "varchar":
                    case "nvarchar":
                        return "string";
                    case "real":
                        return "float";
                    case "smalldatetime":
                        return "date";
                    case "smallint":
                        return "int";
                    case "smallmoney":
                        return "float";
                    case "timestamp":
                        return "int";
                    case "tinyint":
                        return "int";
                    case "udt"://自定义的数据类型
                        return "Object";
                    case "uniqueidentifier":
                        return "string";
                    case "varbinary":
                        return "string";
                    case "variant":
                        return "string";
                    case "xml":
                        return "string";
                    case "varchar2":
                        return "string";
                    case "DATE":
                        return "date";
                    case "number":
                        return "int";
                    case "INTEGER":
                        return "int";
                    case "CLOB":
                        return "string";
                    default:

                        return "string";

                }

            }
        }

        public string ESSystemType
        {
            get
            {
                string result = DataType;

                switch (DataType)
                {
                    case "bigint":
                        return "long";
                    case "binary":
                        return "integer";
                    case "bit":
                        return "boolean";
                    case "char":
                        return "string";
                    case "datetime":
                        return "date";
                    case "decimal":
                        return "float";
                    case "float":
                        return "float";
                    case "double":
                        return "double";
                    case "image":
                        return "string";
                    case "int":
                        return "integer";
                    case "money":
                        return "float";
                    case "nchar":
                        return "string";
                    case "ntext":
                        return "string";
                    case "longtext":
                        return "string";
                    case "nvarchar":
                        return "string";
                    case "real":
                        return "float";
                    case "smalldatetime":
                        return "date";
                    case "smallint":
                        return "short";
                    case "smallmoney":
                        return "float";
                    case "text":
                        return "string";
                    case "timestamp":
                        return "timestamp";
                    case "tinyint":
                        return "integer";
                    case "udt"://自定义的数据类型
                        return "object";
                    case "uniqueidentifier":
                        return "string";
                    case "varbinary":
                        return "string";
                    case "varchar":
                        return "string";
                    case "variant":
                        return "string";
                    case "xml":
                        return "string";
                    case "varchar2":
                        return "string";
                    case "DATE":
                        return "date";
                    case "number":
                        return "integer";
                    case "INTEGER":
                        return "integer";
                    case "CLOB":
                        return "string";
                    default:
                        return "string";
                }
            }
        }


    }

}
