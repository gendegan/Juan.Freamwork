using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JuanGenerate
{
    public partial class TableSchema
    {
        public string CreateGenerateContext(SelectFileInfo objSelectFileInfo)
        {
            string templateCode = "JuanGenerate.Template.GenerateContext.txt".FindResourceText();
            return string.Format(templateCode, EntityName, PrimaryKey.SystemType, objSelectFileInfo.ProjectName, this.Description, objSelectFileInfo.CurrentGenerateHelper.FrameworkName);

        }

        public string CreateContext(SelectFileInfo objSelectFileInfo)
        {

            StringBuilder objColumnCheckBuilder = new StringBuilder();
            foreach (ColumnSchema objColumnSchema in Columns.Where(s => s.IsCheck == true))
            {

                objColumnCheckBuilder.AppendLine(string.Format("            value.{0}.InputNoNull(\"{0}\",\"{1}\");", objColumnSchema.FieldName, objColumnSchema.ErrorMessage));

            }
            string ColumnCheckValue = objColumnCheckBuilder.ToString();
            string overOperator = "";
            if (!string.IsNullOrWhiteSpace(ColumnCheckValue))
            {
                overOperator = string.Format("JuanGenerate.Template.ContextCheck.txt".FindResourceText(), EntityName, ColumnCheckValue);
            }

            return string.Format("JuanGenerate.Template.Context.txt".FindResourceText(), EntityName, PrimaryKey.SystemType, objSelectFileInfo.ProjectName, overOperator, this.Description, objSelectFileInfo.CurrentGenerateHelper.FrameworkName);


        }


    }

}
