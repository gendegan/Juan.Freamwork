using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Core
{
    /// <summary>
    /// 页信息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class PageInfo<T> where T : class, new()
    {

        /// <summary>
        /// 
        /// </summary>
        public PageInfo()
        {
            PageIndex = 0;
            PageSize = 0;
            RecordCount = 0;
            Data = default(T);
        }
        /// <summary>
        /// 当前索引号
        /// </summary>
        public int PageIndex
        {
            get;
            set;
        }
        /// <summary>
        /// 当前页号
        /// </summary>
        public int PageNo
        {
            get
            {
                return PageIndex + 1;
            }

        }
        /// <summary>
        /// 上一页索引
        /// </summary>
        public int PreviousIndex
        {
            get
            {
                if (PageIndex <= 0)
                {
                    return 0;
                }
                return PageIndex - 1;
            }
        }
        /// <summary>
        /// 下一页索引
        /// </summary>
        public int NextIndex
        {
            get
            {
                long _PageCount = PageCount;
                if (_PageCount == 0)
                {
                    return 0;
                }
                if (PageNo >= _PageCount)
                {
                    return PageIndex;

                }
                return PageIndex + 1;
            }
        }

        /// <summary>
        /// 是否下一页
        /// </summary>
        public bool IsNext
        {
            get
            {
                long _PageCount = PageCount;
                if (_PageCount == 0)
                {
                    return false;
                }
                if (PageNo >= _PageCount)
                {
                    return false;

                }
                return true;
            }

        }

        /// <summary>
        /// 是否上一页
        /// </summary>
        public bool IsPrevious
        {
            get
            {
                if (PageIndex == 0)
                {
                    return false;
                }
                return true;
            }

        }

        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize
        {
            get;
            set;
        }
        /// <summary>
        /// 页数
        /// </summary>
        public long PageCount
        {
            get
            {
                if (PageSize == 0)
                {
                    return 0;
                }
                if (RecordCount % PageSize == 0)
                {
                    return RecordCount / PageSize;
                }
                return RecordCount / PageSize + 1;
            }
        }
        /// <summary>
        /// 总记录数
        /// </summary>
        public long RecordCount
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public int RecordCountInt
        {
            get
            {
                return Convert.ToInt32(RecordCount);
            }

        }


        /// <summary>
        /// 数据
        /// </summary>
        public T Data
        {
            get;
            set;
        }
        /// <summary>
        /// 扩展数据
        /// </summary>
        public object ExtendData
        {
            get;
            set;
        }
    }
}
