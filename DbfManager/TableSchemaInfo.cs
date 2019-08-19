using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbfManager
{
    public class TableSchemaInfo
    {
        public TableSchemaInfo(DataRow datarow)
        {
            this.datarow = datarow;
        }

        private DataRow datarow;

        public String TABLE_CATALOG { get { return this.datarow.Field<String>("TABLE_CATALOG"); } }
        public String TABLE_SCHEMA { get { return this.datarow.Field<String>("TABLE_SCHEMA"); } }
        public String TABLE_NAME { get { return this.datarow.Field<String>("TABLE_NAME"); } }
        public String COLUMN_NAME { get { return this.datarow.Field<String>("COLUMN_NAME"); } }
        public Guid? COLUMN_GUID { get { return this.datarow.Field<Guid?>("COLUMN_GUID"); } }
        public Int64? COLUMN_PROPID { get { return this.datarow.Field<Int64?>("COLUMN_PROPID"); } }
        public Int64? ORDINAL_POSITION { get { return this.datarow.Field<Int64?>("ORDINAL_POSITION"); } }
        public Boolean COLUMN_HASDEFAULT { get { return this.datarow.Field<Boolean>("COLUMN_HASDEFAULT"); } }
        public String COLUMN_DEFAULT { get { return this.datarow.Field<String>("COLUMN_DEFAULT"); } }
        public Int64? COLUMN_FLAGS { get { return this.datarow.Field<Int64?>("COLUMN_FLAGS"); } }
        public Boolean IS_NULLABLE { get { return this.datarow.Field<Boolean>("IS_NULLABLE"); } }
        public Int32? DATA_TYPE { get { return this.datarow.Field<Int32?>("DATA_TYPE"); } }
        public Guid? TYPE_GUID { get { return this.datarow.Field<Guid?>("TYPE_GUID"); } }
        public Int64? CHARACTER_MAXIMUM_LENGTH { get { return this.datarow.Field<Int64?>("CHARACTER_MAXIMUM_LENGTH"); } }
        public Int64? CHARACTER_OCTET_LENGTH { get { return this.datarow.Field<Int64?>("CHARACTER_OCTET_LENGTH"); } }
        public Int32? NUMERIC_PRECISION { get { return this.datarow.Field<Int32?>("NUMERIC_PRECISION"); } }
        public Int16? NUMERIC_SCALE { get { return this.datarow.Field<Int16?>("NUMERIC_SCALE"); } }
        public Int64? DATETIME_PRECISION { get { return this.datarow.Field<Int64?>("DATETIME_PRECISION"); } }
        public String CHARACTER_SET_CATALOG { get { return this.datarow.Field<String>("CHARACTER_SET_CATALOG"); } }
        public String CHARACTER_SET_SCHEMA { get { return this.datarow.Field<String>("CHARACTER_SET_SCHEMA"); } }
        public String CHARACTER_SET_NAME { get { return this.datarow.Field<String>("CHARACTER_SET_NAME"); } }
        public String COLLATION_CATALOG { get { return this.datarow.Field<String>("COLLATION_CATALOG"); } }
        public String COLLATION_SCHEMA { get { return this.datarow.Field<String>("COLLATION_SCHEMA"); } }
        public String COLLATION_NAME { get { return this.datarow.Field<String>("COLLATION_NAME"); } }
        public String DOMAIN_CATALOG { get { return this.datarow.Field<String>("DOMAIN_CATALOG"); } }
        public String DOMAIN_SCHEMA { get { return this.datarow.Field<String>("DOMAIN_SCHEMA"); } }
        public String DOMAIN_NAME { get { return this.datarow.Field<String>("DOMAIN_NAME"); } }
        public String DESCRIPTION { get { return this.datarow.Field<String>("DESCRIPTION"); } }

        public Type _Data_Type
        {
            get
            {
                Type _dt;
                switch (this.DATA_TYPE)
                {
                    case 5:
                        _dt = typeof(double);
                        break;
                    case 11:
                        _dt = typeof(bool);
                        break;
                    case 129:
                        _dt = typeof(string);
                        break;
                    case 131:
                        _dt = typeof(decimal);
                        break;
                    case 133:
                        _dt = typeof(DateTime?);
                        break;
                    default:
                        _dt = typeof(string);
                        break;
                }

                return _dt;
            }
        }
    }
}
