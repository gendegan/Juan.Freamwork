using Juan.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Data
{
    public partial class DataContext<T, Key> : IDbContext<T, Key> where T : class, new()
    {
        /// <summary>
        /// 表信息
        /// </summary>
        public override TableAttribute TableSchema
        {
            get
            {
                return DbContext.TableSchema;
            }
        }
        /// <summary>
        /// 主键信息
        /// </summary>
        public override PrimaryKeyAttribute PrimaryKey
        {
            get
            {
                return DbContext.PrimaryKey;
            }

        }


    }
}
