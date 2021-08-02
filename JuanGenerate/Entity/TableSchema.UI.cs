using EnvDTE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JuanGenerate
{
    public partial class TableSchema
    {
        public string ModuleID
        {
            get;
            set;
        }

        public string ViewUrl
        {
            get;
            set;
        }
        public string EditUrl
        {
            get;
            set;
        }
        public string ListUrl
        {
            get;
            set;
        }
        public string UIProjectName
        {
            get;
            set;
        }
        public string UIProjectPath
        {
            get;
            set;
        }

        public string ControllerName
        {
            get;
            set;
        }

        public string AreasName
        {
            get;
            set;
        }

        public string ReadDataPath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(AreasName))
                {
                    return ControllerName;
                }
                return "Areas/" + AreasName + "/" + ControllerName;

            }


        }

        public string ControllerNamespace
        {
            get
            {
                if (string.IsNullOrWhiteSpace(AreasName))
                {
                    return this.UIProjectName + ".Controllers";
                }
                return this.UIProjectName + ".Areas." + this.AreasName + ".Controllers";
            }
        }
        public string ViewsPath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(AreasName))
                {
                    return Path.Combine("Views", this.ControllerName);
                }
                return Path.Combine("Areas", AreasName, "Views", this.ControllerName);

            }

        }
        public string BackViewsPath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(AreasName))
                {
                    return this.ControllerName;
                }
                return AreasName + "//" + this.ControllerName;

            }

        }
        public string ControllersPath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(AreasName))
                {
                    return "Controllers";
                }
                return Path.Combine("Areas", AreasName, "Controllers");

            }


        }



        public void CreateProjectFolder(Project objProject, string folderPath)
        {
            ProjectItems objProjectItems = objProject.ProjectItems;
            foreach (var folder in folderPath.Split('\\'))
            {
                bool isExists = false;
                foreach (ProjectItem objProjectItem in objProjectItems)
                {
                    if (objProjectItem.Name == folder)
                    {
                        isExists = true;
                        objProjectItems = objProjectItem.ProjectItems;
                    }
                }
                if (!isExists)
                {
                    objProjectItems = objProjectItems.AddFolder(folder).ProjectItems;
                }
            }
        }
    }
}
