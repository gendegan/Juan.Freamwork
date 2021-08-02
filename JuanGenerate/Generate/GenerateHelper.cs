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
    public class GenerateHelper
    {




        string _GenerateConfigPath = "";
        public string GenerateConfigPath
        {
            get
            {
                return _GenerateConfigPath;
            }

        }
        XmlDocument _XmlDocument = null;
        /// <summary>
        /// 
        /// </summary>
        public XmlDocument GenerateConfig
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ConnectionKey"></param>
        /// <param name="Database"></param>
        /// <returns></returns>
        public string GetConnectionString(string ConnectionKey, out string Database)
        {
            Database = "";
            XmlNode objConnectionNode = GenerateConfig.SelectSingleNode("//ConnectionStrings/ConnectionString[@Name='" + ConnectionKey + "']");
            if (objConnectionNode == null)
            {

                return "";
            }

            Database = objConnectionNode.ReadAttribute("Database");
            return GetConnectionString(objConnectionNode);
        }
        /// <summary>
        /// 保存
        /// </summary>
        public void Save()
        {
            _XmlDocument.Save(_GenerateConfigPath);
        }

        /// <summary>
        /// 初始化链接串
        /// </summary>
        /// <param name="objComboBox"></param>
        public void InitConnectionStrings(ComboBox objComboBox)
        {
            objComboBox.Items.Clear();
            XmlNodeList objXmlNodeList = GenerateConfig.SelectNodes("//ConnectionStrings/ConnectionString");
            foreach (XmlNode objXmlNode in objXmlNodeList)
            {
                string Name = objXmlNode.ReadAttribute("Name");
                objComboBox.Items.Add(Name);
                objComboBox.SelectedItem = Name;
            }
        }


        /// <summary>
        /// 获取所有业务逻辑
        /// </summary>
        /// <returns></returns>
        public List<ContextInfo> GetContext()
        {
            List<ContextInfo> objDataEntityInfoList = new List<ContextInfo>();
            foreach (XmlNode objXmlNode in _XmlDocument.SelectNodes("//GenerateConfig/ContextConfig/Context"))
            {
                ContextInfo objDataEntityInfo = new ContextInfo()
                {
                    TableName = objXmlNode.ReadAttribute("TableName"),
                    TableFormat = objXmlNode.ReadAttribute("TableFormat"),
                    ViewFormat = objXmlNode.ReadAttribute("ViewFormat"),
                    ViewName = objXmlNode.ReadAttribute("ViewName"),
                    EntityName = objXmlNode.ReadAttribute("EntityName"),
                    ContextKey = objXmlNode.ReadAttribute("ContextKey"),

                    ConnectionKey = objXmlNode.ReadAttribute("ConnectionKey"),
                    UIProjectName = objXmlNode.ReadAttribute("UIProjectName"),
                    UIProjectPath = objXmlNode.ReadAttribute("UIProjectPath"),
                    ModuleID = objXmlNode.ReadAttribute("ModuleID"),
                    EditUrl = objXmlNode.ReadAttribute("EditUrl"),
                    ListUrl = objXmlNode.ReadAttribute("ListUrl"),
                    Description = objXmlNode.ReadAttribute("Description"),




                };
                objDataEntityInfoList.Add(objDataEntityInfo);
            }
            return objDataEntityInfoList;
        }

        public void GetContextInfoUIPath(ContextInfo objContextInfo, out string UIProjectPath, out string UIProjectName)
        {
            UIProjectPath = @"Gao7.ManaeWeb\Manage\XXXX";
            UIProjectName = "Gao7.ManaeWeb";
            if (!string.IsNullOrWhiteSpace(objContextInfo.UIProjectPath) || !string.IsNullOrWhiteSpace(objContextInfo.UIProjectName))
            {
                UIProjectPath = objContextInfo.UIProjectPath;
                UIProjectName = objContextInfo.UIProjectName;
                return;
            }
            ContextInfo objContextInfoValue = GetContext().Where(s => s.UIProjectPath != "" && s.UIProjectName != "").FirstOrDefault();
            if (objContextInfoValue != null)
            {
                UIProjectPath = objContextInfoValue.UIProjectPath;
                UIProjectName = objContextInfoValue.UIProjectName;
            }
            return;

        }


        public string GetContextKey(string ConnectionKey)
        {

            XmlNode objConnectionNode = GenerateConfig.SelectSingleNode("//ConnectionStrings/ConnectionString[@Name='" + ConnectionKey + "']");
            if (objConnectionNode == null)
            {
                return "";
            }
            return objConnectionNode.ReadAttribute("ContextKey");
        }


        /// <summary>
        /// 获取具体业务
        /// </summary>
        /// <param name="EntityName"></param>
        /// <returns></returns>
        public ContextInfo GetContext(string EntityName)
        {

            XmlNode objXmlNode = GenerateConfig.SelectSingleNode("//ContextConfig/Context[@EntityName='" + EntityName + "']");
            if (objXmlNode == null)
            {
                return null;
            }
            ContextInfo objContextInfo = new ContextInfo()
            {
                TableName = objXmlNode.ReadAttribute("TableName"),
                ContextKey = objXmlNode.ReadAttribute("ContextKey"),
                EntityName = objXmlNode.ReadAttribute("EntityName"),
                ConnectionKey = objXmlNode.ReadAttribute("ConnectionKey"),
                UIProjectName = objXmlNode.ReadAttribute("UIProjectName"),
                UIProjectPath = objXmlNode.ReadAttribute("UIProjectPath"),
                ModuleID = objXmlNode.ReadAttribute("ModuleID"),
                EditUrl = objXmlNode.ReadAttribute("EditUrl"),
                ListUrl = objXmlNode.ReadAttribute("ListUrl"),
                Description = objXmlNode.ReadAttribute("Description"),
                ViewName = objXmlNode.ReadAttribute("ViewName"),
                ViewFormat = objXmlNode.ReadAttribute("ViewFormat"),
                TableFormat = objXmlNode.ReadAttribute("TableFormat"),

            };
            return objContextInfo;
        }


        /// <summary>
        /// 业务逻辑数据转表格
        /// </summary>
        /// <param name="objContextInfo"></param>
        /// <param name="objTableRuleSchema"></param>
        public void ContextNodeToTableSchema(ContextInfo objContextInfo, TableSchema objTableSchema)
        {
            if (objContextInfo == null || objTableSchema == null)
            {
                return;
            }
            objTableSchema.ContextKey = objContextInfo.ContextKey;
            objTableSchema.IsGenerateCode = true;
            objTableSchema.EntityName = objContextInfo.EntityName;
            objTableSchema.ConnectionKey = objContextInfo.ConnectionKey;
            objTableSchema.UIProjectPath = objContextInfo.UIProjectPath;
            objTableSchema.UIProjectName = objContextInfo.UIProjectName;
            objTableSchema.EditUrl = objContextInfo.EditUrl;
            objTableSchema.ListUrl = objContextInfo.ListUrl;
            objTableSchema.ModuleID = objContextInfo.ModuleID;

            if (!string.IsNullOrWhiteSpace(objContextInfo.EntityName))
            {
                objTableSchema.EntityName = objContextInfo.EntityName;
            }
            if (!string.IsNullOrWhiteSpace(objContextInfo.Description))
            {
                objTableSchema.Description = objContextInfo.Description;
            }
            if (!string.IsNullOrWhiteSpace(objContextInfo.ViewFormat))
            {
                objTableSchema.ViewFormat = objContextInfo.ViewFormat;
            }
            if (!string.IsNullOrWhiteSpace(objContextInfo.TableFormat))
            {
                objTableSchema.TableFormat = objContextInfo.TableFormat;
            }

            if (!string.IsNullOrWhiteSpace(objContextInfo.ViewName))
            {
                objTableSchema.ViewName = objContextInfo.ViewName;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public GenerateHelper(string path)
        {
            _GenerateConfigPath = path;
        }
        /// <summary>
        /// 获取连接串配置
        /// </summary>
        /// <param name="ConnectionStringName"></param>
        /// <param name="NoExistsAdd"></param>
        /// <returns></returns>
        public XmlNode GetConnectionStringNode(string ConnectionStringName, bool NoExistsAdd = false)
        {

            XmlNode objGenerateXmlNode = _XmlDocument.SelectSingleNode("//GenerateConfig");
            if (objGenerateXmlNode == null)
            {
                objGenerateXmlNode = _XmlDocument.CreateElement("GenerateConfig");
                XmlElement objGeneratXmlElement = ((XmlElement)objGenerateXmlNode);
                objGeneratXmlElement.SetAttribute("SharePath", "");
                objGeneratXmlElement.SetAttribute("FrameworkName", "Juan");
                _XmlDocument.AppendChild(objGenerateXmlNode);
            }
            XmlNode objConnectionStrings = _XmlDocument.SelectSingleNode("//GenerateConfig/ConnectionStrings");

            if (objConnectionStrings == null)
            {
                objConnectionStrings = _XmlDocument.CreateElement("ConnectionStrings");
                objGenerateXmlNode.AppendChild(objConnectionStrings);
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
        /// 获取业务节点
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public XmlNode GetContextNode(string TableName)
        {
            XmlNode objXmlNode = GenerateConfig.SelectSingleNode("//ContextConfig/Context[@TableName='" + TableName + "']");
            if (objXmlNode != null)
            {
                return objXmlNode;
            }
            XmlNode objGenerateXmlNode = _XmlDocument.SelectSingleNode("//GenerateConfig");
            if (objGenerateXmlNode == null)
            {
                objGenerateXmlNode = _XmlDocument.CreateElement("GenerateConfig");

                XmlElement objGeneratXmlElement = ((XmlElement)objGenerateXmlNode);
                objGeneratXmlElement.SetAttribute("SharePath", "");
                objGeneratXmlElement.SetAttribute("FrameworkName", "Juan");
                _XmlDocument.AppendChild(objGenerateXmlNode);

            }
            XmlNode objContextConfig = _XmlDocument.SelectSingleNode("//GenerateConfig/ContextConfig");

            if (objContextConfig == null)
            {
                objContextConfig = _XmlDocument.CreateElement("ContextConfig");
                objGenerateXmlNode.AppendChild(objContextConfig);
            }
            return null;

        }

        /// <summary>
        /// 
        /// </summary>
        public string SharePath
        {

            get
            {
                XmlNode objCodeConfig = _XmlDocument.SelectSingleNode("//GenerateConfig");
                if (objCodeConfig == null)
                {
                    return "";
                }
                return objCodeConfig.ReadAttribute("SharePath");
            }

        }

        public string FrameworkName
        {
            get
            {
                XmlNode objCodeConfig = _XmlDocument.SelectSingleNode("//GenerateConfig");
                if (objCodeConfig == null)
                {
                    return "Juan";
                }
                return objCodeConfig.ReadAttribute("FrameworkName", "Juan");
            }

        }
        /// <summary>
        /// 获取业务配置节点
        /// </summary>
        /// <returns></returns>
        public XmlNode GetContextConfigNode()
        {
            XmlNode objXmlNode = GenerateConfig.SelectSingleNode("//GenerateConfig/ContextConfig");
            if (objXmlNode != null)
            {
                return objXmlNode;
            }
            XmlNode objGenerateXmlNode = _XmlDocument.SelectSingleNode("//GenerateConfig");
            if (objGenerateXmlNode == null)
            {
                objGenerateXmlNode = _XmlDocument.CreateElement("GenerateConfig");
                XmlElement objGeneratXmlElement = ((XmlElement)objGenerateXmlNode);
                objGeneratXmlElement.SetAttribute("SharePath", "");
                objGeneratXmlElement.SetAttribute("FrameworkName", "Juan");
                _XmlDocument.AppendChild(objGenerateXmlNode);
            }
            XmlNode objContextConfig = _XmlDocument.SelectSingleNode("//GenerateConfig/ContextConfig");

            if (objContextConfig == null)
            {
                objContextConfig = _XmlDocument.CreateElement("ContextConfig");
                objGenerateXmlNode.AppendChild(objContextConfig);
            }
            return objContextConfig;

        }

        /// <summary>
        /// 加载配置文件
        /// </summary>
        /// <returns></returns>
        public bool LoadConfigXml()
        {

            if (File.Exists(_GenerateConfigPath))
            {
                _XmlDocument = new XmlDocument();
                try
                {
                    _XmlDocument.Load(_GenerateConfigPath);
                }
                catch (Exception objExp)
                {
                    MessageBox.Show("不好意思，你的GenerateConfig.xml加载异常，请检查配置文件" + objExp.ToString());
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

        public void UpdateXml(List<TableSchema> objTableSchemaList, string ConnectionKey)
        {
            XmlNode objContextConfigXml = GetContextConfigNode();
            foreach (TableSchema objTableSchema in objTableSchemaList)
            {
                XmlNode objXmlNode = GetContextNode(objTableSchema.TableName);
                if (objXmlNode == null)
                {

                    XmlElement objContext = GenerateConfig.CreateElement("Context");
                    objContext.SetAttribute("TableName", objTableSchema.TableName);
                    objContext.SetAttribute("TableFormat", objTableSchema.TableFormat);
                    objContext.SetAttribute("EntityName", objTableSchema.EntityName);
                    objContext.SetAttribute("ViewFormat", objTableSchema.ViewFormat);
                    objContext.SetAttribute("ViewName", objTableSchema.ViewName);

                    objContext.SetAttribute("Description", objTableSchema.Description);
                    objContext.SetAttribute("ConnectionKey", ConnectionKey);
                    objContext.SetAttribute("ContextKey", objTableSchema.ContextKey);

                    objContext.SetAttribute("UIProjectName", objTableSchema.UIProjectName);
                    objContext.SetAttribute("UIProjectPath", objTableSchema.UIProjectPath);
                    objContext.SetAttribute("EditUrl", objTableSchema.EditUrl);
                    objContext.SetAttribute("ListUrl", objTableSchema.ListUrl);
                    objContext.SetAttribute("ModuleID", objTableSchema.ModuleID);


                    foreach (ColumnSchema objColumnSchema in objTableSchema.Columns.Where(s => s.IsXmlField))
                    {
                        XmlElement objField = GenerateConfig.CreateElement("Field");
                        objField.SetAttribute("FieldName", objColumnSchema.FieldName);
                        objField.SetAttribute("FieldTitle", objColumnSchema.FieldTitle);
                        objField.SetAttribute("IsCheck", objColumnSchema.IsCheck.ToString());
                        objField.SetAttribute("ErrorMessage", objColumnSchema.ErrorMessage);
                        objField.SetAttribute("DbIgnore", objColumnSchema.DbIgnore);
                        objContext.AppendChild(objField);
                    }
                    objContextConfigXml.AppendChild(objContext);

                }
                else
                {

                    XmlElement objContext = (XmlElement)objXmlNode;
                    objContext.SetAttribute("TableFormat", objTableSchema.TableFormat);
                    objContext.SetAttribute("EntityName", objTableSchema.EntityName);
                    objContext.SetAttribute("ViewFormat", objTableSchema.ViewFormat);
                    objContext.SetAttribute("ViewName", objTableSchema.ViewName);
                    objContext.SetAttribute("Description", objTableSchema.Description);
                    objContext.SetAttribute("ConnectionKey", ConnectionKey);
                    objContext.SetAttribute("ContextKey", objTableSchema.ContextKey);

                    objContext.SetAttribute("UIProjectName", objTableSchema.UIProjectName);
                    objContext.SetAttribute("UIProjectPath", objTableSchema.UIProjectPath);
                    objContext.SetAttribute("EditUrl", objTableSchema.EditUrl);
                    objContext.SetAttribute("ListUrl", objTableSchema.ListUrl);
                    objContext.SetAttribute("ModuleID", objTableSchema.ModuleID);

                    foreach (ColumnSchema objColumnSchema in objTableSchema.Columns.Where(s => s.IsXmlField))
                    {
                        XmlNode objFieldXmlNode = GenerateConfig.SelectSingleNode("//ContextConfig/Context[@TableName='" + objTableSchema.TableName + "']/Field[@FieldName='" + objColumnSchema.FieldName + "']");
                        if (objFieldXmlNode == null)
                        {
                            XmlElement objField = GenerateConfig.CreateElement("Field");
                            objField.SetAttribute("FieldName", objColumnSchema.FieldName);
                            objField.SetAttribute("FieldTitle", objColumnSchema.FieldTitle);
                            objField.SetAttribute("IsCheck", objColumnSchema.IsCheck.ToString());
                            objField.SetAttribute("ErrorMessage", objColumnSchema.ErrorMessage);
                            objField.SetAttribute("DbIgnore", objColumnSchema.DbIgnore);
                            objContext.AppendChild(objField);
                        }
                        else
                        {
                            XmlElement objField = (XmlElement)objFieldXmlNode;
                            objField.SetAttribute("FieldTitle", objColumnSchema.FieldTitle);
                            objField.SetAttribute("IsCheck", objColumnSchema.IsCheck.ToString());
                            objField.SetAttribute("ErrorMessage", objColumnSchema.ErrorMessage);
                            objField.SetAttribute("DbIgnore", objColumnSchema.DbIgnore);
                        }
                    }
                }
            }
            Save();
        }


        public void UpgradeXml(List<TableSchema> objTableSchemaList)
        {
            XmlNode objContextConfigXml = GetContextConfigNode();
            foreach (TableSchema objTableSchema in objTableSchemaList)
            {
                XmlNode objXmlNode = GetContextNode(objTableSchema.TableName);
                if (objXmlNode == null)
                {

                    XmlElement objContext = GenerateConfig.CreateElement("Context");
                    objContext.SetAttribute("TableName", objTableSchema.TableName);
                    objContext.SetAttribute("TableFormat", objTableSchema.TableFormat);
                    objContext.SetAttribute("EntityName", objTableSchema.EntityName);
                    objContext.SetAttribute("ViewFormat", objTableSchema.ViewFormat);
                    objContext.SetAttribute("ViewName", objTableSchema.ViewName);

                    objContext.SetAttribute("Description", objTableSchema.Description);
                    objContext.SetAttribute("ConnectionKey", objTableSchema.ConnectionKey);
                    objContext.SetAttribute("ContextKey", objTableSchema.ContextKey);

                    objContext.SetAttribute("UIProjectName", objTableSchema.UIProjectName);
                    objContext.SetAttribute("UIProjectPath", objTableSchema.UIProjectPath);
                    objContext.SetAttribute("EditUrl", objTableSchema.EditUrl);
                    objContext.SetAttribute("ListUrl", objTableSchema.ListUrl);
                    objContext.SetAttribute("ModuleID", objTableSchema.ModuleID);


                    foreach (ColumnSchema objColumnSchema in objTableSchema.Columns.Where(s => s.IsXmlField))
                    {
                        XmlElement objField = GenerateConfig.CreateElement("Field");
                        objField.SetAttribute("FieldName", objColumnSchema.FieldName);
                        objField.SetAttribute("FieldTitle", objColumnSchema.FieldTitle);
                        objField.SetAttribute("IsCheck", objColumnSchema.IsCheck.ToString());
                        objField.SetAttribute("ErrorMessage", objColumnSchema.ErrorMessage);
                        objField.SetAttribute("DbIgnore", objColumnSchema.DbIgnore);
                        objContext.AppendChild(objField);
                    }
                    objContextConfigXml.AppendChild(objContext);

                }
                else
                {

                    XmlElement objContext = (XmlElement)objXmlNode;
                    objContext.SetAttribute("TableFormat", objTableSchema.TableFormat);
                    objContext.SetAttribute("EntityName", objTableSchema.EntityName);
                    objContext.SetAttribute("ViewFormat", objTableSchema.ViewFormat);
                    objContext.SetAttribute("ViewName", objTableSchema.ViewName);
                    objContext.SetAttribute("Description", objTableSchema.Description);
                    objContext.SetAttribute("ConnectionKey", objTableSchema.ConnectionKey);
                    objContext.SetAttribute("ContextKey", objTableSchema.ContextKey);

                    objContext.SetAttribute("UIProjectName", objTableSchema.UIProjectName);
                    objContext.SetAttribute("UIProjectPath", objTableSchema.UIProjectPath);
                    objContext.SetAttribute("EditUrl", objTableSchema.EditUrl);
                    objContext.SetAttribute("ListUrl", objTableSchema.ListUrl);
                    objContext.SetAttribute("ModuleID", objTableSchema.ModuleID);

                    foreach (ColumnSchema objColumnSchema in objTableSchema.Columns.Where(s => s.IsXmlField))
                    {
                        XmlNode objFieldXmlNode = GenerateConfig.SelectSingleNode("//ContextConfig/Context[@TableName='" + objTableSchema.TableName + "']/Field[@FieldName='" + objColumnSchema.FieldName + "']");
                        if (objFieldXmlNode == null)
                        {
                            XmlElement objField = GenerateConfig.CreateElement("Field");
                            objField.SetAttribute("FieldName", objColumnSchema.FieldName);
                            objField.SetAttribute("FieldTitle", objColumnSchema.FieldTitle);
                            objField.SetAttribute("IsCheck", objColumnSchema.IsCheck.ToString());
                            objField.SetAttribute("ErrorMessage", objColumnSchema.ErrorMessage);
                            objField.SetAttribute("DbIgnore", objColumnSchema.DbIgnore);
                            objContext.AppendChild(objField);
                        }
                        else
                        {
                            XmlElement objField = (XmlElement)objFieldXmlNode;
                            objField.SetAttribute("FieldTitle", objColumnSchema.FieldTitle);
                            objField.SetAttribute("IsCheck", objColumnSchema.IsCheck.ToString());
                            objField.SetAttribute("ErrorMessage", objColumnSchema.ErrorMessage);
                            objField.SetAttribute("DbIgnore", objColumnSchema.DbIgnore);
                        }
                    }
                }
            }

        }

    }
}
