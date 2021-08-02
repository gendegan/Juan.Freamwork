using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JuanGenerate
{
    public partial class ContextColumnForm : Form
    {


        SelectFileInfo _SelectFileInfo = null;
        TableSchema _TableSchema;
        public ContextColumnForm(TableSchema objTableSchema, SelectFileInfo objSelectFileInfo)
        {
            InitializeComponent();
            _SelectFileInfo = objSelectFileInfo;
            _TableSchema = objTableSchema;
            gvwTableCols.AutoGenerateColumns = false;
            gvwTableCols.DataSource = objTableSchema.Columns;
        }

        private void ContextColumnForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            List<ColumnSchema> objColumnRuleSchemaList = _TableSchema.Columns;
            foreach (DataGridViewRow objDataRow in gvwTableCols.Rows)
            {
                string FieldName = objDataRow.ReadCell("FieldName");
                ColumnSchema objColumnSchema = objColumnRuleSchemaList.First(s => s.FieldName == FieldName);
                objColumnSchema.IsXmlField = CheckIsXmlField(objColumnSchema, objDataRow);
                objColumnSchema.IsCheck = objDataRow.ReadCellBool("IsCheck");
                objColumnSchema.ErrorMessage = objDataRow.ReadCell("ErrorMessage");
                objColumnSchema.FieldTitle = objDataRow.ReadCell("FieldTitle");
                objColumnSchema.DbIgnore = objDataRow.ReadCell("DbIgnore");
            }
        }

        private bool CheckIsXmlField(ColumnSchema objColumnSchema, DataGridViewRow objDataRow)
        {

            if (objColumnSchema.IsXmlField)
            {
                return true;
            }
            if (objDataRow.ReadCellBool("IsCheck") ||
             objColumnSchema.OldDbIgnore != objDataRow.ReadCell("DbIgnore") || objColumnSchema.OldErrorMessage != objDataRow.ReadCell("ErrorMessage") || objColumnSchema.OldFieldTitle != objDataRow.ReadCell("FieldTitle"))
            {
                return true;
            }
            return false;
        }
    }
}
