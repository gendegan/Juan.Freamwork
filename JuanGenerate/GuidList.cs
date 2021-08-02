using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuanGenerate
{
    static class GuidList
    {
        public const string guidJuanGeneratePkgString = "25632b12-5802-4fc4-8a99-8f7db38f368e";
        public const string guidJuanGenerateCmdSetString = "bfadf41c-b6a9-47dd-8c59-5b886c4f4894";
        public const string guidJuanGenerateEditorFactoryString = "0c7fe37f-04e3-4a18-928b-8314c89910a6";

        public static readonly Guid guidJuanGenerateCmdSet = new Guid(guidJuanGenerateCmdSetString);
        public static readonly Guid guidJuanGenerateEditorFactory = new Guid(guidJuanGenerateEditorFactoryString);
    };
}
