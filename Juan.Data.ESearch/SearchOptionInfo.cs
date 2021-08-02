using PlainElastic7.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Juan.Core;
namespace Juan.Data.ESearch
{
    public class SearchOptionInfo
    {
        public string Routing
        {
            get;
            set;
        }
        public string IndexName
        {
            get;
            set;
        }
        public string TypeName
        {
            get;
            set;
        }

        public bool IsNoInfo()
        {
            return Routing.IsNoNullOrWhiteSpace() && IndexName.IsNoNullOrWhiteSpace() && TypeName.IsNoNullOrWhiteSpace();
        }
    }
}
