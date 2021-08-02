using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JuanGenerate
{
    public sealed partial class JuanGeneratePackage
    {




        /// <summary>
        /// 更新代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateContextHandler(object sender, EventArgs e)
        {
            try
            {
                SelectFileInfo objSelectFileInfo = GetSelectFileInfo();
                if (objSelectFileInfo == null)
                {
                    return;
                }
                if (!objSelectFileInfo.IsExistGenerateConfig())
                {
                    return;
                }
                TableSchema objTableSchema = SelectItemTableSchema(objSelectFileInfo);
                if (objTableSchema == null)
                {
                    return;
                }

                string generateCode = "";
                if (objSelectFileInfo.ProjectItemPath.IndexOf("Entity") > 0)
                {
                    if (objTableSchema.PrimaryKey == null)
                    {
                        generateCode = objTableSchema.CreateGenerateViewEntity(objSelectFileInfo);
                    }
                    else
                    {
                        generateCode = objTableSchema.CreateGenerateEntity(objSelectFileInfo);
                    }
                }

                else if (objSelectFileInfo.ProjectItemPath.IndexOf("Context") > 0)
                {
                    generateCode = objTableSchema.CreateGenerateContext(objSelectFileInfo);
                }

                string ProjectItemPath = objSelectFileInfo.ProjectItemPath;
                if (objTableSchema.PrimaryKey != null)
                {
                    if (!ProjectItemPath.Contains(".Generate.cs"))
                    {
                        ProjectItemPath = ProjectItemPath.Replace(".cs", ".Generate.cs");
                    }
                }
                File.WriteAllText(ProjectItemPath, generateCode, Encoding.UTF8);
                MessageBox.Show("更新成功");

            }
            catch (Exception objExp)
            {
                MessageBox.Show("更新代码出现异常,异常错误：" + objExp.ToString());
                return;

            }
        }


    }
}
