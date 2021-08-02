namespace JuanGenerate
{
    partial class ContextForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageContext = new System.Windows.Forms.TabPage();
            this.boxIsCreate = new System.Windows.Forms.ComboBox();
            this.btnValueSet = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnEntity = new System.Windows.Forms.Button();
            this.boxConnectString = new System.Windows.Forms.ComboBox();
            this.btnSchema = new System.Windows.Forms.Button();
            this.btnCreaeAll = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.chkPreviewCode = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.IsView = new System.Windows.Forms.CheckBox();
            this.btnContext = new System.Windows.Forms.Button();
            this.txtContextKey = new System.Windows.Forms.TextBox();
            this.tabPageJava = new System.Windows.Forms.TabPage();
            this.tabPageSolr = new System.Windows.Forms.TabPage();
            this.btnSolrIndex = new System.Windows.Forms.Button();
            this.btnSolrEntity = new System.Windows.Forms.Button();
            this.tabPageESearch = new System.Windows.Forms.TabPage();
            this.btnESMapping = new System.Windows.Forms.Button();
            this.gvdTables = new System.Windows.Forms.DataGridView();
            this.IsCreate = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SetCheck = new System.Windows.Forms.DataGridViewButtonColumn();
            this.TableName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EntityName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ContextKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TableFormat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ViewName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ViewFormat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageContext.SuspendLayout();
            this.tabPageSolr.SuspendLayout();
            this.tabPageESearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvdTables)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(6);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gvdTables);
            this.splitContainer1.Size = new System.Drawing.Size(2290, 1076);
            this.splitContainer1.SplitterDistance = 184;
            this.splitContainer1.SplitterWidth = 8;
            this.splitContainer1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageContext);
            this.tabControl1.Controls.Add(this.tabPageJava);
            this.tabControl1.Controls.Add(this.tabPageSolr);
            this.tabControl1.Controls.Add(this.tabPageESearch);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(6);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(2290, 184);
            this.tabControl1.TabIndex = 60;
            // 
            // tabPageContext
            // 
            this.tabPageContext.Controls.Add(this.boxIsCreate);
            this.tabPageContext.Controls.Add(this.btnValueSet);
            this.tabPageContext.Controls.Add(this.txtSearch);
            this.tabPageContext.Controls.Add(this.label2);
            this.tabPageContext.Controls.Add(this.btnEntity);
            this.tabPageContext.Controls.Add(this.boxConnectString);
            this.tabPageContext.Controls.Add(this.btnSchema);
            this.tabPageContext.Controls.Add(this.btnCreaeAll);
            this.tabPageContext.Controls.Add(this.label5);
            this.tabPageContext.Controls.Add(this.chkPreviewCode);
            this.tabPageContext.Controls.Add(this.label1);
            this.tabPageContext.Controls.Add(this.IsView);
            this.tabPageContext.Controls.Add(this.btnContext);
            this.tabPageContext.Controls.Add(this.txtContextKey);
            this.tabPageContext.Location = new System.Drawing.Point(8, 39);
            this.tabPageContext.Margin = new System.Windows.Forms.Padding(6);
            this.tabPageContext.Name = "tabPageContext";
            this.tabPageContext.Padding = new System.Windows.Forms.Padding(6);
            this.tabPageContext.Size = new System.Drawing.Size(2274, 137);
            this.tabPageContext.TabIndex = 0;
            this.tabPageContext.Text = "Context";
            this.tabPageContext.UseVisualStyleBackColor = true;
            // 
            // boxIsCreate
            // 
            this.boxIsCreate.FormattingEnabled = true;
            this.boxIsCreate.Items.AddRange(new object[] {
            "全部",
            "生成过",
            "未生成"});
            this.boxIsCreate.Location = new System.Drawing.Point(484, 74);
            this.boxIsCreate.Margin = new System.Windows.Forms.Padding(6);
            this.boxIsCreate.Name = "boxIsCreate";
            this.boxIsCreate.Size = new System.Drawing.Size(238, 32);
            this.boxIsCreate.TabIndex = 65;
            this.boxIsCreate.SelectedIndexChanged += new System.EventHandler(this.boxIsCreate_SelectedIndexChanged);
            // 
            // btnValueSet
            // 
            this.btnValueSet.Location = new System.Drawing.Point(2046, 74);
            this.btnValueSet.Margin = new System.Windows.Forms.Padding(6);
            this.btnValueSet.Name = "btnValueSet";
            this.btnValueSet.Size = new System.Drawing.Size(150, 46);
            this.btnValueSet.TabIndex = 62;
            this.btnValueSet.Text = "实体赋值";
            this.btnValueSet.UseVisualStyleBackColor = true;
            this.btnValueSet.Click += new System.EventHandler(this.btnValueSet_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(158, 72);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(6);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(308, 35);
            this.txtSearch.TabIndex = 61;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 88);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 24);
            this.label2.TabIndex = 60;
            this.label2.Text = "表名：";
            // 
            // btnEntity
            // 
            this.btnEntity.Location = new System.Drawing.Point(1884, 16);
            this.btnEntity.Margin = new System.Windows.Forms.Padding(6);
            this.btnEntity.Name = "btnEntity";
            this.btnEntity.Size = new System.Drawing.Size(150, 46);
            this.btnEntity.TabIndex = 19;
            this.btnEntity.Text = "生成实体";
            this.btnEntity.UseVisualStyleBackColor = true;
            this.btnEntity.Click += new System.EventHandler(this.btnEntity_Click);
            // 
            // boxConnectString
            // 
            this.boxConnectString.FormattingEnabled = true;
            this.boxConnectString.Location = new System.Drawing.Point(158, 12);
            this.boxConnectString.Margin = new System.Windows.Forms.Padding(6);
            this.boxConnectString.Name = "boxConnectString";
            this.boxConnectString.Size = new System.Drawing.Size(308, 32);
            this.boxConnectString.TabIndex = 18;
            // 
            // btnSchema
            // 
            this.btnSchema.Location = new System.Drawing.Point(590, 10);
            this.btnSchema.Margin = new System.Windows.Forms.Padding(6);
            this.btnSchema.Name = "btnSchema";
            this.btnSchema.Size = new System.Drawing.Size(150, 46);
            this.btnSchema.TabIndex = 0;
            this.btnSchema.Text = "加载结构";
            this.btnSchema.UseVisualStyleBackColor = true;
            this.btnSchema.Click += new System.EventHandler(this.btnSchema_Click);
            // 
            // btnCreaeAll
            // 
            this.btnCreaeAll.Location = new System.Drawing.Point(1886, 70);
            this.btnCreaeAll.Margin = new System.Windows.Forms.Padding(6);
            this.btnCreaeAll.Name = "btnCreaeAll";
            this.btnCreaeAll.Size = new System.Drawing.Size(148, 46);
            this.btnCreaeAll.TabIndex = 29;
            this.btnCreaeAll.Text = "一键生成";
            this.btnCreaeAll.UseVisualStyleBackColor = true;
            this.btnCreaeAll.Click += new System.EventHandler(this.btnCreaeAll_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(40, 24);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 24);
            this.label5.TabIndex = 17;
            this.label5.Text = "连接串：";
            // 
            // chkPreviewCode
            // 
            this.chkPreviewCode.AutoSize = true;
            this.chkPreviewCode.Location = new System.Drawing.Point(1708, 24);
            this.chkPreviewCode.Margin = new System.Windows.Forms.Padding(6);
            this.chkPreviewCode.Name = "chkPreviewCode";
            this.chkPreviewCode.Size = new System.Drawing.Size(138, 28);
            this.chkPreviewCode.TabIndex = 49;
            this.chkPreviewCode.Text = "预览代码";
            this.chkPreviewCode.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1086, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 24);
            this.label1.TabIndex = 30;
            this.label1.Text = "上下文键值：";
            // 
            // IsView
            // 
            this.IsView.AutoSize = true;
            this.IsView.Location = new System.Drawing.Point(482, 16);
            this.IsView.Margin = new System.Windows.Forms.Padding(6);
            this.IsView.Name = "IsView";
            this.IsView.Size = new System.Drawing.Size(90, 28);
            this.IsView.TabIndex = 52;
            this.IsView.Text = "视图";
            this.IsView.UseVisualStyleBackColor = true;
            // 
            // btnContext
            // 
            this.btnContext.Location = new System.Drawing.Point(2046, 16);
            this.btnContext.Margin = new System.Windows.Forms.Padding(6);
            this.btnContext.Name = "btnContext";
            this.btnContext.Size = new System.Drawing.Size(150, 46);
            this.btnContext.TabIndex = 1;
            this.btnContext.Text = "生成上下文";
            this.btnContext.UseVisualStyleBackColor = true;
            this.btnContext.Click += new System.EventHandler(this.btnContext_Click);
            // 
            // txtContextKey
            // 
            this.txtContextKey.Location = new System.Drawing.Point(1252, 16);
            this.txtContextKey.Margin = new System.Windows.Forms.Padding(6);
            this.txtContextKey.Name = "txtContextKey";
            this.txtContextKey.Size = new System.Drawing.Size(370, 35);
            this.txtContextKey.TabIndex = 31;
            // 
            // tabPageJava
            // 
            this.tabPageJava.Location = new System.Drawing.Point(8, 39);
            this.tabPageJava.Margin = new System.Windows.Forms.Padding(6);
            this.tabPageJava.Name = "tabPageJava";
            this.tabPageJava.Padding = new System.Windows.Forms.Padding(6);
            this.tabPageJava.Size = new System.Drawing.Size(2274, 137);
            this.tabPageJava.TabIndex = 1;
            this.tabPageJava.Text = "Java";
            this.tabPageJava.UseVisualStyleBackColor = true;
            // 
            // tabPageSolr
            // 
            this.tabPageSolr.Controls.Add(this.btnSolrIndex);
            this.tabPageSolr.Controls.Add(this.btnSolrEntity);
            this.tabPageSolr.Location = new System.Drawing.Point(8, 39);
            this.tabPageSolr.Margin = new System.Windows.Forms.Padding(6);
            this.tabPageSolr.Name = "tabPageSolr";
            this.tabPageSolr.Padding = new System.Windows.Forms.Padding(6);
            this.tabPageSolr.Size = new System.Drawing.Size(2274, 137);
            this.tabPageSolr.TabIndex = 2;
            this.tabPageSolr.Text = "Solr";
            this.tabPageSolr.UseVisualStyleBackColor = true;
            // 
            // btnSolrIndex
            // 
            this.btnSolrIndex.Location = new System.Drawing.Point(264, 40);
            this.btnSolrIndex.Margin = new System.Windows.Forms.Padding(6);
            this.btnSolrIndex.Name = "btnSolrIndex";
            this.btnSolrIndex.Size = new System.Drawing.Size(150, 46);
            this.btnSolrIndex.TabIndex = 50;
            this.btnSolrIndex.Text = "SolrIndex";
            this.btnSolrIndex.UseVisualStyleBackColor = true;
            this.btnSolrIndex.Click += new System.EventHandler(this.btnSolrIndex_Click);
            // 
            // btnSolrEntity
            // 
            this.btnSolrEntity.Location = new System.Drawing.Point(60, 40);
            this.btnSolrEntity.Margin = new System.Windows.Forms.Padding(6);
            this.btnSolrEntity.Name = "btnSolrEntity";
            this.btnSolrEntity.Size = new System.Drawing.Size(150, 46);
            this.btnSolrEntity.TabIndex = 51;
            this.btnSolrEntity.Text = "SolrEntity";
            this.btnSolrEntity.UseVisualStyleBackColor = true;
            this.btnSolrEntity.Click += new System.EventHandler(this.btnSolrEntity_Click);
            // 
            // tabPageESearch
            // 
            this.tabPageESearch.Controls.Add(this.btnESMapping);
            this.tabPageESearch.Location = new System.Drawing.Point(8, 39);
            this.tabPageESearch.Margin = new System.Windows.Forms.Padding(6);
            this.tabPageESearch.Name = "tabPageESearch";
            this.tabPageESearch.Padding = new System.Windows.Forms.Padding(6);
            this.tabPageESearch.Size = new System.Drawing.Size(2274, 137);
            this.tabPageESearch.TabIndex = 3;
            this.tabPageESearch.Text = "ESearch";
            this.tabPageESearch.UseVisualStyleBackColor = true;
            // 
            // btnESMapping
            // 
            this.btnESMapping.Location = new System.Drawing.Point(76, 42);
            this.btnESMapping.Margin = new System.Windows.Forms.Padding(6);
            this.btnESMapping.Name = "btnESMapping";
            this.btnESMapping.Size = new System.Drawing.Size(150, 46);
            this.btnESMapping.TabIndex = 58;
            this.btnESMapping.Text = "ESMapping";
            this.btnESMapping.UseVisualStyleBackColor = true;
            this.btnESMapping.Click += new System.EventHandler(this.btnESMapping_Click);
            // 
            // gvdTables
            // 
            this.gvdTables.AllowUserToAddRows = false;
            this.gvdTables.AllowUserToDeleteRows = false;
            this.gvdTables.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvdTables.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvdTables.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gvdTables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvdTables.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IsCreate,
            this.SetCheck,
            this.TableName,
            this.EntityName,
            this.Description,
            this.ContextKey,
            this.TableFormat,
            this.ViewName,
            this.ViewFormat});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvdTables.DefaultCellStyle = dataGridViewCellStyle2;
            this.gvdTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvdTables.Location = new System.Drawing.Point(0, 0);
            this.gvdTables.Margin = new System.Windows.Forms.Padding(6);
            this.gvdTables.Name = "gvdTables";
            this.gvdTables.RowHeadersVisible = false;
            this.gvdTables.RowTemplate.Height = 23;
            this.gvdTables.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvdTables.Size = new System.Drawing.Size(2290, 884);
            this.gvdTables.TabIndex = 1;
            this.gvdTables.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvdTables_CellClick);
            // 
            // IsCreate
            // 
            this.IsCreate.DataPropertyName = "IsCreate";
            this.IsCreate.FalseValue = "False";
            this.IsCreate.FillWeight = 91.37057F;
            this.IsCreate.HeaderText = "生成";
            this.IsCreate.Name = "IsCreate";
            this.IsCreate.TrueValue = "True";
            // 
            // SetCheck
            // 
            this.SetCheck.FillWeight = 101.0787F;
            this.SetCheck.HeaderText = "字段";
            this.SetCheck.Name = "SetCheck";
            this.SetCheck.Text = "设置";
            this.SetCheck.UseColumnTextForButtonValue = true;
            // 
            // TableName
            // 
            this.TableName.DataPropertyName = "TableName";
            this.TableName.FillWeight = 101.0787F;
            this.TableName.HeaderText = "表名";
            this.TableName.Name = "TableName";
            // 
            // EntityName
            // 
            this.EntityName.DataPropertyName = "EntityName";
            this.EntityName.FillWeight = 101.0787F;
            this.EntityName.HeaderText = "实体名称";
            this.EntityName.Name = "EntityName";
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.FillWeight = 101.0787F;
            this.Description.HeaderText = "表说明";
            this.Description.Name = "Description";
            // 
            // ContextKey
            // 
            this.ContextKey.DataPropertyName = "ContextKey";
            this.ContextKey.FillWeight = 101.0787F;
            this.ContextKey.HeaderText = "上下文键值";
            this.ContextKey.Name = "ContextKey";
            // 
            // TableFormat
            // 
            this.TableFormat.DataPropertyName = "TableFormat";
            this.TableFormat.FillWeight = 101.0787F;
            this.TableFormat.HeaderText = "分表格式";
            this.TableFormat.Name = "TableFormat";
            // 
            // ViewName
            // 
            this.ViewName.DataPropertyName = "ViewName";
            this.ViewName.FillWeight = 101.0787F;
            this.ViewName.HeaderText = "视图名称";
            this.ViewName.Name = "ViewName";
            // 
            // ViewFormat
            // 
            this.ViewFormat.DataPropertyName = "ViewFormat";
            this.ViewFormat.FillWeight = 101.0787F;
            this.ViewFormat.HeaderText = "视图格式";
            this.ViewFormat.Name = "ViewFormat";
            // 
            // ContextForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2290, 1076);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "ContextForm";
            this.Text = "Context生成";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageContext.ResumeLayout(false);
            this.tabPageContext.PerformLayout();
            this.tabPageSolr.ResumeLayout(false);
            this.tabPageESearch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvdTables)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnSchema;
        private System.Windows.Forms.ComboBox boxConnectString;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnCreaeAll;
        private System.Windows.Forms.Button btnEntity;
        private System.Windows.Forms.Button btnContext;
        private System.Windows.Forms.TextBox txtContextKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkPreviewCode;
        private System.Windows.Forms.Button btnSolrEntity;
        private System.Windows.Forms.Button btnSolrIndex;
        private System.Windows.Forms.CheckBox IsView;
        private System.Windows.Forms.Button btnESMapping;
        private System.Windows.Forms.DataGridView gvdTables;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageContext;
        private System.Windows.Forms.TabPage tabPageJava;
        private System.Windows.Forms.TabPage tabPageSolr;
        private System.Windows.Forms.TabPage tabPageESearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnValueSet;
        private System.Windows.Forms.ComboBox boxIsCreate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsCreate;
        private System.Windows.Forms.DataGridViewButtonColumn SetCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn TableName;
        private System.Windows.Forms.DataGridViewTextBoxColumn EntityName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn ContextKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn TableFormat;
        private System.Windows.Forms.DataGridViewTextBoxColumn ViewName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ViewFormat;
    }
}