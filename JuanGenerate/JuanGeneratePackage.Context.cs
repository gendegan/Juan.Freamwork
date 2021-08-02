using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JuanGenerate
{
    public sealed partial class JuanGeneratePackage
    {
        /// <summary>
        /// 新增业务层
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextHandler(object sender, EventArgs e)
        {
            try
            {
                SelectFileInfo objSelectFileInfo = GetSelectFileInfo();
                if (objSelectFileInfo == null)
                {
                    return;
                }
                if (!objSelectFileInfo.IsExistGenerateConfig())
                {
                    return;
                }


                ContextForm objContextForm = new ContextForm(objSelectFileInfo);
                objContextForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("新建上下文抛出异常，异常信息：" + ex.ToString());

            }
        }


        private void UpgradeContextHandler(object sender, EventArgs e)
        {
            try
            {
                SelectFileInfo objSelectFileInfo = GetSelectFileInfo();
                if (objSelectFileInfo == null)
                {
                    return;
                }
                if (File.Exists(objSelectFileInfo.GeneratePath))
                {
                    MessageBox.Show("新版配置文件已经存在无法一键升级");
                    return;
                }


                CodeConfigHelper _CodeConfigHelper = new CodeConfigHelper(objSelectFileInfo.CodeConfigPath);

                if (!_CodeConfigHelper.IsExistCodeConfig())
                {
                    MessageBox.Show("不存在旧版配置文件无法升级");
                    return;
                }
                if (!_CodeConfigHelper.LoadCodeConfigXml())
                {
                    MessageBox.Show("旧版本配置文件加载出现异常因此无法升级");
                    return;
                }
                GenerateHelper objGenerateHelper = new GenerateHelper(objSelectFileInfo.GeneratePath);
                if (!objGenerateHelper.LoadConfigXml())
                {
                    return;
                }

                _CodeConfigHelper.UpgradeConfig(objGenerateHelper);

                if (!File.Exists(objGenerateHelper.GenerateConfigPath))
                {
                    objGenerateHelper.Save();
                    objSelectFileInfo.Project.ProjectItems.AddFromFile(objGenerateHelper.GenerateConfigPath);
                }

                ContextForm objContextForm = new ContextForm(objSelectFileInfo, true);
                objContextForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("升级上下文抛出异常，异常信息：" + ex.ToString());

            }
        }

    }
}
