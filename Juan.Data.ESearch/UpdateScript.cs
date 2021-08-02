using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Juan.Core;
using PlainElastic7.Net.Serialization;
namespace Juan.Data.ESearch
{
    public class UpdateScript
    {

        public UpdateScript()
        {

            Script = new ScriptInfo();
            Upsert = null;
        }

        public ScriptInfo Script
        {
            get;
            set;
        }

        public object Upsert
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        public string Routing
        {
            get;
            set;
        }

        public string ToJsonString()
        {
            if (Upsert != null)
            {
                return string.Format("{{\"script\":{0},\"upsert\":{1}}}", ESHelper.Serializer.ToJson(Script), ESHelper.Serializer.ToJson(Upsert));
            }
            else
            {
                return string.Format("{{\"script\":{0}}}", ESHelper.Serializer.ToJson(Script));
            }

        }
    }

    public class ScriptInfo
    {

        public ScriptInfo()
        {
            @params = new Dictionary<string, object>();
            source = "";
            lang = "painless";
        }
        public string source
        {
            get;
            set;

        }
        public string lang
        {
            get;
            set;
        }

        public object @params
        {
            get;
            set;
        }
    }
}
