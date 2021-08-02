using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Core
{
    public enum HtmlType
    {
        Tag,
        Script,
        Table,
        Class,
        Style,
        Font,
        Object,
        Marquee,
        Xml
    }

    public enum JsonSetType
    {
        DateTime,
        NullIgnore,
        LongString,
        Stamp,
        CamelCase,
        StringEnum,
        IgnoreField,
        OnlyField,
        PageInfo,
        EscapeNonAscii,
        EscapeHtml,
        StampSecond
    }

    public enum ShowEnumType
    {
        Value,
        Description,
        Key
    }


    /// <summary>
    /// 连接类型
    /// </summary>
    public enum ConnectionType
    {
        /// <summary>
        /// 自动选择
        /// </summary>
        Auto,
        /// <summary>
        /// 写库
        /// </summary>
        Write

    }


    /// <summary>
    /// 上下文数据库
    /// </summary>
    public enum ContextType
    {
        /// <summary>
        /// 微软Sql
        /// </summary>
        [Enum("微软Sql")]
        Sql,
        /// <summary>
        /// Oracle
        /// </summary>
        [Enum("Oracle")]
        Oracle,
        /// <summary>
        /// Mysql
        /// </summary>
        [Enum("Mysql")]
        MySql
    }

}
