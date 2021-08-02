using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JuanGenerate
{
    public partial class TableSchema
    {
        public string CreateGenerateEntity(SelectFileInfo objSelectFileInfo)
        {

            StringBuilder objGenerateBuilder = new StringBuilder();

            objGenerateBuilder.AppendLine("using System;");
            objGenerateBuilder.AppendLine("using System.Collections;");
            objGenerateBuilder.AppendLine("using System.Runtime.Serialization;");
            objGenerateBuilder.AppendLine($"using {objSelectFileInfo.CurrentGenerateHelper.FrameworkName}.Core;");
            objGenerateBuilder.AppendLine("");
            objGenerateBuilder.AppendLine("namespace " + objSelectFileInfo.ProjectName + ".Entity");
            objGenerateBuilder.AppendLine("{");

            objGenerateBuilder.AppendLine("    #region " + EntityName);
            objGenerateBuilder.AppendLine("    /// <summary>");
            if (String.IsNullOrEmpty(Description))
            {
                objGenerateBuilder.AppendLine("    /// " + EntityName);
            }
            else
            {
                objGenerateBuilder.AppendLine("    /// " + Description + " 实体层");
            }
            objGenerateBuilder.AppendLine("    /// </summary>");

            string Ver = "";
            foreach (var FieldName in Columns.Select(s => s.FieldName.ToLower()).ToList().OrderBy(key => key))
            {
                Ver += FieldName + ",";
            }

            Ver = Ver.Trim(',');
            Ver = Ver.MD5Encrypt();
            objGenerateBuilder.AppendLine(string.Format("    [Table(TableName = \"{0}\",VerNo = \"{1}\", ViewName = \"{2}\", TableFormat = \"{3}\", ViewFormat = \"{4}\")]", TableName, Ver, ViewName, TableFormat, ViewFormat));
            if (string.IsNullOrWhiteSpace(ContextKey))
            {
                objGenerateBuilder.AppendLine("    [Context()]");
            }
            else
            {
                objGenerateBuilder.AppendLine("    [Context(\"" + ContextKey + "\")]");
            }
            objGenerateBuilder.AppendLine("    [Serializable]");
            objGenerateBuilder.AppendLine("    public partial class " + EntityName + " : IData");
            objGenerateBuilder.AppendLine("    {");

            //Constructors
            objGenerateBuilder.AppendLine("        #region  构造函数 ");
            objGenerateBuilder.AppendLine("        public " + EntityName + "()");
            objGenerateBuilder.AppendLine("        {");
            foreach (ColumnSchema objColumnSchema in Columns)
            {
                string nameLower = objColumnSchema.FieldName.ToLower();
                if (nameLower == "guid")
                {
                    objGenerateBuilder.AppendLine("         " + objColumnSchema.PropertyName + " = System.Guid.NewGuid().ToString();");
                }
                else if (objColumnSchema.DataType == "varchar" && objColumnSchema.Length == 36 && objColumnSchema.IsPrimaryKey)
                {
                    objGenerateBuilder.AppendLine("         " + objColumnSchema.PropertyName + " = System.Guid.NewGuid().ToString();");
                }
                else if (objColumnSchema.DataType == "char" && objColumnSchema.Length == 36)
                {
                    objGenerateBuilder.AppendLine("         " + objColumnSchema.PropertyName + " = System.Guid.NewGuid();");
                }
                else
                {
                    objGenerateBuilder.AppendLine("         " + objColumnSchema.PropertyName + "=" + objColumnSchema.DefaultValue + ";");
                }
            }
            objGenerateBuilder.AppendLine("        }");
            objGenerateBuilder.AppendLine("        #endregion ");
            objGenerateBuilder.AppendLine("");
            //Public Properties
            objGenerateBuilder.AppendLine("        #region 属性 ");
            ColumnSchema objPrimaryKeyColumn = PrimaryKey;
            foreach (ColumnSchema objColumnSchema in Columns)
            {
                string SystemType = objColumnSchema.SystemType;
                objGenerateBuilder.AppendLine("        /// <summary>");

                if (!String.IsNullOrEmpty(objColumnSchema.Description))
                {
                    objGenerateBuilder.AppendLine("        /// " + objColumnSchema.Description);
                }
                else
                {
                    objGenerateBuilder.AppendLine("        /// " + objColumnSchema.FieldName);
                }
                objGenerateBuilder.AppendLine("        /// </summary>");
                if (objPrimaryKeyColumn != null && objPrimaryKeyColumn.FieldName == objColumnSchema.FieldName)
                {
                    if (objPrimaryKeyColumn.IsIdentity)
                    {
                        objGenerateBuilder.AppendLine("        [PrimaryKey]");
                    }
                    else
                    {
                        objGenerateBuilder.AppendLine("        [PrimaryKey(false)]");
                    }
                }
                if (objColumnSchema.DbIgnore != "No")
                {
                    objGenerateBuilder.AppendLine("        [DbIgnore(Ignore = IgnoreType." + objColumnSchema.DbIgnore + ")]");
                }
                objGenerateBuilder.AppendLine("        public " + SystemType + " " + objColumnSchema.PropertyName);
                objGenerateBuilder.AppendLine("        {");
                objGenerateBuilder.AppendLine("            get;");
                objGenerateBuilder.AppendLine("            set;");
                objGenerateBuilder.AppendLine("        }");
            }

            objGenerateBuilder.AppendLine("    #endregion ");

            objGenerateBuilder.AppendLine("    }");
            objGenerateBuilder.AppendLine("    #endregion ");
            objGenerateBuilder.AppendLine();
            objGenerateBuilder.AppendLine();
            objGenerateBuilder.AppendLine();
            objGenerateBuilder.AppendLine();
            objGenerateBuilder.AppendLine("}");

            return objGenerateBuilder.ToString();


        }



        public string CreateGenerateViewEntity(SelectFileInfo objSelectFileInfo)
        {

            StringBuilder objGenerateBuilder = new StringBuilder();

            objGenerateBuilder.AppendLine("using System;");
            objGenerateBuilder.AppendLine("using System.Collections;");
            objGenerateBuilder.AppendLine("using System.Runtime.Serialization;");
            objGenerateBuilder.AppendLine($"using {objSelectFileInfo.CurrentGenerateHelper.FrameworkName}.Core;");
            objGenerateBuilder.AppendLine("");
            objGenerateBuilder.AppendLine("namespace " + objSelectFileInfo.ProjectName + ".Entity");
            objGenerateBuilder.AppendLine("{");

            objGenerateBuilder.AppendLine("    #region " + EntityName);
            objGenerateBuilder.AppendLine("    /// <summary>");
            if (String.IsNullOrEmpty(Description))
            {
                objGenerateBuilder.AppendLine("    /// " + EntityName);
            }
            else
            {
                objGenerateBuilder.AppendLine("    /// " + Description + " 实体层");
            }
            objGenerateBuilder.AppendLine("    /// </summary>");


            objGenerateBuilder.AppendLine("    [Serializable]");
            objGenerateBuilder.AppendLine("    public partial class " + EntityName + "");
            objGenerateBuilder.AppendLine("    {");

            //Constructors
            objGenerateBuilder.AppendLine("        #region  构造函数 ");
            objGenerateBuilder.AppendLine("        public " + EntityName + "()");
            objGenerateBuilder.AppendLine("        {");
            foreach (ColumnSchema objColumnSchema in Columns)
            {
                string nameLower = objColumnSchema.FieldName.ToLower();
                if (nameLower == "guid")
                {
                    objGenerateBuilder.AppendLine("         " + objColumnSchema.PropertyName + " = System.Guid.NewGuid().ToString();");
                }
                else if (objColumnSchema.DataType == "varchar" && objColumnSchema.Length == 36 && objColumnSchema.IsPrimaryKey)
                {
                    objGenerateBuilder.AppendLine("         " + objColumnSchema.PropertyName + " = System.Guid.NewGuid().ToString();");
                }
                else if (objColumnSchema.DataType == "char" && objColumnSchema.Length == 36)
                {
                    objGenerateBuilder.AppendLine("         " + objColumnSchema.PropertyName + " = System.Guid.NewGuid();");
                }
                else
                {
                    objGenerateBuilder.AppendLine("         " + objColumnSchema.PropertyName + "=" + objColumnSchema.DefaultValue + ";");
                }
            }
            objGenerateBuilder.AppendLine("        }");
            objGenerateBuilder.AppendLine("        #endregion ");
            objGenerateBuilder.AppendLine("");
            //Public Properties
            objGenerateBuilder.AppendLine("        #region 属性 ");
            ColumnSchema objPrimaryKeyColumn = PrimaryKey;
            foreach (ColumnSchema objColumnSchema in Columns)
            {
                string SystemType = objColumnSchema.SystemType;
                objGenerateBuilder.AppendLine("        /// <summary>");

                if (!String.IsNullOrEmpty(objColumnSchema.Description))
                {
                    objGenerateBuilder.AppendLine("        /// " + objColumnSchema.Description);
                }
                else
                {
                    objGenerateBuilder.AppendLine("        /// " + objColumnSchema.FieldName);
                }
                objGenerateBuilder.AppendLine("        /// </summary>");
                if (objPrimaryKeyColumn != null && objPrimaryKeyColumn.FieldName == objColumnSchema.FieldName)
                {
                    if (objPrimaryKeyColumn.IsIdentity)
                    {
                        objGenerateBuilder.AppendLine("        [PrimaryKey]");
                    }
                    else
                    {
                        objGenerateBuilder.AppendLine("        [PrimaryKey(false)]");
                    }
                }
                objGenerateBuilder.AppendLine("        public " + SystemType + " " + objColumnSchema.PropertyName);
                objGenerateBuilder.AppendLine("        {");
                objGenerateBuilder.AppendLine("            get;");
                objGenerateBuilder.AppendLine("            set;");
                objGenerateBuilder.AppendLine("        }");
            }

            objGenerateBuilder.AppendLine("    #endregion ");

            objGenerateBuilder.AppendLine("    }");
            objGenerateBuilder.AppendLine("    #endregion ");
            objGenerateBuilder.AppendLine();
            objGenerateBuilder.AppendLine();
            objGenerateBuilder.AppendLine();
            objGenerateBuilder.AppendLine();
            objGenerateBuilder.AppendLine("}");

            return objGenerateBuilder.ToString();


        }

        public string CreateEntity(SelectFileInfo objSelectFileInfo)
        {

            StringBuilder objGenerateBuilder = new StringBuilder();

            objGenerateBuilder.AppendLine("using System;");
            objGenerateBuilder.AppendLine("using System.Collections;");
            objGenerateBuilder.AppendLine("using System.Runtime.Serialization;");
            objGenerateBuilder.AppendLine($"using {objSelectFileInfo.CurrentGenerateHelper.FrameworkName}.Core;");
            objGenerateBuilder.AppendLine("");
            objGenerateBuilder.AppendLine("namespace " + objSelectFileInfo.ProjectName + ".Entity");
            objGenerateBuilder.AppendLine("{");

            objGenerateBuilder.AppendLine("    /// <summary>");
            if (String.IsNullOrEmpty(Description))
            {
                objGenerateBuilder.AppendLine("    /// " + EntityName);
            }
            else
            {
                objGenerateBuilder.AppendLine("    /// " + Description + " 实体层");
            }
            objGenerateBuilder.AppendLine("    /// </summary>");

            objGenerateBuilder.AppendLine("    public partial class " + EntityName + " : IData");
            objGenerateBuilder.AppendLine("    {");


            objGenerateBuilder.AppendLine("    }");
            objGenerateBuilder.AppendLine("}");
            return objGenerateBuilder.ToString();


        }
        public string CreateEntityValue()
        {
            StringBuilder objGenerateBuilder = new StringBuilder();
            objGenerateBuilder.AppendLine(EntityName + " obj" + EntityName + "= new " + EntityName + "();");

            foreach (ColumnSchema objColumnSchema in Columns.Where(s => s.FieldName.ToLower() != "guid"))
            {

                if (!objColumnSchema.IsIdentity)
                {
                    objGenerateBuilder.AppendLine(" obj" + EntityName + "." + objColumnSchema.PropertyName + "=;");
                }

            }

            return objGenerateBuilder.ToString();

        }

        public string CreateSolrEntity()
        {
            StringBuilder objGenerateBuilder = new StringBuilder();
            foreach (ColumnSchema objColumnSchema in Columns)
            {
                objGenerateBuilder.AppendLine(string.Format("<field column=\"{0}\" name=\"{0}\" />", objColumnSchema.FieldName));
            }
            return objGenerateBuilder.ToString();

        }

        public string CreateSolrIndex()
        {
            StringBuilder objGenerateBuilder = new StringBuilder();

            foreach (ColumnSchema objColumnSchema in Columns)
            {
                if (objColumnSchema.IsPrimaryKey)
                {
                    objGenerateBuilder.AppendLine(string.Format("<field name=\"{0}\" type=\"{1}\" indexed=\"true\" stored=\"true\" multiValued=\"false\" required=\"true\" />", objColumnSchema.FieldName, objColumnSchema.SolrSystemType));
                }
                else
                {
                    objGenerateBuilder.AppendLine(string.Format("<field name=\"{0}\" type=\"{1}\" indexed=\"true\" stored=\"true\" multiValued=\"false\" />", objColumnSchema.FieldName, objColumnSchema.SolrSystemType));

                }
            }

            return objGenerateBuilder.ToString();
        }


        public string CreateESMappingIndex()
        {
            StringBuilder objGenerateBuilder = new StringBuilder();
            objGenerateBuilder.AppendLine("{");
            objGenerateBuilder.AppendLine("    \"dynamic\":\"strict\",");
            objGenerateBuilder.Append("    \"properties\" :{");
            foreach (ColumnSchema objColumnSchema in Columns)
            {
                objGenerateBuilder.AppendLine("");
                objGenerateBuilder.AppendLine(string.Format("        \"{0}\":{{", objColumnSchema.FieldName));

                string systemType = objColumnSchema.ESSystemType;
                if (systemType.Equals("timestamp"))
                {
                    objGenerateBuilder.AppendLine("            \"type\":\"date\"");
                    objGenerateBuilder.AppendLine("            ,\"format\":\"epoch_millis\"");
                }
                else
                {
                    objGenerateBuilder.AppendLine(string.Format("            \"type\":\"{0}\"", systemType));
                    if (systemType.Equals("date"))
                    {
                        objGenerateBuilder.AppendLine("            ,\"format\":\"yyyy-MM-dd HH:mm:ss\"");
                    }
                    if (systemType.Equals("string"))
                    {
                        objGenerateBuilder.AppendLine("            ,\"index\":\"not_analyzed\"");
                    }
                }
                objGenerateBuilder.Append("        },");
            }

            return objGenerateBuilder.ToString().TrimEnd(',') + "\r\n       }\r\n}";
        }
    }

}
