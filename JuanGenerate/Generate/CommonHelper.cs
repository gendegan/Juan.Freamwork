using EnvDTE;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace JuanGenerate
{
    public static class CommonHelper
    {
        /// <summary>
        /// 显示代码
        /// </summary>
        /// <param name="code"></param>
        public static void ShowCodeForm(string generateCode)
        {
            ShowCodeForm objShowCodeForm = new ShowCodeForm(generateCode);
            objShowCodeForm.Show();

        }

        public static string FindResourceText(this string resourceName)
        {
            Assembly asmbly = Assembly.GetExecutingAssembly();
            Stream objStream = asmbly.GetManifestResourceStream(resourceName);
            if (objStream == null)
            {
                return "";
            }
            using (Stream s = objStream)
            {
                using (StreamReader sr = new StreamReader(s, System.Text.Encoding.UTF8))
                {
                    return sr.ReadToEnd();
                }
            };
        }

        public static string ReadAttribute(this XmlNode objXmlNode, string AttributeName, string defaultValue = "")
        {
            XmlAttribute objXmlAttribute = objXmlNode.Attributes[AttributeName];
            if (objXmlAttribute != null)
            {
                return objXmlAttribute.Value;
            }
            else
            {
                return defaultValue;
            }
        }

        public static int ReadAttributeInt(this XmlNode objXmlNode, string AttributeName, int defaultValue)
        {
            XmlAttribute objXmlAttribute = objXmlNode.Attributes[AttributeName];
            string value = "";
            if (objXmlAttribute != null)
            {
                value = objXmlAttribute.Value;

            }
            if (string.IsNullOrWhiteSpace(value))
            {
                return defaultValue;
            }
            return int.Parse(value);

        }

        public static bool ReadAttributeBool(this XmlNode objXmlNode, string AttributeName, bool defaultValue)
        {
            XmlAttribute objXmlAttribute = objXmlNode.Attributes[AttributeName];
            string value = "";
            if (objXmlAttribute != null)
            {
                value = objXmlAttribute.Value;

            }
            if (string.IsNullOrWhiteSpace(value))
            {
                return defaultValue;
            }
            return bool.Parse(value);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objDataGridViewRow"></param>
        /// <param name="CellName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string ReadCell(this DataGridViewRow objDataGridViewRow, string CellName, string defaultValue = "")
        {
            DataGridViewCell objDataGridViewCell = objDataGridViewRow.Cells[CellName];
            if (objDataGridViewCell == null)
            {
                return defaultValue;
            }
            else
            {
                object value = objDataGridViewCell.Value;
                if (value == null)
                {
                    return defaultValue;
                }
                string valueString = value.ToString();
                if (string.IsNullOrEmpty(valueString))
                {
                    return defaultValue;

                }
                else
                {
                    return valueString;
                }
            }
        }

        public static bool ReadCellBool(this DataGridViewRow objDataGridViewRow, string CellName, bool defaultValue = false)
        {
            DataGridViewCell objDataGridViewCell = objDataGridViewRow.Cells[CellName];
            if (objDataGridViewCell == null)
            {
                return false;
            }
            else
            {
                object value = objDataGridViewCell.Value;
                if (value == null)
                {
                    return false;
                }
                string valueString = value.ToString();
                if (string.IsNullOrEmpty(valueString))
                {
                    return false;

                }
                else
                {
                    return bool.Parse(valueString);
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <param name="generateCode"></param>
        public static void WriteCode(string filePath, string fileName, string generateCode, bool isPreviewCode = false, Project project = null)
        {

            if (isPreviewCode)
            {
                ShowCodeForm(generateCode);
                return;
            }
            if (File.Exists(filePath))
            {
                if (MessageBox.Show("确定要覆盖" + fileName + "文件吗?此操作不可恢复！", "", MessageBoxButtons.OKCancel) == DialogResult.OK)//如果点击“确定”按钮
                {
                    File.WriteAllText(filePath, generateCode, Encoding.UTF8);

                }
                else
                {
                    ShowCodeForm(generateCode);
                }
            }
            else
            {
                FileStream fs = File.Create(filePath);
                fs.Close();
                File.WriteAllText(filePath, generateCode, Encoding.UTF8);
                if (project != null)
                {
                    project.ProjectItems.AddFromFile(filePath);
                }
                MessageBox.Show(fileName + "生成成功");
            }
        }


        public static string CreateUIProjectPath(string UIProjectPath, SelectFileInfo objSelectFileInfo)
        {
            string dirPath = "";
            if (File.Exists(UIProjectPath))
            {
                dirPath = UIProjectPath;
            }
            else
            {
                DirectoryInfo di = new DirectoryInfo(string.Format(@"{0}\" + UIProjectPath, objSelectFileInfo.ProjectPath.Replace(objSelectFileInfo.ProjectName, "")));
                dirPath = di.FullName;
            }
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            return dirPath;
        }

    }
}
