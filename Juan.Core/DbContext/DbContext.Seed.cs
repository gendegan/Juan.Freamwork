
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Data;
namespace Juan.Core
{
    public abstract partial class DbContext
    {



        /// <summary>
        /// 下一个键值
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="step">步长</param>
        /// <param name="contextKey">上下文键值</param>
        /// <returns></returns>
        public Int64 NextKey(string tableName, int step = 1, string contextKey = "Juan")
        {
            tableName.ArgumentNoNull("tableName", "tableName不能为空");
            DbContext objDbContext = DbContext.Create(contextKey, Core.ConnectionType.Write);
            object value = objDbContext.ExecuteScalar(CommandType.StoredProcedure, "Sys_GetSeedNextValue", "p_seedCode,p_steplength".CreateParameter(tableName.ToLower(), step));

            Int64 SeedNextValue = Convert.ToInt64(value.ToString());
            if (SeedNextValue == 0)
            {
                throw new Exception("获取下一个种子出现异常:" + tableName);
            }
            return SeedNextValue;
        }
        /// <summary>
        /// 下一个键值
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="step">步长</param>
        /// <param name="contextKey">上下文键值</param>
        /// <returns></returns>
        public int NextKeyInt(string tableName, int step = 1, string contextKey = "Juan")
        {
            Int64 SeedValue = NextKey(tableName, step, contextKey);
            return Convert.ToInt32(SeedValue);
        }






    }
}
