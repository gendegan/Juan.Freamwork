using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace JuanGenerate
{
    public static class SchemaHelper
    {


        public static DataTable GetSchemata(string connectionString)
        {

            return MySqlHelper.ExecuteDataTable(connectionString, " SELECT * FROM INFORMATION_SCHEMA.SCHEMATA");
        }


        public static MySqlParameter[] CreateParameter(string tableSchema, string tableName)
        {
            MySqlParameter[] prams = new MySqlParameter[2];
            prams[0] = new MySqlParameter("?TableName", tableName);
            prams[1] = new MySqlParameter("?TableSchema", tableSchema);
            return prams;
        }

        public static List<string> GetTableForeignFileName(string tableSchema, string tableName, string connectionString)
        {

            DataTable objDataTable = MySqlHelper.ExecuteDataTable(connectionString, MySqlResource.GetTableForeign, CreateParameter(tableSchema, tableName));
            return objDataTable.Select().Select(cols => (string)cols["ForeignColumnName"]).ToList();

        }

        public static DataTable GetTableColumns(string tableSchema, string tableName, string connectionString = "")
        {

            return MySqlHelper.ExecuteDataTable(connectionString, MySqlResource.GetTableColumns, CreateParameter(tableSchema, tableName));
        }




        public static List<TableSchema> GetTables(string tableSchema, string connectionString, bool IsTable = true)
        {

            MySqlParameter[] prams = new MySqlParameter[2];
            prams[0] = new MySqlParameter("?TableType", IsTable ? "BASE TABLE" : "VIEW");
            prams[1] = new MySqlParameter("?TableSchema", tableSchema);
            DataTable objDataTable = MySqlHelper.ExecuteDataTable(connectionString, MySqlResource.GetTables, prams);
            List<TableSchema> objTableSchemaList = new List<TableSchema>();
            foreach (DataRow objDataRow in objDataTable.Rows)
            {
                string TableName = objDataRow["TableName"].ToString();
                string EntityName = TableName.Replace("_tb", "");
                string Description = Regex.Replace(objDataRow["Description"].ToString(), @"[A-Za-z0-9_]", "", RegexOptions.IgnoreCase);
                string TableFormat = EntityName + "_{0}_tb";
                string ViewFormat = EntityName + "_{0}_vw";
                string ViewName = EntityName + "_vw";
                string[] EntityNameArray = EntityName.Split('_');
                if (EntityNameArray.Length == 1)
                {
                    EntityName = EntityNameArray[0];
                }
                else
                {
                    EntityName = EntityNameArray[1];
                }
                EntityName = EntityName.PascalCase();
                objTableSchemaList.Add(new TableSchema() { TableName = TableName, ViewName = ViewName, ViewFormat = ViewFormat, TableFormat = TableFormat, Description = Description, EntityName = EntityName });
            }
            return objTableSchemaList;
        }

        public static string GetControlType(string DataType)
        {

            string ControlType = "";
            switch (DataType)
            {
                case "int":
                    ControlType = "DropDown";
                    break;
                case "bigint":
                    ControlType = "DropDown";
                    break;
                case "char":
                    ControlType = "TextBox";
                    break;
                case "varchar":
                    ControlType = "TextBox";
                    break;
                case "nvarchar":
                    ControlType = "TextBox";
                    break;
                case "longtext":
                    ControlType = "Xhtml";
                    break;
                case "text":
                    ControlType = "Xhtml";
                    break;
                case "bit":
                    ControlType = "CheckBox";
                    break;
                case "datetime":
                    ControlType = "TextDateTime";
                    break;
                case "date":
                    ControlType = "TextDate";
                    break;
                default:
                    ControlType = "TextBox";
                    break;


            }
            return ControlType;
        }

        public static List<ColumnSchema> GetTableColumnsSchema(TableSchema objTableSchema, string connectionstring, string schemaName, string tableName, GenerateHelper objCodeConfigHelper)
        {
            DataTable objTableColumns = new DataTable("TableColumns");

            List<string> foreignKeyList = GetTableForeignFileName(schemaName, tableName, connectionstring);
            List<ColumnSchema> objColumnSchemaList = new List<ColumnSchema>();
            foreach (DataRow objDataRow in GetTableColumns(schemaName, tableName, connectionstring).Rows)
            {
                bool isCheck = false;
                bool isXmlField = false;
                string DbIgnore = "No";
                string errorMessage = "请输入" + objDataRow["Description"];
                System.Xml.XmlNode objField = objCodeConfigHelper.GenerateConfig.SelectSingleNode("//ContextConfig/Context[@TableName='" + tableName + "']/Field[@FieldName='" + objDataRow["FieldName"].ToString() + "']");
                if (objField == null)
                {
                    isCheck = false;
                }
                else
                {
                    DbIgnore = objField.ReadAttribute("DbIgnore", "No");
                    isCheck = objField.ReadAttributeBool("IsCheck", false);
                    errorMessage = objField.ReadAttribute("ErrorMessage");
                    isXmlField = true;
                }
                string FieldTitle = objField == null ? objDataRow["Description"].ToString() : objField.Attributes["FieldTitle"].Value;
                objColumnSchemaList.Add(new ColumnSchema(objTableSchema)
                {
                    IsXmlField = isXmlField,
                    FieldName = objDataRow["FieldName"].ToString(),
                    Description = objDataRow["Description"].ToString(),
                    DataType = objDataRow["DataType"].ToString(),
                    IsIdentity = objDataRow["Identity"].ToString() == "1",
                    IsUnsigned = objDataRow["IsUnsigned"].ToString() == "1",
                    Length = long.Parse(objDataRow["Length"].ToString()),
                    ColumnType = objDataRow["IsKey"].ToString() == "1" ? "PrimaryKey" : foreignKeyList.Contains(objDataRow["FieldName"].ToString()) ? "ForeignKey" : "Common",
                    IsEmpty = objDataRow["IsNullable"].ToString() == "1",
                    DbIgnore = DbIgnore,
                    FieldTitle = FieldTitle,
                    IsCheck = isCheck,
                    ErrorMessage = errorMessage,
                    OldErrorMessage = errorMessage,
                    OldFieldTitle = FieldTitle,
                    OldDbIgnore = DbIgnore
                });

            }
            return objColumnSchemaList;
        }




        //public static TableEditMvcSchema GetTableEditMvcSchema(string schemaName, string tableName, string ConnectionString, GenerateHelper objGenerateHelper, SelectFileInfo objSelectFileInfo)
        //{
        //    TableEditMvcSchema objTableEditSchema = new TableEditMvcSchema();
        //    objTableEditSchema.TableName = tableName;
        //    DataTable objTableColumns = new DataTable("TableColumns");
        //    List<string> foreignKeyList = GetTableForeignFileName(schemaName, tableName, ConnectionString);
        //    foreach (DataRow objDataRow in GetTableColumns(schemaName, tableName, ConnectionString).Rows)
        //    {
        //        System.Xml.XmlNode objField = objGenerateHelper.GenerateConfig.SelectSingleNode("//ContextConfig/Context[@TableName='" + tableName + "']/Field[@FieldName='" + objDataRow["FieldName"].ToString() + "']");
        //        bool isNullField = objField == null;
        //        objTableEditSchema.Columns.Add(new ColumnEditSchema(objTableEditSchema)
        //        {
        //            FieldName = objDataRow["FieldName"].ToString(),
        //            FieldTitle = isNullField ? objDataRow["Description"].ToString() : objField.Attributes["FieldTitle"].Value,
        //            DataType = objDataRow["DataType"].ToString(),
        //            IsIdentity = objDataRow["Identity"].ToString() == "1",
        //            IsUnsigned = objDataRow["IsUnsigned"].ToString() == "1",
        //            Length = long.Parse(objDataRow["Length"].ToString()),
        //            IsEmpty = objDataRow["IsNullable"].ToString() == "1",
        //            ColumnType = objDataRow["IsKey"].ToString() == "1" ? "PrimaryKey" : foreignKeyList.Contains(objDataRow["FieldName"].ToString()) ? "ForeignKey" : "Common",
        //            IsShow = false,
        //            ControlType = GetControlType(objDataRow["DataType"].ToString()),
        //            ErrorMessage = isNullField ? ((objDataRow["DataType"].ToString() == "int" || objDataRow["DataType"].ToString() == "bit" ? "请选择" : "请输入") + objDataRow["Description"]) : objField.Attributes["ErrorMessage"].Value,
        //            ValidationReg = ""
        //        });

        //    }

        //    return objTableEditSchema;
        //}
        //public static TableEditSchema GetTableEditSchema(string schemaName, string tableName, string ConnectionString, GenerateHelper objGenerateHelper, SelectFileInfo objSelectFileInfo)
        //{
        //    TableEditSchema objTableEditSchema = new TableEditSchema();
        //    objTableEditSchema.TableName = tableName;
        //    DataTable objTableColumns = new DataTable("TableColumns");
        //    List<string> foreignKeyList = GetTableForeignFileName(schemaName, tableName, ConnectionString);
        //    string[] AccountFields = SchemaHelper.GetFieldAttributes("AccountField").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        //    string[] AddFields = SchemaHelper.GetFieldAttributes("AddField").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        //    string[] UpdateFields = SchemaHelper.GetFieldAttributes("UpdateField").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        //    string[] EditNoShows = SchemaHelper.GetFieldAttributes("EditNoShow").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);


        //    List<string> objFieldNameList = SchemaHelper.GetFieldNameList();
        //    foreach (DataRow objDataRow in GetTableColumns(schemaName, tableName, ConnectionString).Rows)
        //    {
        //        System.Xml.XmlNode objField = objGenerateHelper.GenerateConfig.SelectSingleNode("//ContextConfig/Context[@TableName='" + tableName + "']/Field[@FieldName='" + objDataRow["FieldName"].ToString() + "']");
        //        bool isNullField = objField == null;

        //        string defaultValue = "";
        //        if (objDataRow["DataType"].ToString().Contains("date"))
        //        {
        //            defaultValue = "DateTime.Now";
        //        }
        //        if (AccountFields.Contains(objDataRow["FieldName"].ToString()))
        //        {
        //            defaultValue = "CurrentUser.ID";
        //        }

        //        string controlType = GetControlType(objDataRow["DataType"].ToString());


        //        if (objFieldNameList.Contains(objDataRow["FieldName"].ToString()))
        //        {
        //            controlType = objDataRow["FieldName"].ToString() + "模板";

        //        }
        //        string defaultShow = "新增和修改";
        //        if (AddFields.Contains(objDataRow["FieldName"].ToString()))
        //        {
        //            defaultShow = "新增";
        //        }
        //        if (UpdateFields.Contains(objDataRow["FieldName"].ToString()))
        //        {
        //            defaultShow = "修改";
        //        }
        //        bool isShow = true;
        //        if (EditNoShows.Contains(objDataRow["FieldName"].ToString()) || objDataRow["IsKey"].ToString() == "1")
        //        {
        //            isShow = false;
        //        }
        //        objTableEditSchema.Columns.Add(new ColumnEditSchema(objTableEditSchema)
        //        {
        //            FieldName = objDataRow["FieldName"].ToString(),
        //            FieldTitle = isNullField ? objDataRow["Description"].ToString() : objField.Attributes["FieldTitle"].Value,
        //            DataType = objDataRow["DataType"].ToString(),
        //            IsIdentity = objDataRow["Identity"].ToString() == "1",
        //            IsUnsigned = objDataRow["IsUnsigned"].ToString() == "1",
        //            Length = long.Parse(objDataRow["Length"].ToString()),
        //            IsEmpty = objDataRow["IsNullable"].ToString() == "1",
        //            ColumnType = objDataRow["IsKey"].ToString() == "1" ? "PrimaryKey" : foreignKeyList.Contains(objDataRow["FieldName"].ToString()) ? "ForeignKey" : "Common",
        //            IsShow = isShow,
        //            ControlType = controlType,
        //            ErrorMessage = isNullField ? ((objDataRow["DataType"].ToString() == "int" || objDataRow["DataType"].ToString() == "bit" ? "请选择" : "请输入") + objDataRow["Description"]) : objField.Attributes["ErrorMessage"].Value,
        //            ValidationReg = "",
        //            EditDefaultValue = defaultValue,
        //            DefaultShow = defaultShow
        //        });
        //    }
        //    return objTableEditSchema;
        //}

        //public static TableViewSchema GetTableViewSchema(string schemaName, string tableName, string ConnectionString, GenerateHelper objGenerateHelper, SelectFileInfo objSelectFileInfo)
        //{
        //    TableViewSchema objTableEditSchema = new TableViewSchema();
        //    objTableEditSchema.TableName = tableName;
        //    DataTable objTableColumns = new DataTable("TableColumns");
        //    List<string> foreignKeyList = GetTableForeignFileName(schemaName, tableName, ConnectionString);
        //    foreach (DataRow objDataRow in GetTableColumns(schemaName, tableName, ConnectionString).Rows)
        //    {
        //        System.Xml.XmlNode objField = objGenerateHelper.GenerateConfig.SelectSingleNode("//ContextConfig/Context[@TableName='" + tableName + "']/Field[@FieldName='" + objDataRow["FieldName"].ToString() + "']");
        //        bool isNullField = objField == null;
        //        objTableEditSchema.Columns.Add(new ColumnViewSchema(objTableEditSchema)
        //        {
        //            FieldName = objDataRow["FieldName"].ToString(),
        //            FieldTitle = isNullField ? objDataRow["Description"].ToString() : objField.Attributes["FieldTitle"].Value,
        //            DataType = objDataRow["DataType"].ToString(),
        //            IsIdentity = objDataRow["Identity"].ToString() == "1",
        //            IsUnsigned = objDataRow["IsUnsigned"].ToString() == "1",
        //            Length = long.Parse(objDataRow["Length"].ToString()),
        //            IsEmpty = objDataRow["IsNullable"].ToString() == "1",
        //            ColumnType = objDataRow["IsKey"].ToString() == "1" ? "PrimaryKey" : foreignKeyList.Contains(objDataRow["FieldName"].ToString()) ? "ForeignKey" : "Common",

        //            IsShow = false,
        //            ErrorMessage = isNullField ? ((objDataRow["DataType"].ToString() == "int" || objDataRow["DataType"].ToString() == "bit" ? "请选择" : "请输入") + objDataRow["Description"]) : objField.Attributes["ErrorMessage"].Value,
        //        });
        //    }

        //    return objTableEditSchema;
        //}

        //public static TableListSchema GetTableListSchema(string tableSchema, string tableName, string moduleID, string editUrl, string ConnectionString, GenerateHelper objGenerateHelper, SelectFileInfo objSelectFileInfo)
        //{

        //    TableListSchema objTableListSchema = new TableListSchema();
        //    objTableListSchema.TableName = tableName;
        //    DataTable objTableColumns = new DataTable("TableColumns");
        //    List<string> foreignKeyList = GetTableForeignFileName(tableSchema, tableName, ConnectionString);
        //    foreach (DataRow objDataRow in GetTableColumns(tableSchema, tableName, ConnectionString).Rows)
        //    {
        //        System.Xml.XmlNode objField = objGenerateHelper.GenerateConfig.SelectSingleNode("//ContextConfig/Context[@TableName='" + tableName + "']/Field[@FieldName='" + objDataRow["FieldName"].ToString() + "']");
        //        bool isNullField = objField == null;
        //        int Width = 0;
        //        if (objDataRow["DataType"].ToString().IndexOf("date") >= 0)
        //        {
        //            Width = 120;
        //        }
        //        else if (objDataRow["DataType"].ToString().IndexOf("int") >= 0)
        //        {
        //            Width = 80;
        //        }
        //        bool IsSearch = false;
        //        if (objDataRow["IsKey"].ToString() == "1")
        //        {
        //            IsSearch = true;
        //        }
        //        string SearchControlType = "QueryText";
        //        if (objDataRow["DataType"].ToString() == "int" && objDataRow["IsKey"].ToString() != "1")
        //        {
        //            SearchControlType = "QueryDrop";
        //        }
        //        else if (objDataRow["DataType"].ToString().IndexOf("date") >= 0)
        //        {
        //            SearchControlType = "QueryDate";
        //        }
        //        string ControlType = objDataRow["DataType"].ToString() == "int" || objDataRow["DataType"].ToString() == "bit" ? "TemplateField" : "BoundField";
        //        List<string> objFieldNameList = SchemaHelper.GetFieldNameList();
        //        if (objFieldNameList.Contains(objDataRow["FieldName"].ToString()))
        //        {
        //            ControlType = objDataRow["FieldName"].ToString() + "模板";
        //            SearchControlType = objDataRow["FieldName"].ToString() + "模板";
        //        }
        //        objTableListSchema.Columns.Add(new ColumnListSchema(objTableListSchema)
        //        {

        //            FieldName = objDataRow["FieldName"].ToString(),
        //            FieldTitle = isNullField ? objDataRow["Description"].ToString() : objField.Attributes["FieldTitle"].Value,
        //            DataType = objDataRow["DataType"].ToString(),
        //            IsIdentity = objDataRow["Identity"].ToString() == "1",
        //            IsUnsigned = objDataRow["IsUnsigned"].ToString() == "1",
        //            Length = long.Parse(objDataRow["Length"].ToString()),
        //            IsEmpty = objDataRow["IsNullable"].ToString() == "1",
        //            ColumnType = objDataRow["IsKey"].ToString() == "1" ? "PrimaryKey" : foreignKeyList.Contains(objDataRow["FieldName"].ToString()) ? "ForeignKey" : "Common",
        //            ControlType = ControlType,
        //            Width = Width,
        //            IsShow = false,
        //            IsSearch = IsSearch,
        //            SearchControlType = SearchControlType,
        //            IsSort = false,
        //            FormatString = objDataRow["DataType"].ToString().IndexOf("datetime") >= 0 ? "yyyy-MM-dd HH:mm:ss" : ""

        //        });

        //    }

        //    if (!string.IsNullOrWhiteSpace(moduleID))
        //    {
        //        foreach (DataRow objDataRow in MySqlHelper.ExecuteDataTable(objSelectFileInfo.GetPowerConnectionString(objGenerateHelper.SharePath), string.Format(Resources.GetCommand, moduleID)).Rows)
        //        {

        //            objTableListSchema.Commands.Add(new CommandSchema()
        //            {
        //                CommandName = objDataRow["CommandName"].ToString(),
        //                ProcessType = objDataRow["CommandName"].ToString().IndexOf("Modify") >= 0 || objDataRow["CommandName"].ToString().IndexOf("View") >= 0 ? "RedirectState" : objDataRow["CommandName"].ToString().IndexOf("Create") >= 0 || objDataRow["CommandName"].ToString().IndexOf("Back") >= 0 ? "Redirect" : "RenderPage",
        //                RedirectUrl = objDataRow["CommandName"].ToString().IndexOf("Modify") >= 0 || objDataRow["CommandName"].ToString().IndexOf("Create") >= 0 || objDataRow["CommandName"].ToString().IndexOf("View") >= 0 || objDataRow["CommandName"].ToString().IndexOf("Back") >= 0 ? editUrl : "",
        //                ModuleName = objDataRow["ModuleName"].ToString(),
        //                IsTop = objDataRow["PlaceType"].ToString().IndexOf("101") >= 0,
        //                IsBotom = objDataRow["PlaceType"].ToString().IndexOf("103") >= 0,
        //                IsList = objDataRow["PlaceType"].ToString().IndexOf("102") >= 0,
        //                SortIndex = int.Parse(objDataRow["SortIndex"].ToString())
        //            });
        //        }
        //    }

        //    return objTableListSchema;
        //}


        //public static TableListMvcSchema GetTableListMvcSchema(string tableSchema, string tableName, string ConnectionString, GenerateHelper objGenerateHelper, SelectFileInfo objSelectFileInfo)
        //{

        //    TableListMvcSchema objTableListSchema = new TableListMvcSchema();
        //    objTableListSchema.TableName = tableName;
        //    DataTable objTableColumns = new DataTable("TableColumns");
        //    List<string> foreignKeyList = GetTableForeignFileName(tableSchema, tableName, ConnectionString);
        //    foreach (DataRow objDataRow in GetTableColumns(tableSchema, tableName, ConnectionString).Rows)
        //    {
        //        System.Xml.XmlNode objField = objGenerateHelper.GenerateConfig.SelectSingleNode("//ContextConfig/Context[@TableName='" + tableName + "']/Field[@FieldName='" + objDataRow["FieldName"].ToString() + "']");
        //        bool isNullField = objField == null;
        //        objTableListSchema.Columns.Add(new ColumnListSchema(objTableListSchema)
        //        {

        //            FieldName = objDataRow["FieldName"].ToString(),
        //            FieldTitle = isNullField ? objDataRow["Description"].ToString() : objField.Attributes["FieldTitle"].Value,
        //            DataType = objDataRow["DataType"].ToString(),
        //            IsIdentity = objDataRow["Identity"].ToString() == "1",
        //            IsUnsigned = objDataRow["IsUnsigned"].ToString() == "1",
        //            Length = long.Parse(objDataRow["Length"].ToString()),
        //            IsEmpty = objDataRow["IsNullable"].ToString() == "1",
        //            ColumnType = objDataRow["IsKey"].ToString() == "1" ? "PrimaryKey" : foreignKeyList.Contains(objDataRow["FieldName"].ToString()) ? "ForeignKey" : "Common",

        //            ControlType = objDataRow["DataType"].ToString() == "int" || objDataRow["DataType"].ToString() == "bit" ? "TemplateField" : "BoundField",
        //            Width = objDataRow["DataType"].ToString().IndexOf("datetime") >= 0 ? 120 : 0,
        //            IsShow = false,
        //            IsSearch = false,
        //            IsSort = false,
        //            FormatString = objDataRow["DataType"].ToString().IndexOf("datetime") >= 0 ? "yyyy-MM-dd HH:mm:ss" : ""

        //        });

        //    }

        //    return objTableListSchema;
        //}

        public static string GetFieldSearch(string fileName)
        {
            return GetFieldConfig(fileName, "Search");
        }

        public static string GetFieldEdit(string fileName)
        {
            return GetFieldConfig(fileName, "Edit");
        }
        public static string GetFieldList(string fileName)
        {
            return GetFieldConfig(fileName, "List");
        }

        private static string _FieldConfigPath = @"C:\JuanGenerate\FieldConfig.xml";
        public static string GetFieldAttributes(string fileName)
        {

            if (_GenerateFieldDocument == null && !File.Exists(_FieldConfigPath))
            {
                return "";
            }
            if (_GenerateFieldDocument == null)
            {
                _GenerateFieldDocument = new XmlDocument();
                _GenerateFieldDocument.Load(_FieldConfigPath);
            }

            XmlAttribute objXmlAttribute = _GenerateFieldDocument.DocumentElement.Attributes[fileName];
            if (objXmlAttribute != null)
            {
                return objXmlAttribute.Value;
            }
            return "";
        }
        static List<string> _FieldNameList = null;
        public static List<string> GetFieldNameList()
        {
            if (_FieldNameList != null)
            {
                return _FieldNameList;
            }
            _FieldNameList = new List<string>();

            if (_GenerateFieldDocument == null && !File.Exists(_FieldConfigPath))
            {
                return _FieldNameList;
            }
            if (_GenerateFieldDocument == null)
            {
                _GenerateFieldDocument = new XmlDocument();
                _GenerateFieldDocument.Load(_FieldConfigPath);
            }

            foreach (XmlNode objXmlNode in _GenerateFieldDocument.DocumentElement.ChildNodes)
            {
                _FieldNameList.Add(objXmlNode.Name);
            }
            return _FieldNameList;
        }
        static XmlDocument _GenerateFieldDocument = null;
        public static string GetFieldConfig(string fileName, string type)
        {

            if (_GenerateFieldDocument == null && !File.Exists(_FieldConfigPath))
            {
                return "";
            }
            if (_GenerateFieldDocument == null)
            {
                _GenerateFieldDocument = new XmlDocument();
                _GenerateFieldDocument.Load(_FieldConfigPath);
            }
            XmlNode objXmlNode = _GenerateFieldDocument.SelectSingleNode("//" + fileName + "/" + type);
            if (objXmlNode == null)
            {
                return "";
            }
            return objXmlNode.InnerText;
        }
    }
}
