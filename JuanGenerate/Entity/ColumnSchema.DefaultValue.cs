using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JuanGenerate
{
    public partial class ColumnSchema
    {
        public string DefaultValue
        {
            get
            {
                string result = "";
                switch (DataType)
                {
                    case "bigint":
                    case "binary":
                    case "decimal":
                    case "float":
                    case "int":
                    case "double":
                    case "money":
                        return "0";
                    case "bit":
                        return "false";
                    case "image":
                        return "object";
                    case "datetime":
                        return "System.DateTime.Now";
                    case "char":
                    case "varchar":
                    case "text":
                    case "nchar":
                    case "ntext":
                    case "nvarchar":
                    case "mediumtext":
                    case "longtext":
                        return "String.Empty";
                    case "tinyblob":
                    case "blob":
                    case "longblob":
                    case "mediumblob":
                        return "new Byte[0]";
                    case "real":
                        return "0";
                    case "smalldatetime":
                        return "System.DateTime.Now";
                    case "smallint":
                        return "0";
                    case "smallmoney":
                        return "0";
                    case "timestamp":
                        return "System.DateTime.Now";
                    case "tinyint":
                        return "0";
                    case "udt"://自定义的数据类型
                        return "object";
                    case "uniqueidentifier":
                        return "object";
                    case "varbinary":
                        return "object";
                    case "variant":
                        return "object";
                    case "xml":
                        return "object";

                    case "DATE":
                        return "DateTime";
                    case "number":
                        return "int";
                    case "enum":
                        return "\"0\"";
                    default:
                        return null;

                }
                return result;
            }
        }


    }

}
