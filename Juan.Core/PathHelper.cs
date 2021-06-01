using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Core
{
    public static class PathHelper
    {
        // Methods
        public static string CurrentDataPath(this string applicationData)
        {
            if (string.IsNullOrWhiteSpace(applicationData))
            {
                throw new ArgumentNullException("参数applicationData不能为空");
            }
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), applicationData);
        }

        public static string CurrentDllPath(this string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException("参数fileName不能为空");
            }
            if (SysVariable.CurrentContext == null)
            {
                return Path.Combine(SysVariable.BaseDirectory, fileName);
            }
            return Path.Combine(SysVariable.BaseDirectory, "bin", fileName);
        }

        public static string CurrentPath(this string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException("参数fileName不能为空");
            }
            return Path.Combine(SysVariable.BaseDirectory, fileName);
        }

        // Properties
        public static string BaseDirectory
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }
    }




}
