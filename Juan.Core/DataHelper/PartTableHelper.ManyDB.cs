using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Juan.Core
{

    /// <summary>
    /// 
    /// </summary>
    public class ShardTable
    {
        /// <summary>
        /// 表位置
        /// </summary>
        public int TablePosition
        {
            get;
            set;
        }
        /// <summary>
        /// 数据库位置
        /// </summary>
        public int DBPosition
        {
            get;
            set;
        }
    }
    /// <summary>
    /// 扩展表名运算
    /// </summary>
    public static partial class PartTableHelper
    {

        /// <summary>
        /// 数据库个数
        /// </summary>
        /// <param name="shardValue">分片值</param>
        /// <param name="dbShardNum">数据库个数</param>
        /// <param name="tableShardNum">总共表个数</param>
        /// <returns></returns>
        public static ShardTable ShardTableInfo(this object shardValue, int dbShardNum, int tableShardNum)
        {
            long shard_value = Convert.ToInt64(shardValue.ToString());
            ShardTable objPartTable = new ShardTable();
            int tablePosition = (int)(shard_value % tableShardNum);
            int dbPosition = (int)(tablePosition / (tableShardNum / dbShardNum));
            objPartTable.TablePosition = tablePosition;
            objPartTable.DBPosition = dbPosition;
            return objPartTable;
        }

        /// <summary>
        /// 数据库个数
        /// </summary>
        /// <param name="shardValue">分片值</param>
        /// <param name="dbShardNum">数据库个数</param>
        /// <param name="tableShardNum">总共表个数</param>
        /// <returns></returns>
        public static int ShardTablePosition(this object shardValue, int dbShardNum, int tableShardNum)
        {

            long shard_value = Convert.ToInt64(shardValue.ToString());

            return (int)(shard_value % tableShardNum);


        }
        /// <summary>
        /// 数据库个数
        /// </summary>
        /// <param name="shardValue">分片值</param>
        /// <param name="dbShardNum">数据库个数</param>
        /// <param name="tableShardNum">总共表个数</param>
        /// <returns></returns>
        public static int ShardDBPosition(this object shardValue, int dbShardNum, int tableShardNum)
        {
            long shard_value = Convert.ToInt64(shardValue.ToString());
            int tablePosition = (int)(shard_value % tableShardNum);
            return (int)(tablePosition / (tableShardNum / dbShardNum));

        }




        //例子32*32就是32个数据库每个库32表名，
        //扩展性1，前期几个数据库放在一台机器上或一个集群上,然后量大了，每个数据库分开作集群，
        // 扩展性2  当进行了32个集群了，再根据(32*2^n)*(32/2^n)规则最大到1个数据库只有一个表

    }
}
