
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace JuanGenerate
{
    public partial class ConnectConfigFrom : Form
    {
        bool IsAdd = true;

        GenerateHelper _GenerateHelper = null;
        SelectFileInfo _SelectFileInfo = null;
        public ConnectConfigFrom(SelectFileInfo objSelectFileInfo)
        {
            InitializeComponent();
            _SelectFileInfo = objSelectFileInfo;
            _GenerateHelper = new GenerateHelper(objSelectFileInfo.GeneratePath);
            LoadConnectString();
        }

        private void cbbConnectString_SelectedValueChanged(object sender, EventArgs e)
        {

            LoadConnectString(cbbConnectString.SelectedItem.ToString());
            IsAdd = false;
        }

        void LoadConnectString(string ConnectStringName)
        {
            if (string.IsNullOrWhiteSpace(ConnectStringName))
            {
                return;
            }
            if (!_GenerateHelper.LoadConfigXml())
            {
                return;
            }

            XmlNode objXmlNode = _GenerateHelper.GetConnectionStringNode(ConnectStringName, true);
            XmlElement objXmlElement = (XmlElement)objXmlNode;
            txtConnectString.Text = objXmlElement.GetAttribute("Name");
            txtDatabase.Text = objXmlElement.GetAttribute("Database");
            txtPassword.Text = objXmlElement.GetAttribute("Pwd");
            txtPort.Text = objXmlElement.GetAttribute("Port");
            txtServerIP.Text = objXmlElement.GetAttribute("ServerIP");
            txtUserID.Text = objXmlElement.GetAttribute("Uid");
            txtContextKey.Text = objXmlElement.GetAttribute("ContextKey");
        }

        void SetInitConnectValue()
        {
            txtConnectString.Text = _SelectFileInfo.ProjectName + ".ConnectionString";
            txtPort.Text = "3306";
            txtServerIP.Text = "192.168.100.179";
            txtUserID.Text = "gao7test";
            txtDatabase.Text = "";
            txtPassword.Text = "gao7test";
            IsAdd = true;
        }

        /// <summary>
        /// 加载连接cbb
        /// </summary>
        private void LoadConnectString()
        {
            cbbConnectString.Items.Clear();

            if (!_GenerateHelper.LoadConfigXml())
            {
                return;
            }
            XmlNodeList objXmlNodeList = _GenerateHelper.GenerateConfig.SelectNodes("//ConnectionStrings/ConnectionString");
            foreach (XmlNode objXmlNode in objXmlNodeList)
            {
                cbbConnectString.Items.Add(objXmlNode.Attributes["Name"].Value);
            }
        }

        /// <summary>
        /// 检查输入
        /// </summary>
        /// <returns></returns>
        bool CheckInput()
        {
            if (string.IsNullOrWhiteSpace(txtConnectString.Text))
            {
                MessageBox.Show("连接串名称不能为空");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtServerIP.Text))
            {
                MessageBox.Show("SQL主机地址不能为空");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtUserID.Text))
            {
                MessageBox.Show("用户名不能为空");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("密码不能为空");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtPort.Text))
            {
                MessageBox.Show("端口不能为空");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtDatabase.Text))
            {
                MessageBox.Show("数据库不能为空");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 保存连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckInput())
                {
                    return;
                }
                if (!_GenerateHelper.LoadConfigXml())
                {
                    return;
                }
                XmlElement objXmlElement = null;
                if (IsAdd)
                {
                    int index = cbbConnectString.FindString(txtConnectString.Text);
                    if (index >= 0)
                    {
                        MessageBox.Show("此连接串名称已经存在");
                        return;
                    }
                    objXmlElement = (XmlElement)_GenerateHelper.GetConnectionStringNode(txtConnectString.Text, true);
                }
                else
                {
                    objXmlElement = (XmlElement)_GenerateHelper.GetConnectionStringNode(cbbConnectString.SelectedItem.ToString(), true);
                }
                objXmlElement.SetAttribute("Name", txtConnectString.Text);
                objXmlElement.SetAttribute("ServerIP", txtServerIP.Text);
                objXmlElement.SetAttribute("Uid", txtUserID.Text);
                objXmlElement.SetAttribute("Pwd", txtPassword.Text);
                objXmlElement.SetAttribute("Port", txtPort.Text);
                objXmlElement.SetAttribute("Database", txtDatabase.Text);
                objXmlElement.SetAttribute("DbType", "MySql");
                objXmlElement.SetAttribute("ContextKey", txtContextKey.Text);
                if (!IsAdd && (txtConnectString.Text != cbbConnectString.SelectedItem.ToString()))
                {
                    XmlNodeList objBizXmlNodeList = _GenerateHelper.GenerateConfig.SelectNodes("//ContextConfig/Context[@ConnectionKey='" + cbbConnectString.SelectedItem + "']");
                    foreach (XmlNode objBizXmlNode in objBizXmlNodeList)
                    {
                        XmlElement objBizXmlElement = (XmlElement)objBizXmlNode;
                        objBizXmlElement.SetAttribute("ConnectionKey", txtConnectString.Text);
                    }
                }
                if (!File.Exists(_GenerateHelper.GenerateConfigPath))
                {

                    _GenerateHelper.Save();
                    _SelectFileInfo.Project.ProjectItems.AddFromFile(_GenerateHelper.GenerateConfigPath);
                }
                else
                {
                    _GenerateHelper.Save();
                }
                LoadConnectString();
                cbbConnectString.SelectedItem = txtConnectString.Text;
                IsAdd = false;
                MessageBox.Show("保存成功！");
            }
            catch (Exception objExp)
            {
                MessageBox.Show(objExp.Message);
            }
        }

        /// <summary>
        /// 测试连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTest_Click(object sender, EventArgs e)
        {
            if (!CheckInput())
            {
                return;
            }
            try
            {
                string connectString = "SERVER=" + txtServerIP.Text + ";Port=" + txtPort.Text + ";Database=" + txtDatabase.Text + ";Uid=" + txtUserID.Text + ";Pwd=" + txtPassword.Text + ";persist security info=True;Allow User Variables=True;Charset=utf8; ";
                DataTable objDataTable = MySqlHelper.ExecuteDataTable(connectString, " SELECT * FROM INFORMATION_SCHEMA.SCHEMATA");
                if (objDataTable == null)
                {
                    MessageBox.Show("测试连接失败！");
                    return;
                }
                MessageBox.Show("测试连接成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("测试连接抛出异常，请查看连接串是否配置正确，异常信息：" + ex.ToString());
                return;
            }
        }

        /// <summary>
        /// 新建连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            SetInitConnectValue();
        }

        private void ConnectConfigFrom_Load(object sender, EventArgs e)
        {

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder objStringBuilder = new StringBuilder();
                objStringBuilder.Append("SERVER=" + txtServerIP.Text + ";");
                objStringBuilder.Append("Port=" + txtPort.Text + ";");
                objStringBuilder.Append("user id=" + txtUserID.Text + ";");
                objStringBuilder.Append("password=" + txtPassword.Text + ";");
                objStringBuilder.Append("persist security info=True;Allow User Variables=True;Charset=utf8; ");
                string connectionString = objStringBuilder.ToString();
                DataTable objDataTable = SchemaHelper.GetSchemata(connectionString);
                boxDataBase.Items.Clear();
                foreach (DataRow objDataRow in objDataTable.Rows)
                {
                    boxDataBase.Items.Add(objDataRow["SCHEMA_NAME"]);
                }
            }
            catch (Exception objExp)
            {
                MessageBox.Show("加载库出错" + objExp.Message);
            }
        }

        private void boxDataBase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (boxDataBase.SelectedItem != null)
            {
                txtDatabase.Text = boxDataBase.SelectedItem.ToString();
            }
        }
    }
}
