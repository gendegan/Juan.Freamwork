using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace JuanGenerate
{
    public class CodeConfigHelper
    {




        public string _CodeConfigPath = "";
        XmlDocument _XmlDocument = null;
        /// <summary>
        /// 
        /// </summary>
        public XmlDocument CodeConfig
        {
            get
            {
                return _XmlDocument;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objConnectionNode"></param>
        /// <returns></returns>
        public string GetConnectionString(XmlNode objConnectionNode)
        {
            StringBuilder connectionString = new StringBuilder();
            connectionString.Append("SERVER=" + objConnectionNode.Attributes["ServerIP"].Value + ";");
            connectionString.Append("Port=" + objConnectionNode.Attributes["Port"].Value + ";");
            connectionString.Append("user id=" + objConnectionNode.Attributes["Uid"].Value + ";");
            connectionString.Append("password=" + objConnectionNode.Attributes["Pwd"].Value + ";");
            connectionString.Append("Database=" + objConnectionNode.Attributes["Database"].Value + ";");
            connectionString.Append("persist security info=True;Allow User Variables=True;Charset=utf8; ");
            return connectionString.ToString();
        }
        public bool IsExistCodeConfig()
        {
            return File.Exists(_CodeConfigPath);
        }



        /// <summary>
        /// 获取所有业务逻辑
        /// </summary>
        /// <returns></returns>
        public List<BusinessNodeInfo> GetUpgradeBusiness()
        {
            List<BusinessNodeInfo> objDataEntityInfoList = new List<BusinessNodeInfo>();
            foreach (XmlNode objXmlNode in _XmlDocument.SelectNodes("//CodeConfig/BusinessConfig/Business"))
            {
                BusinessNodeInfo objDataEntityInfo = new BusinessNodeInfo()
                {
                    TableName = objXmlNode.ReadAttribute("TableName"),
                    ConnectionKeyOrConnectionString = objXmlNode.ReadAttribute("ConnectionKeyOrConnectionString"),
                    EntityName = objXmlNode.ReadAttribute("EntityName"),
                    ConnectionKey = objXmlNode.ReadAttribute("ConnectionKey"),
                    UIProjectName = objXmlNode.ReadAttribute("UIProjectName"),
                    UIProjectPath = objXmlNode.ReadAttribute("UIProjectPath"),
                    ModuleID = objXmlNode.ReadAttribute("ModuleID"),
                    EditUrl = objXmlNode.ReadAttribute("EditUrl"),
                    ListUrl = objXmlNode.ReadAttribute("ListUrl"),
                    Description = objXmlNode.ReadAttribute("Description")
                };
                objDataEntityInfoList.Add(objDataEntityInfo);
            }
            return objDataEntityInfoList;
        }


        public void UpgradeConfig(GenerateHelper objGenerateHelper)
        {




            XmlNodeList objXmlNodeList = CodeConfig.SelectNodes("//ConnectionStrings/ConnectionString");
            foreach (XmlNode objXmlNode in objXmlNodeList)
            {

                XmlElement objConnectXmlElement = (XmlElement)objGenerateHelper.GetConnectionStringNode(objXmlNode.Attributes["Name"].Value, true);
                objConnectXmlElement.SetAttribute("Name", objXmlNode.Attributes["Name"].Value);
                objConnectXmlElement.SetAttribute("ServerIP", objXmlNode.Attributes["ServerIP"].Value);
                objConnectXmlElement.SetAttribute("Uid", objXmlNode.Attributes["Uid"].Value);
                objConnectXmlElement.SetAttribute("Pwd", objXmlNode.Attributes["Pwd"].Value);
                objConnectXmlElement.SetAttribute("Port", objXmlNode.Attributes["Port"].Value);
                objConnectXmlElement.SetAttribute("Database", objXmlNode.Attributes["Database"].Value);
                objConnectXmlElement.SetAttribute("DbType", "MySql");
            }

            List<BusinessNodeInfo> objBusinessNodeInfoList = GetUpgradeBusiness();
            List<TableSchema> objTableSchemaList = new List<TableSchema>();
            foreach (BusinessNodeInfo objBusinessNodeInfo in objBusinessNodeInfoList)
            {

                string TableNamePri = objBusinessNodeInfo.TableName.Replace("_tb", "");
                TableSchema obTableSchema = new TableSchema()
                {
                    TableName = objBusinessNodeInfo.TableName,
                    ViewName = TableNamePri + "_vw",
                    ViewFormat = TableNamePri + "_{0}_vw",
                    TableFormat = TableNamePri + "_{0}_tb",
                    Description = objBusinessNodeInfo.Description,
                    EntityName = objBusinessNodeInfo.EntityName,
                    ModuleID = objBusinessNodeInfo.ModuleID,
                    UIProjectPath = objBusinessNodeInfo.UIProjectPath,
                    UIProjectName = objBusinessNodeInfo.UIProjectPath,
                    ListUrl = objBusinessNodeInfo.ListUrl,
                    EditUrl = objBusinessNodeInfo.EditUrl,
                    ConnectionKey = objBusinessNodeInfo.ConnectionKey,
                    ContextKey = objBusinessNodeInfo.ConnectionKeyOrConnectionString.Replace(".ConnectionString", "")


                };
                objTableSchemaList.Add(obTableSchema);
            }
            objGenerateHelper.UpgradeXml(objTableSchemaList);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public CodeConfigHelper(string path)
        {
            _CodeConfigPath = path;
        }
        /// <summary>
        /// 获取连接串配置
        /// </summary>
        /// <param name="ConnectionStringName"></param>
        /// <param name="NoExistsAdd"></param>
        /// <returns></returns>
        public XmlNode GetConnectionStringNode(string ConnectionStringName, bool NoExistsAdd = false)
        {

            XmlNode objCodeConfig = _XmlDocument.SelectSingleNode("//CodeConfig");
            if (objCodeConfig == null)
            {
                objCodeConfig = _XmlDocument.CreateElement("CodeConfig");
                _XmlDocument.AppendChild(objCodeConfig);
            }
            XmlNode objConnectionStrings = _XmlDocument.SelectSingleNode("//CodeConfig/ConnectionStrings");

            if (objConnectionStrings == null)
            {
                objConnectionStrings = _XmlDocument.CreateElement("ConnectionStrings");
                objCodeConfig.AppendChild(objConnectionStrings);
            }

            XmlNode objConnectionString = _XmlDocument.SelectSingleNode("//ConnectionStrings/ConnectionString[@Name='" + ConnectionStringName + "']");

            if (objConnectionString == null && NoExistsAdd)
            {
                objConnectionString = _XmlDocument.CreateElement("ConnectionString");
                objConnectionStrings.AppendChild(objConnectionString);
            }
            return objConnectionString;

        }




        /// <summary>
        /// 加载配置文件
        /// </summary>
        /// <returns></returns>
        public bool LoadCodeConfigXml()
        {

            if (File.Exists(_CodeConfigPath))
            {
                _XmlDocument = new XmlDocument();
                try
                {
                    _XmlDocument.Load(_CodeConfigPath);
                }
                catch (Exception objExp)
                {
                    MessageBox.Show("不好意思，你的CodeConfig.xml加载异常，请检查配置文件" + objExp.ToString());
                    return false;
                }
                return true;

            }
            else
            {
                _XmlDocument = new XmlDocument();
                return true;
            }
        }



    }
}
