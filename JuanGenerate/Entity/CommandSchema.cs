using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JuanGenerate
{
    public class CommandSchema
    {
        public string ModuleName
        {
            get;
            set;
        }

        public string CommandName
        {
            get;
            set;
        }
        public string ProcessType
        {
            get;
            set;
        }
        public string RedirectUrl
        {
            get;
            set;
        }
        public bool IsTop
        {
            get;
            set;
        }
        public bool IsList
        {
            get;
            set;
        }

        public bool IsBotom
        {
            get;
            set;
        }
        public int SortIndex
        {
            get;
            set;
        }


        public string ToCommandCode(string ContextName, string PrimaryKey, bool IsToolBar)
        {
            if (CommandName == "Create")
            {
                return string.Format(CodeResource.GetCommandRedirect, this.CommandName, "\"" + this.RedirectUrl + "\"");
            }
            if (CommandName == "Remove")
            {
                return string.Format(CodeResource.GetCommandRenderPage, this.CommandName, string.Format("obj{0}.DeleteID({1});", ContextName, IsToolBar ? "gdvContent.SelectedRowDataKeys" : "e.CommandArgument.ToString()"));
            }
            if (CommandName == "Back")
            {
                return string.Format(CodeResource.GetCommandRedirect, this.CommandName, "\"" + this.RedirectUrl + "\"");
            }
            if (CommandName == "Search")
            {
                return CodeResource.GetCommandSearch;
            }
            if (CommandName == "Release")
            {
                return string.Format(CodeResource.GetCommandRenderPage, this.CommandName, string.Format("obj{0}.SetFields(\"IsRelease=1\", ReadOptions.Search(\"{1}=\" + {2}));", ContextName, PrimaryKey, IsToolBar ? "gdvContent.SelectedRowDataKeys" : "e.CommandArgument.ToString()"));
            }
            if (CommandName == "UnRelease")
            {
                return string.Format(CodeResource.GetCommandRenderPage, this.CommandName, string.Format("obj{0}.SetFields(\"IsRelease=0\", ReadOptions.Search(\"{1}=\" + {2}));", ContextName, PrimaryKey, IsToolBar ? "gdvContent.SelectedRowDataKeys" : "e.CommandArgument.ToString()"));
            }
            if (ProcessType == "Redirect")
            {
                return string.Format(CodeResource.GetCommandRedirect, this.CommandName, "\"" + this.RedirectUrl + "?" + PrimaryKey + "=\"+" + (IsToolBar ? "gdvContent.SelectedRowFirstKey" : "e.CommandArgument.ToString()"));
            }
            else if (ProcessType == "RedirectState")
            {
                return string.Format(CodeResource.GetCommandRedirectState, this.CommandName, "\"" + this.RedirectUrl + "?" + PrimaryKey + "=\"+" + (IsToolBar ? "gdvContent.SelectedRowFirstKey" : "e.CommandArgument.ToString()"));
            }
            else
            {
                return string.Format(CodeResource.GetCommandRenderPage, this.CommandName, "");
            }
        }
    }
}
