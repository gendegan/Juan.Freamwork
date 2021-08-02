
namespace Juan.Data
{
    /// <summary>
    /// 表排序
    /// </summary>
    public class TableSortInfo<T>
    {
        /// <summary>
        /// 排序标识
        /// </summary>
        public T ID
        {
            get;
            set;
        }
        /// <summary>
        /// 排序值
        /// </summary>
        public int SortValue
        {
            get;
            set;
        }

    }
}
