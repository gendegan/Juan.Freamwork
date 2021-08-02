using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JuanGenerate
{
    public sealed partial class JuanGeneratePackage
    {
        /// <summary>
        /// 配置连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectConfigHandler(object sender, EventArgs e)
        {
            try
            {
                SelectFileInfo objSelectFileInfo = GetSelectFileInfo();
                if (objSelectFileInfo == null)
                {
                    return;
                }
                ConnectConfigFrom objConnectConfigFrom = new ConnectConfigFrom(objSelectFileInfo);
                objConnectConfigFrom.Show();
            }
            catch (Exception objExp)
            {
                MessageBox.Show("配置链接串" + objExp.Message);
            }
        }
    }
}
