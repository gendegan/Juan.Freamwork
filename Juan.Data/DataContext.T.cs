using Juan.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Data
{
    /// <summary>
    /// 数据上下文
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <typeparam name="Key">主键类型</typeparam>
    public partial class DataContext<T, Key> : IDbContext<T, Key> where T : class, new()
    {



        DbContext<T, Key> _DbContext = null;

        /// <summary>
        /// 当前数据库上下文
        /// </summary>
        public DbContext<T, Key> DbContext
        {
            get
            {
                return _DbContext;
            }

        }
        /// <summary>
        /// 当前数据操作上下文
        /// </summary>
        public override DbContext Context
        {
            get
            {
                return _DbContext.Context;

            }

        }
        /// <summary>
        /// 分表信息
        /// </summary>
        public ShardTable ShardTableValue
        {
            get;
            set;
        }

        /// <summary>
        /// 当前程序集名称
        /// </summary>
        private string AssemblyName
        {
            get
            {
                return this.GetType().Assembly.GetName().Name;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataContext()
            : this("", ConnectionType.Auto)
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionType">连接类型</param>
        public DataContext(ConnectionType connectionType)
            : this("", connectionType)
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="partName">分表名称</param>
        /// <param name="connectionType">连接类型</param>
        public DataContext(string partName, ConnectionType connectionType = ConnectionType.Auto)
            : base(partName)
        {

            _DbContext = DbContext<T, Key>.CreateContext(partName, connectionType);
        }



        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="contextKey">上下文键值</param>
        /// <param name="partName">分表名称</param>
        /// <param name="connectionType">连接类型</param>
        public DataContext(string contextKey, string partName, ConnectionType connectionType = ConnectionType.Auto)
            : base(partName)
        {
            _DbContext = DbContext<T, Key>.CreateContext(contextKey, partName, connectionType);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="contextKey">上下文键值</param>
        /// <param name="shardValue">分表值</param>
        /// <param name="dbShardNum">分数据库个数[1为分表不分库]</param>
        /// <param name="tableShardNum">表名</param>
        /// <param name="connectionType"></param>
        public DataContext(string contextKey, object shardValue, int dbShardNum, int tableShardNum, ConnectionType connectionType = ConnectionType.Auto)
            : base(shardValue.ShardTablePosition(dbShardNum, tableShardNum).ToString())
        {
            ShardTable objShardTable = shardValue.ShardTableInfo(dbShardNum, tableShardNum);
            ShardTableValue = objShardTable;
            if (dbShardNum > 1)
            {
                contextKey = contextKey + objShardTable.DBPosition;
            }

            _DbContext = DbContext<T, Key>.CreateContext(contextKey, objShardTable.TablePosition.ToString(), connectionType);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="shardValue">分表值</param>
        /// <param name="dbShardNum">分数据库个数[1为分表不分库]</param>
        /// <param name="tableShardNum">表名</param>
        /// <param name="connectionType"></param>
        public DataContext(object shardValue, int dbShardNum, int tableShardNum, ConnectionType connectionType = ConnectionType.Auto)
            : base(shardValue.ShardTablePosition(dbShardNum, tableShardNum).ToString())
        {
            string contextKey = "";
            ContextAttribute objContextAttribute = ContextHelper.GetContextInfo(typeof(T));
            if (objContextAttribute == null || string.IsNullOrWhiteSpace(objContextAttribute.ContextKey))
            {
                contextKey = typeof(T).Assembly.GetName().Name;
            }
            else
            {
                contextKey = objContextAttribute.ContextKey;
            }
            ShardTable objShardTable = shardValue.ShardTableInfo(dbShardNum, tableShardNum);

            ShardTableValue = objShardTable;
            if (dbShardNum > 1)
            {
                contextKey = contextKey + "." + objShardTable.DBPosition;
            }

            _DbContext = DbContext<T, Key>.CreateContext(contextKey, objShardTable.TablePosition.ToString(), connectionType);
        }


    }
}
