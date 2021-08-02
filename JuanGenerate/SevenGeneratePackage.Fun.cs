using EnvDTE;
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
        public SelectFileInfo GetSelectFileInfo()
        {

            Array objProjects = (Array)ApplicationObject.ActiveSolutionProjects;
            if (objProjects.Length <= 0)
            {
                return null;
            }

            Project objProject = (Project)(objProjects).GetValue(0);

            string ProjectName = Path.GetFileNameWithoutExtension(objProject.FullName);
            string projectPath = Path.GetDirectoryName(objProject.FullName); //项目路径
            string projectItemPath = "";
            string itemFileName = "";
            ProjectItem oProjectItem = ApplicationObject.SelectedItems.Item(1).ProjectItem;
            if (oProjectItem != null)
            {
                projectItemPath = (string)oProjectItem.Properties.Item("FullPath").Value;
                itemFileName = Path.GetFileNameWithoutExtension((string)oProjectItem.Properties.Item("FullPath").Value);
            }
            string GenerateConfigPath = projectPath + @"\GenerateConfig.xml";

            return new SelectFileInfo() { GeneratePath = GenerateConfigPath, Project = objProject, ApplicationObject = ApplicationObject, ProjectItem = oProjectItem, ProjectName = ProjectName, ItemFileName = itemFileName, ProjectItemPath = projectItemPath, ProjectPath = projectPath };
        }




        public TableSchema SelectItemTableSchema(SelectFileInfo objSelectFileInfo)
        {

            if (!objSelectFileInfo.IsExistGenerateConfig())
            {
                return null;
            }
            string entityName = objSelectFileInfo.GetItemPathEntityName();
            if (string.IsNullOrWhiteSpace(entityName))
            {
                MessageBox.Show(objSelectFileInfo.ItemFileName + ".cs,不是基础业务层类,因此无法更新或新增");
                return null;
            }

            GenerateHelper objGenerateHelper = new GenerateHelper(objSelectFileInfo.GeneratePath);
            if (!objGenerateHelper.LoadConfigXml())
            {
                return null;
            }
            objSelectFileInfo.CurrentGenerateHelper = objGenerateHelper;
            ContextInfo objContextInfo = objGenerateHelper.GetContext(entityName);
            if (objContextInfo == null)
            {
                MessageBox.Show(entityName + "不是数据库实体");
                return null;
            }
            else
            {
                string database = "";
                string connectionString = objGenerateHelper.GetConnectionString(objContextInfo.ConnectionKey, out database);
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    if (MessageBox.Show("连接串键值(" + objContextInfo.ConnectionKey + ")不存在,要配置连接吗？", "", MessageBoxButtons.OKCancel) == DialogResult.OK)//如果点击“确定”按钮
                    {
                        ConnectConfigFrom cFrom = new ConnectConfigFrom(objSelectFileInfo);
                        cFrom.Show();
                    }
                    return null;
                }
                string tableName = objContextInfo.TableName;
                try
                {

                    TableSchema objTableSchema = SchemaHelper.GetTables(database, connectionString).FirstOrDefault(s => s.TableName == tableName);
                    if (objTableSchema == null)
                    {
                        MessageBox.Show("数据库找不到此:" + tableName + ",请检查配置是否正确");
                        return null;
                    }
                    objGenerateHelper.ContextNodeToTableSchema(objContextInfo, objTableSchema);

                    objTableSchema.Columns = SchemaHelper.GetTableColumnsSchema(objTableSchema, connectionString, database, objTableSchema.TableName, objGenerateHelper);

                    return objTableSchema;
                }

                catch (Exception objExp)
                {
                    MessageBox.Show("更新代码出现异常,异常错误：" + objExp.ToString());
                    return null;
                }
            }

        }
        private static string GetItemPathEntityName(string projectItemPath, string itemFileName)
        {

            string entityName = string.Empty;
            if (projectItemPath.IndexOf("Entity") > 0)
            {
                entityName = itemFileName;
            }
            else if (projectItemPath.IndexOf("Context") > 0)
            {
                entityName = itemFileName.Replace("Context", "");
            }
            return entityName;

        }

        public ContextInfo GetContextInfo(SelectFileInfo objSelectFileInfo)
        {
            if (!objSelectFileInfo.IsExistGenerateConfig())
            {
                return null;
            }
            string entityName = objSelectFileInfo.GetItemPathEntityName();
            if (string.IsNullOrWhiteSpace(entityName))
            {
                MessageBox.Show(objSelectFileInfo.ItemFileName + ".cs,不是基础业务层类,因此无法更新或新增");
                return null;
            }

            GenerateHelper objGenerateHelper = new GenerateHelper(objSelectFileInfo.GeneratePath);
            if (!objGenerateHelper.LoadConfigXml())
            {
                return null;
            }
            ContextInfo objContextInfo = objGenerateHelper.GetContext(entityName);
            if (objContextInfo == null)
            {
                MessageBox.Show(entityName + "不是数据库实体");
                return null;
            }

            return objContextInfo;
        }

    }
}
