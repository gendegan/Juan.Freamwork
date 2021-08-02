using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace JuanGenerate
{
    public static class NameHeper
    {
        private static Regex cleanRegEx = new Regex(@"\s+|_|-|\.", RegexOptions.Compiled);

        public static string CleanName(this string name)
        {
            return name;
            //cleanRegEx.Replace(name, "");
        }
        public static string PascalCase(this string name)
        {
            string output = CleanName(name);
            return char.ToUpper(output[0]) + output.Substring(1);
        }

        public static string PropertyName(this string column)
        {
            return PascalCase(column);
        }

        public static string MemberName(this string column)
        {
            return "_" + CamelCase(column);
        }

        public static string CamelCase(this string name)
        {
            string output = CleanName(name);
            return char.ToLower(output[0]) + output.Substring(1);
        }

    }
}
