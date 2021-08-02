using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JuanGenerate
{
    public partial class TableSchema
    {

        public TableSchema()
        {
            UIProjectName = "";
            UIProjectPath = "";
            EditUrl = "";
            ListUrl = "";
            ModuleID = "";
            ContextKey = "";
           

        }
        public bool Is
        {
            get;
            set;
        }
        public string TableName
        {
            get;
            set;
        }


        public string TableFormat
        {
            get;
            set;
        }

        public string ViewName
        {
            get;
            set;
        }

        public string ViewFormat
        {
            get;
            set;
        }
      

        public bool IsGenerateCode
        {
            get;
            set;
        }

        public string SimpleTableName
        {
            get
            {
                return this.TableName.Split('_').Count() > 1 ? this.TableName.Split('_')[1] : this.TableName;
            }
        }

        public string EntityName
        {
            get;
            set;
        }



        public string EntityMemberName
        {
            get
            {

                return EntityName.CamelCase();


            }
        }

        public ColumnSchema PrimaryKey
        {
            get
            {

                return Columns.FirstOrDefault(s => s.ColumnType == "PrimaryKey");
            }

        }
        public ColumnSchema ForeignKey
        {
            get
            {

                return Columns.FirstOrDefault(s => s.ColumnType == "ForeignKey");
            }
        }
        public string Description
        {
            get;
            set;
        }



        List<ColumnSchema> _Columns = new List<ColumnSchema>();
        public List<ColumnSchema> Columns
        {
            get
            {
                return _Columns;
            }
            set
            {
                _Columns = value;
            }
        }


        public string ContextKey
        {
            get;
            set;
        }
     
        public string ConnectionKey
        {
            get;
            set;
        }
        public string ContextName
        {
            get
            {

                return EntityName + "Context";
            }
        }

        public bool IsCreate
        {
            get;
            set;
        }
    }

}
