using Juan.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Juan.Data
{
    /// <summary>
    /// DbContext
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <typeparam name="Key">主键类型</typeparam>
    public abstract partial class DbContext<T, Key> : IDbContext<T, Key> where T : class,new()
    {





        /// <summary>
        /// 当前数据库上下文
        /// </summary>
        private DbContext _DbContext;
        /// <summary>
        /// 当前数据库上下文
        /// </summary>
        public override DbContext Context
        {
            get
            {

                return _DbContext;

            }

        }


        PrimaryKeyAttribute _PrimaryKeyAttribute = null;
        /// <summary>
        /// 主键信息
        /// </summary>
        public override PrimaryKeyAttribute PrimaryKey
        {
            get
            {
                return _PrimaryKeyAttribute;
            }

        }

        TableAttribute _TableSchemaAttribute = null;
        /// <summary>
        /// 表结构信息
        /// </summary>
        public override TableAttribute TableSchema
        {
            get
            {
                return _TableSchemaAttribute;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="context">数据操作上下文</param>
        /// <param name="partName">分表名称</param>
        public DbContext(DbContext context, string partName = "")
            : base(partName)
        {
            _DbContext = context;
            _DbContext.DbExecute = new DbLogExecute();
            _PrimaryKeyAttribute = PrimaryKeyHelper.GetPrimaryKeyAttribute<T>();
            if (PrimaryKey == null)
            {
                throw new ArgumentException("实体" + typeof(T).ToString() + "未设置主键PrimaryKey特性");
            }
            _TableSchemaAttribute = TableHelper.GetTableAttribute<T>();
            if (TableSchema == null)
            {
                throw new ArgumentException("实体" + typeof(T).ToString() + "未设置主键TableSchema特性");
            }

        }

    }
}
