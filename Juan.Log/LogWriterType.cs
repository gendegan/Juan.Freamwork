using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juan.Core
{

    /// <summary>
    /// 日志写入类型
    /// </summary>
    public enum LogWriterType
    {
        /// <summary>
        /// 数据库
        /// </summary>
        [Enum("数据库")]
        DataWriter = 1,
        /// <summary>
        /// 文件
        /// </summary>
        [Enum("文件")]
        FileWriter = 2,

    }
}
