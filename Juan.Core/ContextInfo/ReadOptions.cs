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
    public partial class ReadOptions
    {



        /// <summary>
        /// 构造函数
        /// </summary>
        public ReadOptions()
        {

            TableName = "";
            CommandText = "";
            Condition = "";
            SortExpression = "";
            Fields = "*";
            QueryItemList = new List<QueryItem>();
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
        /// 自动查询
        /// </summary>
        public List<QueryItem> QueryItemList
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
        /// 是否查询视图
        /// </summary>
        public bool IsView
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ReadOptions Clone()
        {
            ReadOptions clone = new ReadOptions()
            {
                CommandText = this.CommandText,
                Condition = this.Condition,
                Fields = this.Fields,
                IsView = this.IsView,
                PageIndex = this.PageIndex,
                PageSize = this.PageSize,
                Parameters = this.Parameters,
                Skip = this.Skip,
                SortExpression = this.SortExpression,
                TableName = this.TableName,
                Take = this.Take,
                QueryItemList = this.QueryItemList
            };

            return clone;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ReadData ToReadData()
        {
            ReadData objReadData = ReadData.Create();
            objReadData.CommandText = this.CommandText;
            objReadData.Condition = this.MergeCondition();
            objReadData.Fields = this.Fields;
            objReadData.PageIndex = this.PageIndex;
            objReadData.PageSize = this.PageSize;
            objReadData.Parameters = this.MergeParameters();
            objReadData.Skip = this.Skip;
            objReadData.SortExpression = this.SortExpression;
            objReadData.TableName = this.TableName;
            return objReadData;
        }

    }
}
