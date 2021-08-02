using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Core
{
    /// <summary>
    /// 读取参数
    /// </summary>
    public partial class ReadData : ICloneable
    {


        /// <summary>
        /// 构造函数
        /// </summary>
        public ReadData()
        {
            TableName = "";
            CommandText = "";
            Condition = "";
            SortExpression = "";
            Fields = "*";
        }
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get;
            set;
        }

        /// <summary>
        /// 执行完整SQL语句
        /// </summary>
        public string CommandText
        {
            get;
            set;
        }

        /// <summary>
        ///查询条件
        /// </summary>
        public string Condition
        {
            get;
            set;
        }
        /// <summary>
        /// 排序
        /// </summary>
        public string SortExpression
        {
            get;
            set;
        }
        /// <summary>
        /// 返回字段
        /// </summary>
        public string Fields
        {
            get;
            set;
        }
        /// <summary>
        /// 页大小
        /// </summary>
        public int? PageSize
        {
            get;
            set;
        }
        /// <summary>
        /// 第几页
        /// </summary>
        public int? PageIndex
        {
            get;
            set;
        }
        /// <summary>
        /// 跳过多少条
        /// </summary>
        public int? Skip
        {
            get;
            set;
        }
        /// <summary>
        /// 返回条数
        /// </summary>
        public int? Take
        {
            get;
            set;
        }
        /// <summary>
        /// 参数
        /// </summary>
        public DataParameter[] Parameters
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual ReadData Clone()
        {


            ReadData clone = new ReadData()
            {
                CommandText = this.CommandText,
                Condition = this.Condition,
                Fields = this.Fields,
                PageIndex = this.PageIndex,
                PageSize = this.PageSize,
                Parameters = this.Parameters,
                Skip = this.Skip,
                SortExpression = this.SortExpression,
                TableName = this.TableName,
                Take = this.Take

            };
            return clone;
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }
    }
}
