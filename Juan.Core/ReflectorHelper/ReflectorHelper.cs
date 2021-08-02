using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;

namespace Juan.Core
{
    /// <summary>
    /// 反射帮助类
    /// </summary>
    public static partial class ReflectorHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        public static ResourceSet FindResourceSet(this Assembly assembly, string resourceName)
        {
            Stream stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
            {
                return null;
            }
            ResourceSet resourceSet = new ResourceSet(stream);
            stream.Close();
            return resourceSet;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        public static Stream FindResourceStream(this Assembly assembly, string resourceName)
        {

            return assembly.GetManifestResourceStream(resourceName);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="resourceName"></param>
        /// <param name="encode">编码</param>
        /// <returns></returns>
        public static string FindResourceText(this Assembly assembly, string resourceName, Encoding encode = null)
        {
            Stream objStream = assembly.GetManifestResourceStream(resourceName);
            return objStream.ToText();
        }


    }
}
