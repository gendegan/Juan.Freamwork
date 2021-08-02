using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;

namespace Juan.Core
{
    /// <summary>
    /// Zip压缩
    /// </summary>
    public static class ZipHelper
    {

        /// <summary>
        /// 创建 zip 存档，该存档包括指定目录的文件和目录，使用指定压缩级别，以及可以选择包含基目录。
        /// </summary>
        /// <param name="sourceDirectoryName"></param>
        /// <param name="destinationArchiveFileName"></param>
        public static void CompressZip(this string sourceDirectoryName, string destinationArchiveFileName)
        {
            ZipFile.CreateFromDirectory(sourceDirectoryName, destinationArchiveFileName);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceDirectoryName"></param>
        /// <param name="destinationArchiveFileName"></param>
        /// <param name="compressionLevel"></param>
        /// <param name="includeBaseDirectory"></param>
        public static void CompressZip(this string sourceDirectoryName, string destinationArchiveFileName, CompressionLevel compressionLevel, bool includeBaseDirectory)
        {
            ZipFile.CreateFromDirectory(sourceDirectoryName, destinationArchiveFileName, compressionLevel, includeBaseDirectory);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceDirectoryName"></param>
        /// <param name="destinationArchiveFileName"></param>
        /// <param name="compressionLevel"></param>
        /// <param name="includeBaseDirectory"></param>
        /// <param name="entryNameEncoding"></param>
        public static void CompressZip(this string sourceDirectoryName, string destinationArchiveFileName, CompressionLevel compressionLevel, bool includeBaseDirectory, Encoding entryNameEncoding)
        {
            ZipFile.CreateFromDirectory(sourceDirectoryName, destinationArchiveFileName, compressionLevel, includeBaseDirectory, entryNameEncoding);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceArchiveFileName"></param>
        /// <param name="destinationDirectoryName"></param>
        public static void DecompressZip(this string sourceArchiveFileName, string destinationDirectoryName)
        {

            ZipFile.ExtractToDirectory(sourceArchiveFileName, destinationDirectoryName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceArchiveFileName"></param>
        /// <param name="destinationDirectoryName"></param>
        /// <param name="entryNameEncoding"></param>
        public static void DecompressZip(this string sourceArchiveFileName, string destinationDirectoryName, Encoding entryNameEncoding)
        {
            ZipFile.ExtractToDirectory(sourceArchiveFileName, destinationDirectoryName, entryNameEncoding);
        }
    }

}
