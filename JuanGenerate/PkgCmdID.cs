using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuanGenerate
{
    static class PkgCmdIDList
    {
        /// <summary>
        /// 数据库连接串配置
        /// </summary>
        public const uint cmdidConnectConfigCommand = 0x101;
        /// <summary>
        ///上下文生成
        /// </summary>
        public const uint cmdidContextCommand = 0x102;

        /// <summary>
        ///上下文生成
        /// </summary>
        public const uint cmdidUpgradeCommand = 0x103;

        /// <summary>
        /// 更新代码单个类
        /// </summary>
        public const uint cmdidUpdateCodeCommand = 0x105;
        /// <summary>
        /// 新增编辑页
        /// </summary>
        public const uint cmdidEditCommand = 0x107;
        /// <summary>
        /// 生成列表界面
        /// </summary>
        public const uint cmdidListCommand = 0x108;

        /// <summary>
        /// 生成详情界面
        /// </summary>
        public const uint cmdidViewCommand = 0x109;

        /// <summary>
        /// 生成列表界面
        /// </summary>

        public const uint cmdidListMvcCommand = 0x110;
        /// <summary>
        /// 新增编辑页
        /// </summary>
        public const uint cmdidEditMvcCommand = 0x111;

    };
}
