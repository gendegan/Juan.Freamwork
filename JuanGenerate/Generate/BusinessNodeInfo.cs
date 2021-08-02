using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JuanGenerate
{
    public class BusinessNodeInfo
    {

        public BusinessNodeInfo()
        {
            UIProjectName = "";
            UIProjectPath = "";
          
            ConnectionKeyOrConnectionString = "";
        }
        public string TableName
        {
            get;
            set;
        }
        public string Description
        {
            get;
            set;
        }

        public string EntityName
        {
            get;
            set;
        }

        public string ConnectionKeyOrConnectionString
        {
            get;
            set;
        }
        public string ConnectionKey
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
        public string ModuleID
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
    }
}
