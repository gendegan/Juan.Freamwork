namespace JuanGenerate
{
    partial class ContextColumnForm
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
            this.gvwTableCols = new System.Windows.Forms.DataGridView();
            this.FieldName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FieldTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ErrorMessage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DbIgnore = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gvwTableCols)).BeginInit();
            this.SuspendLayout();
            // 
            // gvwTableCols
            // 
            this.gvwTableCols.AllowUserToAddRows = false;
            this.gvwTableCols.AllowUserToDeleteRows = false;
            this.gvwTableCols.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvwTableCols.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvwTableCols.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gvwTableCols.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvwTableCols.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FieldName,
            this.FieldTitle,
            this.IsCheck,
            this.ErrorMessage,
            this.DbIgnore});
            this.gvwTableCols.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvwTableCols.Location = new System.Drawing.Point(0, 0);
            this.gvwTableCols.Margin = new System.Windows.Forms.Padding(6);
            this.gvwTableCols.Name = "gvwTableCols";
            this.gvwTableCols.RowHeadersVisible = false;
            this.gvwTableCols.RowTemplate.Height = 23;
            this.gvwTableCols.Size = new System.Drawing.Size(1698, 850);
            this.gvwTableCols.TabIndex = 1;
            // 
            // FieldName
            // 
            this.FieldName.DataPropertyName = "FieldName";
            this.FieldName.HeaderText = "字段名";
            this.FieldName.Name = "FieldName";
            this.FieldName.ReadOnly = true;
            // 
            // FieldTitle
            // 
            this.FieldTitle.DataPropertyName = "FieldTitle";
            this.FieldTitle.HeaderText = "字段标题";
            this.FieldTitle.Name = "FieldTitle";
            // 
            // IsCheck
            // 
            this.IsCheck.DataPropertyName = "IsCheck";
            this.IsCheck.FalseValue = "False";
            this.IsCheck.HeaderText = "是否检查";
            this.IsCheck.Name = "IsCheck";
            this.IsCheck.TrueValue = "True";
            // 
            // ErrorMessage
            // 
            this.ErrorMessage.DataPropertyName = "ErrorMessage";
            this.ErrorMessage.HeaderText = "错误提示";
            this.ErrorMessage.Name = "ErrorMessage";
            this.ErrorMessage.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ErrorMessage.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // DbIgnore
            // 
            this.DbIgnore.DataPropertyName = "DbIgnore";
            this.DbIgnore.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.DbIgnore.HeaderText = "DbIgnore";
            this.DbIgnore.Items.AddRange(new object[] {
            "No",
            "All",
            "Add",
            "Update"});
            this.DbIgnore.Name = "DbIgnore";
            // 
            // ContextColumnForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1698, 850);
            this.Controls.Add(this.gvwTableCols);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ContextColumnForm";
            this.Text = "字段设置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ContextColumnForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.gvwTableCols)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gvwTableCols;
        private System.Windows.Forms.DataGridViewTextBoxColumn FieldName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FieldTitle;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn ErrorMessage;
        private System.Windows.Forms.DataGridViewComboBoxColumn DbIgnore;
    }
}