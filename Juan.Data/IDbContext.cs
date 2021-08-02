using Juan.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Data
{
    /// <summary>
    /// 数据上下文抽象类
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <typeparam name="Key">主键类型</typeparam>
    public abstract partial class IDbContext<T, Key> where T : class, new()
    {


        /// <summary>
        /// 实体数据类型
        /// </summary>
        public Type DataType
        {
            get
            {

                return typeof(T);
            }
        }
        /// <summary>
        /// 获取键值类型
        /// </summary>
        public Type KeyType
        {
            get
            {
                return typeof(Key);
            }
        }

        /// <summary>
        /// 表结构信息
        /// </summary>
        public abstract TableAttribute TableSchema
        {
            get;

        }


        /// <summary>
        /// 主键信息
        /// </summary>
        public abstract PrimaryKeyAttribute PrimaryKey
        {
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public abstract DbContext Context
        {
            get;
        }




        /// <summary>
        /// 获取表名
        /// </summary>
        /// <returns></returns>
        public string TableName
        {

            get
            {
                if (string.IsNullOrWhiteSpace(PartName))
                {
                    return TableSchema.TableName;
                }
                else
                {
                    return TableSchema.TableFormat.FormatValue(PartName);
                }
            }

        }
        /// <summary>
        /// 
        /// </summary>
        public string DBTableName
        {

            get
            {
                if (string.IsNullOrWhiteSpace(this.DBName))
                {
                    return this.TableName;
                }
                else
                {
                    return string.Format("{0}.{1}", this.DBName, this.TableName);
                }
            }

        }
        /// <summary>
        /// 
        /// </summary>
        public string DBViewName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.DBName))
                {
                    return this.ViewName;
                }
                else
                {
                    return string.Format("{0}.{1}", this.DBName, this.ViewName);
                }
            }
        }
        /// <summary>
        /// 获取视图名
        /// </summary>
        /// <returns></returns>
        public string ViewName
        {
            get
            {

                if (string.IsNullOrWhiteSpace(PartName))
                {
                    return TableSchema.ViewName;
                }
                else
                {
                    return TableSchema.ViewFormat.FormatValue(PartName);
                }
            }

        }
        /// <summary>
        /// 分表名称
        /// </summary>
        protected string _PartName = "";

        /// <summary>
        /// 分表名称
        /// </summary>
        public string PartName
        {
            get
            {
                return _PartName;
            }
            set
            {
                _PartName = value;
            }

        }

        /// <summary>
        /// 数据库名称
        /// </summary>
        protected string _DBName = "";

        /// <summary>
        /// 数据库名称
        /// </summary>
        public virtual string DBName
        {
            get
            {
                return _DBName;
            }
            set
            {
                _DBName = value;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="partName"></param>
        public IDbContext(string partName)
        {
            _PartName = partName;
        }


    }
}
