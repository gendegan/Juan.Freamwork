using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuanGenerate
{
    public class ColumnViewSchema : ColumnSchema
    {
        public ColumnViewSchema(TableSchema tableSchema)
            : base(tableSchema)
        {

        }

        public bool IsShow
        {
            get;
            set;
        }

        public string ToTableTrUI()
        {
            return string.Format(@"
            <tr>
                <td>
                    {0}
                </td>
                <td>
                    {1}
                </td>
            </tr>", IsEmpty ? "" : FieldTitle + ":", GetControlUI());

        }

        public string GetControlUI()
        {
            string Control = "";

            switch (this.DataType)
            {
                case "datetime":
                    Control = string.Format("<%= {0}.{1}.ToString(\"yyyy-MM-dd HH:mm:ss\") %>", "obj" + this.TableSchema.EntityName, this.PropertyName);
                    break;
                default:
                    Control = string.Format("<%= {0}.{1} %>", "obj" + this.TableSchema.EntityName, this.PropertyName);
                    break;
            }
            return Control;
        }

        public string ToRequestCode()
        {
            return string.Format(@"
            public {1} {0}
            {{
                get
                {{
                    return {2}(""{0}"");

                }}

            }}", this.PropertyName, SystemType, GetRequestType());
        }

        public string GetRequestType()
        {
            string RequestType = "";
            switch (DataType)
            {
                case "int":
                    RequestType = "GetInt";
                    break;
                case "bigint":
                    RequestType = "GetLong";
                    break;
                case "uniqueidentifier":
                    RequestType = "GetGuid";
                    break;
                case "char":
                    if (Length == 36)
                    {
                        RequestType = "GetGuid";
                    }
                    else
                    {
                        RequestType = "GetString";
                    }
                    break;
                case "varchar":
                    RequestType = "GetString";
                    break;
                case "nvarchar":
                    RequestType = "GetString";
                    break;
                case "text":
                    RequestType = "GetString";
                    break;
                default:
                    RequestType = "GetString";
                    break;
            }
            return RequestType;
        }
    }
}
