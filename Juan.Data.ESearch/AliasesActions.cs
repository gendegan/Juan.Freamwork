using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juan.Data.ESearch
{
    public class AliasesActions
    {
        public AliasesActions()
        {
            actions = new List<Dictionary<string, AliasInfo>>();
        }
        public List<Dictionary<string, AliasInfo>> actions
        {
            get;
            set;
        }
        public void Add(OperatorType operatorType, string index, string alias)
        {
            Dictionary<string, AliasInfo> objAliasInfo = new Dictionary<string, AliasInfo>();
            objAliasInfo.Add(operatorType.ToString(), new AliasInfo(index, alias));
            actions.Add(objAliasInfo);
        }

    }
    public enum OperatorType
    {
        remove,
        add
    }

    public class AliasInfo
    {

        public AliasInfo(string _index, string _alias)
        {
            index = _index;
            alias = _alias;
        }
        public string index
        {
            get;
            set;
        }
        public string alias
        {
            get;
            set;
        }
    }
}
