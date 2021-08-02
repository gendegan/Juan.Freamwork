using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Juan.Core
{
    /// <summary>
    /// 扩展表名运算
    /// </summary>
    public static partial class PartTableHelper
    {


        /// <summary>
        /// 获取模算法扩展表名
        /// </summary>
        /// <param name="ID">标识</param>
        /// <param name="tableNameObject">护展表名</param>
        /// <param name="moldValue">模大小</param>
        /// <returns></returns>
        public static string PartMoldTableName(this long ID, string tableNameObject, int moldValue = 10)
        {

            long modTableValue = ID % moldValue;
            if (string.IsNullOrWhiteSpace(tableNameObject))
            {
                return string.Format("{0}", modTableValue);
            }
            else
            {
                return string.Format("{0}_{1}", tableNameObject, modTableValue);
            }

        }
        /// <summary>
        /// 获取模算法扩展表名
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="moldValue"></param>
        /// <returns></returns>
        public static string PartMoldTableName(this long ID, int moldValue = 10)
        {

            return PartMoldTableName(ID, "", moldValue);

        }


        /// <summary>
        /// 获取模算法扩展表名
        /// </summary>
        /// <param name="ID">标识</param>
        /// <param name="tableNameObject">护展表名</param>
        /// <param name="moldValue">模大小</param>
        /// <returns></returns>
        public static string PartMoldTableName(this int ID, string tableNameObject, int moldValue = 10)
        {

            long modTableValue = ID % moldValue;
            if (string.IsNullOrWhiteSpace(tableNameObject))
            {
                return string.Format("{0}", modTableValue);
            }
            else
            {
                return string.Format("{0}_{1}", tableNameObject, modTableValue);
            }

        }
        /// <summary>
        /// 获取模算法扩展表名
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="moldValue"></param>
        /// <returns></returns>
        public static string PartMoldTableName(this int ID, int moldValue = 10)
        {

            return PartMoldTableName(ID, "", moldValue);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataObjectParam"></param>
        /// <param name="sourcetable"></param>
        /// <param name="connectionKeyOrString">连接键值或连接串</param>
        public static void CreatePartTableObject(string dataObjectParam, string sourcetable, string connectionKeyOrString)
        {
            if (dataObjectParam.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException("dataObjectParam参数不能为空");
            }
            if (sourcetable.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException("sourcetable参数不能为空");
            }
            if (connectionKeyOrString.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException("connectionKeyOrString参数不能为空");
            }
            string partTable = sourcetable.ToLower().Replace("_tb", "") + "_" + dataObjectParam + "_tb";
            DataSet objDataSet = MySqlHelper.ExecuteDataSet(connectionKeyOrString, string.Format("SHOW TABLES LIKE '{0}';", partTable));
            if (objDataSet.IsNull())
            {
                MySqlHelper.ExecuteNonQuery(connectionKeyOrString, string.Format("CREATE TABLE {0}  LIKE {1};", partTable, sourcetable));
            }
        }
    }
}
