using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JuanGenerate
{
    public class ContextInfo
    {
        public ContextInfo()
        {
            UIProjectName = "";
            UIProjectPath = "";
            ContextKey = "";
        
        }
        public string TableName
        {
            get;
            set;
        }

        public string TableFormat
        {
            get;
            set;
        }

        public string ViewName
        {
            get;
            set;
        }

        public string ViewFormat
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
    

        public string ContextKey
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
    }
}
