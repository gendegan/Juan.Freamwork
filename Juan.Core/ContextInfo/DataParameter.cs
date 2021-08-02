using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Core
{
    /// <summary>
    /// 数据参数
    /// </summary>
    public class DataParameter
    {
        string _Name = "";
        /// <summary>
        /// 参数名称
        /// </summary>
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }
        /// <summary>
        /// 数据类型
        /// </summary>
        public DbType DbType
        {
            get;
            set;
        }

        /// <summary>
        /// 方向
        /// </summary>
        public ParameterDirection Direction
        {
            get;
            set;
        }

        /// <summary>
        /// 值
        /// </summary>
        public object Value
        {
            get;
            set;
        }

        /// <summary>
        /// 数据参数
        /// </summary>
        public DataParameter()
        {
            Direction = ParameterDirection.Input;
        }
        /// <summary>
        /// 数据参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="value">参数值</param>
        public DataParameter(string name, object value)
            : this(name, value, null, ParameterDirection.Input)
        {


        }

        /// <summary>
        /// 数据参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="dbType">类型</param>
        /// <param name="direction">参数方向</param>
        public DataParameter(string name, DbType dbType, ParameterDirection direction)
            : this(name, null, dbType, direction)
        {
        }
        /// <summary>
        /// 数据参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="value">值</param>
        /// <param name="direction">参数方向</param>
        public DataParameter(string name, object value, ParameterDirection direction)
            : this(name, value, null, direction)
        {
        }
        /// <summary>
        /// 数据参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="value">值</param>
        /// <param name="dbType">数据类型</param>
        public DataParameter(string name, object value, DbType dbType)
            : this(name, value, dbType, ParameterDirection.Input)
        {
        }

        /// <summary>
        /// 数据参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="value">值</param>
        /// <param name="dbType">类型</param>
        /// <param name="direction">参数方向</param>
        public DataParameter(string name, object value, DbType? dbType, ParameterDirection direction)
        {
            this.Name = name;
            this.Value = value;
            this.DbType = GetDataType(dbType);
            this.Direction = direction;


        }
        private DbType GetDataType(DbType? dbType)
        {
            if (dbType.HasValue)
            {
                return dbType.Value;
            }
            if (this.Value == null)
            {
                throw new ArgumentNullException(this.Name, "数据实体属性:" + this.Name + "值不空，因此无法获取参数DbType类型");

            }
            string typeName = this.Value.GetType().Name;

            if (typeName != null && typeName == "Byte[]")
            {
                return DbType.Binary;
            }

            return (DbType)System.Enum.Parse(typeof(DbType), typeName);
        }
        private DbType GetDataType(Type type)
        {

            string typeName = type.Name;

            if (typeName != null && typeName == "Byte[]")
            {
                return DbType.Binary;
            }


            return (DbType)System.Enum.Parse(typeof(DbType), typeName);
        }
    }

    /// <summary>
    /// 数据参数帮助类
    /// </summary>
    public static class DataParameterHelper
    {
        /// <summary>
        /// 创建参数
        /// </summary>
        /// <param name="parameterNames"></param>
        /// <param name="parmsValue"></param>
        /// <returns></returns>
        public static DataParameter[] CreateParameter(this string parameterNames, params object[] parmsValue)
        {
            if (string.IsNullOrWhiteSpace(parameterNames))
            {
                throw new ArgumentException("parameterNames,不能为空值");
            }

            if (parmsValue == null || parmsValue.Length == 0)
            {
                throw new ArgumentException("parmsValue,不能为空值");
            }
            string[] paramNames = parameterNames.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            if (paramNames.Length == 0)
            {
                throw new ArgumentException("parameterNames,不能为空值：" + paramNames.Length);
            }
            if (paramNames.Length == parmsValue.Length)
            {

                DataParameter[] param = new DataParameter[paramNames.Length];
                for (int i = 0; i < paramNames.Length; i++)
                {
                    param[i] = new DataParameter(paramNames[i], parmsValue[i]);
                }
                return param;
            }
            else
            {
                throw new ArgumentException("传入的参数个数和值的个数不一致");
            }

        }
        /// <summary>
        /// 合并参数不改变原来个数
        /// </summary>
        /// <param name="parameters">参数</param>
        /// <param name="dataParms"></param>
        /// <returns></returns>
        public static DataParameter[] MergeParameter(this IEnumerable<DataParameter> parameters, params DataParameter[] dataParms)
        {
            List<DataParameter> objDataParameterList = new List<DataParameter>();
            if (parameters != null && parameters.Count() > 0)
            {
                objDataParameterList.AddRange(parameters);
            }
            if (dataParms != null && dataParms.Length > 0)
            {
                objDataParameterList.AddRange(dataParms);
            }
            return objDataParameterList.ToArray();
        }


    }
}
