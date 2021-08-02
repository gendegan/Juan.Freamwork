using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using EnvDTE80;
using EnvDTE;
using System.Configuration;

namespace JuanGenerate
{
    public class SelectFileInfo
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string ProjectName
        {
            get;
            set;
        }
        /// <summary>
        /// 工程路经
        /// </summary>
        public string ProjectPath
        {
            get;
            set;
        }

        /// <summary>
        /// 工程项路经
        /// </summary>
        public string ProjectItemPath
        {
            get;
            set;
        }
        /// <summary>
        /// 项文件名
        /// </summary>
        public string ItemFileName
        {
            get;
            set;
        }
        /// <summary>
        /// 代码配置文件路经
        /// </summary>
        public string GeneratePath
        {
            get;
            set;
        }

       

        public GenerateHelper CurrentGenerateHelper
        {
            get;
            set;
        }
        public string CodeConfigPath
        {
            get
            {
                return ProjectPath + @"\CodeConfig.xml";
            }

        }

        public IEnumerable<ProjectItem> GetItems()
        {

            foreach (ProjectItem item in this.Project.ProjectItems)
                yield return item;

        }

        /// <summary>
        ///  插件Juan配置路经
        /// </summary>
        public string JuanGenerate
        {
            get
            {
                return Path.Combine(@"C:\JuanGenerate\", "JuanGenerate.config");
            }

        }

        public DTE2 ApplicationObject
        {
            get;
            set;
        }

        public Project Project
        {
            get;
            set;
        }

        public ProjectItem ProjectItem
        {
            get;
            set;
        }
        /// <summary>
        /// 检查是否存在配置
        /// </summary>
        /// <returns></returns>
        public bool IsExistGenerateConfig()
        {

            if (!File.Exists(GeneratePath))
            {
                DialogResult dr = MessageBox.Show("GenerateConfig.xml不存在，请先配置连接，是否马上配置?", "", MessageBoxButtons.OKCancel);

                if (dr == DialogResult.OK)//如果点击“确定”按钮
                {
                    ConnectConfigFrom objConnectConfigFrom = new ConnectConfigFrom(this);
                    objConnectConfigFrom.Show();
                }
                return false;
            }
            return true;
        }

        /// <summary>
        /// 检查配置文件是否存在
        /// </summary>
        /// <returns></returns>
        public bool IsExistShareGenerate(string GeneratePath)
        {
            if (string.IsNullOrWhiteSpace(GeneratePath))
            {
                GeneratePath = JuanGenerate;
            }
            if (!File.Exists(GeneratePath))
            {
                MessageBox.Show("插件根目录JuanGenerate.config不存在,配置此文件");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取项路经实体名
        /// </summary>
        /// <returns></returns>
        public string GetItemPathEntityName()
        {
            string entityName = string.Empty;
            if (ProjectItemPath.IndexOf("Entity") > 0)
            {
                entityName = ItemFileName;
            }
            else if (ProjectItemPath.IndexOf("Context") > 0)
            {
                entityName = ItemFileName.Replace("Context", "");
            }
            return entityName;


        }

        public string GetPowerConnectionString(string GeneratePath)
        {

            if (string.IsNullOrWhiteSpace(GeneratePath))
            {
                GeneratePath = JuanGenerate;
            }
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
            fileMap.ExeConfigFilename = GeneratePath;
            System.Configuration.Configuration objConfiguration = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            return objConfiguration.ConnectionStrings.ConnectionStrings["JuanConnectionString"].ToString();

        }

        public SelectFileInfo GetProjectFileInfo(string projectName)
        {
            Projects objProjects =  ApplicationObject.Solution.Projects;
            if (objProjects.Count <= 0)
            {
                return null;
            }
            Project objProject = null;
            foreach (var item in objProjects)
            {
                objProject = (Project)(item);
                if (objProject.Name == projectName)
                {
                    break;
                }
                else
                {
                    objProject = null;
                }
            }
            if (objProject == null)
            {
                return null;
            }
            string projectPath = Path.GetDirectoryName(objProject.FullName); //项目路径
            return new SelectFileInfo() { Project = objProject, ProjectName = ProjectName,   ProjectPath = projectPath };
        }




    }
}
