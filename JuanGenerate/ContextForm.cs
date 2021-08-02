using EnvDTE;
using EnvDTE80;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace JuanGenerate
{
    public partial class ContextForm : Form
    {
        /// <summary>
        /// application对象
        /// </summary>



        GenerateHelper _GenerateHelper = null;
        SelectFileInfo _SelectFileInfo = null;

        bool _IsUpgrade = false;
        public ContextForm(SelectFileInfo objSelectFileInfo, bool isUpgrade = false)
        {
            InitializeComponent();
            _SelectFileInfo = objSelectFileInfo;
            boxIsCreate.Enabled = false;
            _IsUpgrade = isUpgrade;
            _GenerateHelper = new GenerateHelper(objSelectFileInfo.GeneratePath);
            objSelectFileInfo.CurrentGenerateHelper = _GenerateHelper;
            if (!_GenerateHelper.LoadConfigXml())
            {
                return;
            }



            _GenerateHelper.InitConnectionStrings(boxConnectString);
            LoadTableSchema(boxConnectString.SelectedItem.ToString());
        }


        List<TableSchema> _TableSchemaList = new List<TableSchema>();
        string _ConnectionString = string.Empty;
        string _Database = string.Empty;


        void LoadTableSchema(string ConnectionStringName)
        {
            if (string.IsNullOrWhiteSpace(ConnectionStringName))
            {
                return;
            }
            gvdTables.AutoGenerateColumns = false;
            txtContextKey.Text = _GenerateHelper.GetContextKey(ConnectionStringName);
            _ConnectionString = _GenerateHelper.GetConnectionString(ConnectionStringName, out _Database);
            try
            {
                _TableSchemaList = SchemaHelper.GetTables(_Database, _ConnectionString, !IsView.Checked);
                List<ContextInfo> objBusinessNodeInfoList = _GenerateHelper.GetContext();
                foreach (TableSchema objTableSchema in _TableSchemaList)
                {
                    ContextInfo objBusinessNodeInfo = objBusinessNodeInfoList.FirstOrDefault(s => s.TableName == objTableSchema.TableName);
                    if (objBusinessNodeInfo != null && _IsUpgrade)
                    {
                        objTableSchema.IsCreate = true;
                    }
                    _GenerateHelper.ContextNodeToTableSchema(objBusinessNodeInfo, objTableSchema);
                }
                if (_IsUpgrade)
                {
                    gvdTables.DataSource = _TableSchemaList.Where(s => s.IsGenerateCode == true).ToList();

                }
                else
                {
                    gvdTables.DataSource = _TableSchemaList;
                }
                boxIsCreate.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载表错误，异常信息：" + ex.ToString());
                return;
            }
        }


        private void gvdTables_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (gvdTables.Columns[e.ColumnIndex].Name == "SetCheck")
                {

                    string TableName = gvdTables.Rows[e.RowIndex].ReadCell("TableName");

                    TableSchema objTableSchema = _TableSchemaList.First(s => s.TableName == TableName);
                    if (objTableSchema.Columns.Count == 0)
                    {
                        objTableSchema.Columns = SchemaHelper.GetTableColumnsSchema(objTableSchema, _ConnectionString, _Database, TableName, _GenerateHelper);
                    }
                    ContextColumnForm objContextColumnForm = new ContextColumnForm(objTableSchema, _SelectFileInfo);
                    objContextColumnForm.Show();
                }
            }
            catch (Exception objExp)
            {
                MessageBox.Show(objExp.Message);

            }
        }


        private void gvwTableCols_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private List<TableSchema> ProcessSelectTable()
        {


            foreach (DataGridViewRow objDataRow in gvdTables.Rows)
            {
                string TableName = objDataRow.ReadCell("TableName");
                string ContextKey = objDataRow.ReadCell("ContextKey");

                ContextKey = string.IsNullOrWhiteSpace(ContextKey) ? txtContextKey.Text : ContextKey;

                TableSchema objTableSchema = _TableSchemaList.First(s => s.TableName == TableName);
                objTableSchema.IsCreate = objDataRow.ReadCellBool("IsCreate");
                objTableSchema.Description = objDataRow.ReadCell("Description");
                objTableSchema.EntityName = objDataRow.ReadCell("EntityName");
                objTableSchema.ViewName = objDataRow.ReadCell("ViewName");
                objTableSchema.TableFormat = objDataRow.ReadCell("TableFormat");
                objTableSchema.ViewFormat = objDataRow.ReadCell("ViewFormat");
                objTableSchema.ContextKey = ContextKey;
                if (objTableSchema.IsCreate && objTableSchema.Columns.Count == 0)
                {
                    objTableSchema.Columns = SchemaHelper.GetTableColumnsSchema(objTableSchema, _ConnectionString, _Database, TableName, _GenerateHelper);
                }
            }

            List<TableSchema> objTableSchemaList = _TableSchemaList.Where(s => s.IsCreate == true).ToList();
            if (objTableSchemaList.Count() == 0)
            {
                MessageBox.Show("请选择表");
                return null;
            }
            return objTableSchemaList;
        }
        private void btnEntity_Click(object sender, EventArgs e)
        {
            try
            {
                List<TableSchema> objTableSchemaList = ProcessSelectTable();
                if (objTableSchemaList == null)
                {
                    return;
                }
                CreateEntityProcess(objTableSchemaList);
                if (!chkPreviewCode.Checked)
                {
                    MessageBox.Show("生成成功");
                }
                _GenerateHelper.UpdateXml(objTableSchemaList, boxConnectString.SelectedItem.ToString());
            }
            catch (Exception objExp)
            {
                MessageBox.Show(objExp.Message);

            }


        }


        /// <summary>
        /// 生成业务逻辑层
        /// </summary>
        /// <param name="tableRuleSchemaList"></param>
        public void CreateContextProcess(List<TableSchema> objTableSchemaList)
        {

            string ContextFilePath = _SelectFileInfo.ProjectPath + @"\Context";
            if (!Directory.Exists(ContextFilePath))
            {
                _SelectFileInfo.Project.ProjectItems.AddFolder("Context");
            }
            foreach (TableSchema objTableSchema in objTableSchemaList)
            {
                if (objTableSchema.PrimaryKey == null)
                {
                    if (objTableSchema.PrimaryKey == null)
                    {
                        MessageBox.Show(objTableSchema.TableName + ":未设置主键因此无法生成");
                        continue;
                    }
                }

                string generateCode = objTableSchema.CreateGenerateContext(_SelectFileInfo);
                string codeGeneratePath = ContextFilePath + @"\" + objTableSchema.EntityName + "Context.Generate.cs";

                if (chkPreviewCode.Checked)
                {
                    CommonHelper.ShowCodeForm(generateCode);
                    continue;
                }
                if (!File.Exists(codeGeneratePath))
                {

                    string CodePartial = objTableSchema.CreateContext(_SelectFileInfo);
                    string codePath = ContextFilePath + @"\" + objTableSchema.EntityName + "Context.cs";
                    FileStream fs = File.Create(codePath);
                    fs.Close();
                    ProjectItem objProjectItem = _SelectFileInfo.Project.ProjectItems.AddFromFile(codePath);
                    File.WriteAllText(codePath, CodePartial, Encoding.UTF8);


                    fs = File.Create(codeGeneratePath);
                    fs.Close();

                    _SelectFileInfo.Project.ProjectItems.AddFromFile(codeGeneratePath);
                    File.WriteAllText(codeGeneratePath, generateCode, Encoding.UTF8);

                    objProjectItem.ProjectItems.AddFromFile(codeGeneratePath);

                }
                else
                {

                    File.WriteAllText(codeGeneratePath, generateCode, Encoding.UTF8);

                }
            }

        }


        private const string CordovaKind = "{262852C6-CD72-467D-83FE-5EEB1973A190}";

        /// <summary>
        /// 生成实体
        /// </summary>
        /// <param name="tableRuleSchemaList"></param>
        public void CreateEntityProcess(List<TableSchema> objTableSchemaList)
        {
            StringBuilder error = new StringBuilder();
            string entityFilePath = _SelectFileInfo.ProjectPath + @"\Entity";
            if (!Directory.Exists(entityFilePath))
            {
                _SelectFileInfo.Project.ProjectItems.AddFolder("Entity");
            }
            foreach (TableSchema objTableSchema in objTableSchemaList)
            {

                if (objTableSchema.PrimaryKey == null)
                {
                    string codeViewPath = entityFilePath + @"\" + objTableSchema.EntityName + ".cs";

                    string generateViewCode = objTableSchema.CreateGenerateViewEntity(_SelectFileInfo);
                    if (chkPreviewCode.Checked)
                    {
                        CommonHelper.ShowCodeForm(generateViewCode);
                        continue;
                    }
                    if (!File.Exists(codeViewPath))
                    {
                        FileStream fs = File.Create(codeViewPath);
                        fs.Close();
                        _SelectFileInfo.Project.ProjectItems.AddFromFile(codeViewPath);
                        File.WriteAllText(codeViewPath, generateViewCode, Encoding.UTF8);
                    }
                    File.WriteAllText(codeViewPath, generateViewCode, Encoding.UTF8);
                    continue;
                }
                string generateCode = objTableSchema.CreateGenerateEntity(_SelectFileInfo);

                string codeGeneratePath = entityFilePath + @"\" + objTableSchema.EntityName + ".Generate.cs";

                if (chkPreviewCode.Checked)
                {
                    CommonHelper.ShowCodeForm(generateCode);
                    continue;
                }
                if (!File.Exists(codeGeneratePath))
                {
                    string CodePartial = objTableSchema.CreateEntity(_SelectFileInfo);
                    string codePath = entityFilePath + @"\" + objTableSchema.EntityName + ".cs";
                    FileStream fs = File.Create(codePath);
                    fs.Close();
                    ProjectItem objProjectItem = _SelectFileInfo.Project.ProjectItems.AddFromFile(codePath);
                    File.WriteAllText(codePath, CodePartial, Encoding.UTF8);


                    fs = File.Create(codeGeneratePath);
                    fs.Close();
                    _SelectFileInfo.Project.ProjectItems.AddFromFile(codeGeneratePath);
                    File.WriteAllText(codeGeneratePath, generateCode, Encoding.UTF8);
                    objProjectItem.ProjectItems.AddFromFile(codeGeneratePath);

                }
                else
                {
                    File.WriteAllText(codeGeneratePath, generateCode, Encoding.UTF8);

                }
            }

        }



        private void btnSchema_Click(object sender, EventArgs e)
        {
            if (boxConnectString.SelectedItem == null)
            {

                MessageBox.Show("请选择连接串");
                return;
            }
            LoadTableSchema(boxConnectString.SelectedItem.ToString());
        }

        private void btnContext_Click(object sender, EventArgs e)
        {

            try
            {

                List<TableSchema> objTableSchemaList = ProcessSelectTable();
                if (objTableSchemaList == null)
                {
                    return;
                }

                CreateContextProcess(objTableSchemaList);
                if (!chkPreviewCode.Checked)
                {
                    MessageBox.Show("生成成功");
                }
                _GenerateHelper.UpdateXml(objTableSchemaList, boxConnectString.SelectedItem.ToString());
            }
            catch (Exception objExp)
            {
                MessageBox.Show(objExp.Message);

            }
        }

        private void btnCreaeAll_Click(object sender, EventArgs e)
        {
            try
            {

                List<TableSchema> objTableSchemaList = ProcessSelectTable();
                if (objTableSchemaList == null)
                {
                    return;
                }
                CreateEntityProcess(objTableSchemaList);
                CreateContextProcess(objTableSchemaList);
                if (!chkPreviewCode.Checked)
                {
                    MessageBox.Show("生成成功");
                }
                _GenerateHelper.UpdateXml(objTableSchemaList, boxConnectString.SelectedItem.ToString());

            }
            catch (Exception objExp)
            {
                MessageBox.Show(objExp.Message);

            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                gvdTables.DataSource = _TableSchemaList;
            }
            else
            {
                gvdTables.DataSource = _TableSchemaList.Where(s => s.TableName.Contains(txtSearch.Text)).ToList();
            }
        }

        private void btnValueSet_Click(object sender, EventArgs e)
        {
            try
            {
                List<TableSchema> objTableSchemaList = ProcessSelectTable();
                if (objTableSchemaList == null)
                {
                    return;
                }
                foreach (TableSchema objTableSchema in objTableSchemaList)
                {
                    string generateCode = objTableSchema.CreateEntityValue();
                    CommonHelper.ShowCodeForm(generateCode);
                    continue;

                }
            }
            catch (Exception objExp)
            {
                MessageBox.Show(objExp.Message);

            }

        }

        private void btnSolrEntity_Click(object sender, EventArgs e)
        {
            List<TableSchema> objTableSchemaList = ProcessSelectTable();
            if (objTableSchemaList == null)
            {
                return;
            }
            foreach (TableSchema objTableSchema in objTableSchemaList)
            {
                string generateCode = objTableSchema.CreateSolrEntity();
                CommonHelper.ShowCodeForm(generateCode);
                continue;
            }
        }

        private void btnSolrIndex_Click(object sender, EventArgs e)
        {
            List<TableSchema> objTableSchemaList = ProcessSelectTable();
            if (objTableSchemaList == null)
            {
                return;
            }
            foreach (TableSchema objTableSchema in objTableSchemaList)
            {
                string generateCode = objTableSchema.CreateSolrIndex();
                CommonHelper.ShowCodeForm(generateCode);
                continue;
            }
        }

        private void btnESMapping_Click(object sender, EventArgs e)
        {
            List<TableSchema> objTableSchemaList = ProcessSelectTable();
            if (objTableSchemaList == null)
            {
                return;
            }
            foreach (TableSchema objTableSchema in objTableSchemaList)
            {
                string generateCode = objTableSchema.CreateESMappingIndex();
                CommonHelper.ShowCodeForm(generateCode);
                continue;
            }
        }

        private void boxIsCreate_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (boxIsCreate.SelectedItem == null)
            {

                MessageBox.Show("请选择过滤条件");
                return;
            }
            string fiterValue = boxIsCreate.SelectedItem.ToString();
            if (fiterValue == "未生成")
            {
                gvdTables.DataSource = _TableSchemaList.Where(s => s.IsGenerateCode == false).ToList();
            }
            else if (fiterValue == "生成过")
            {
                gvdTables.DataSource = _TableSchemaList.Where(s => s.IsGenerateCode == true).ToList();
            }
            else
            {
                gvdTables.DataSource = _TableSchemaList;
            }
        }




    }
}
