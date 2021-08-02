using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Core
{
    /// <summary>
    /// 用户操作类型
    /// </summary>
    public enum OperationType
    {
        /// <summary>
        /// 新增
        /// </summary>
        [EnumAttribute("新增")]
        Insert = 1,
        /// <summary>
        /// 修改
        /// </summary>
        [EnumAttribute("修改")]
        Update = 2,
        /// <summary>
        /// 删除
        /// </summary>
        [EnumAttribute("删除")]
        Delete = 3,
        /// <summary>
        /// 发布
        /// </summary>
        [EnumAttribute("发布")]
        Release = 4,
        /// <summary>
        /// 取消发布
        /// </summary>
        [EnumAttribute("取消发布")]
        UnRelease = 5,
        /// <summary>
        /// 审核
        /// </summary>
        [EnumAttribute("审核")]
        Audit = 6,
        /// <summary>
        /// 取消审核
        /// </summary>
        [EnumAttribute("取消审核")]
        UnAudit = 7,
        /// <summary>
        /// 推荐
        /// </summary>
        [EnumAttribute("推荐")]
        Vouch = 8,
        /// <summary>
        /// 取消推荐
        /// </summary>
        [EnumAttribute("取消推荐")]
        UnVouch = 9,
        /// <summary>
        /// 置顶
        /// </summary>
        [EnumAttribute("置顶")]
        Top = 10,
        /// <summary>
        /// 置顶
        /// </summary>
        [EnumAttribute("取消置顶")]
        CancelTop = 11,
        /// <summary>
        /// 移动记录
        /// </summary>
        [EnumAttribute("移动记录")]
        MoveRow = 12,
        /// <summary>
        /// 交换记录
        /// </summary>
        [EnumAttribute("交换记录")]
        ChanageMove = 13,
        /// <summary>
        /// 其它
        /// </summary>
        [EnumAttribute("其它操作")]
        Other = 1000,

    }
}
