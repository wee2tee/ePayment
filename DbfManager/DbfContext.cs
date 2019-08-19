using DbfManager.DbfModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DbfManager
{
    public class DbfContext
    {
        private string _data_path;
        public string DataPath { get { return this._data_path; } }
        private OleDbConnection _conn;
        public OleDbConnection Connection { get { return this._conn;} }

        public DbfContext(string absolute_data_path)
        {
            this._data_path = absolute_data_path += absolute_data_path.EndsWith(@"\") ? "" : @"\";
            this._conn = new OleDbConnection(@"Provider=VFPOLEDB.1;Data Source=" + this._data_path);
        }

        public List<string> GetDbfFileName()
        {
            List<string> filename = new List<string>();
            DirectoryInfo dinfo = new DirectoryInfo(this._data_path);
            FileInfo[] finfo = dinfo.GetFiles("*.dbf");
            finfo.ToList().ForEach(f => filename.Add(f.Name.ToUpper().Replace(".DBF", "")));

            return filename;
        }

        public List<TableSchemaInfo> GetTableSchemaInfoList(string table_name = "")
        {
            if (!(Directory.Exists(this._data_path)))
            {
                MessageBox.Show("ค้นหาไดเร็คทอรี่ " + this._data_path + " ไม่พบ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return null;
            }

            List<TableSchemaInfo> schema = new List<TableSchemaInfo>();
            try
            {
                this._conn.Open();

                if (this._conn.State == ConnectionState.Open)
                {
                    DataTable dt = this._conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Columns, new Object[] { });
                    this._conn.Close();

                    foreach (DataRow row in dt.Rows)
                    {
                        if (row.Field<string>("TABLE_NAME").ToUpper() == table_name.ToUpper())
                            schema.Add(new TableSchemaInfo(row));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return schema;
        }

        //public DataTable GetDataBySql(string sql)
        //{
        //    OleDbCommand cmd = this.Connection.CreateCommand();
        //    cmd.CommandText = sql;

        //    DataTable dt = new DataTable();

        //    this.Connection.Open();
        //    using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
        //    {
        //        da.Fill(dt);
        //    }
        //    this.Connection.Close();

        //    return dt;
        //}

        public List<T> GetDataBySql<T>(string sql, string from_table)
        {
            OleDbCommand cmd = this.Connection.CreateCommand();
            cmd.CommandText = sql;

            DataTable dt = new DataTable();

            try
            {
                this.Connection.Open();
                using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
                this.Connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            var table_schema = this.GetTableSchemaInfoList(from_table);

            return dt.CastTo<T>(table_schema);
        }

        /* Test get data from Artrn */
        //public artrn ArtrnAt(string docnum)
        //{
        //    if (!(Directory.Exists(this._data_path) && File.Exists(this._data_path + "artrn.dbf")))
        //    {
        //        MessageBox.Show("ค้นหาแฟ้ม Artrn.dbf ในที่เก็บข้อมูล \"" + this._data_path + "\" ไม่พบ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //        return null;
        //    }

        //    artrn artrn = null;
        //    try
        //    {
        //        this._conn.Open();

        //        DataTable dt = new DataTable();

        //        if (this._conn.State == ConnectionState.Open)
        //        {
        //            string sql = "Select * from artrn Where RTRIM(docnum)='" + docnum.TrimEnd() + "'";
        //            OleDbCommand cmd = new OleDbCommand(sql, this._conn);
        //            OleDbDataAdapter da = new OleDbDataAdapter(cmd);

        //            da.Fill(dt);
        //            this._conn.Close();

        //            if(dt.Rows.Count > 0)
        //            {
        //                artrn = dt.Rows[0].ToArtrn(this.GetTableSchemaInfoList("artrn"));
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }

        //    return artrn;
        //}

        //public List<artrn> ArtrnRange(string docnum_from, string docnum_to)
        //{
        //    if (!(Directory.Exists(this._data_path) && File.Exists(this._data_path + "artrn.dbf")))
        //    {
        //        MessageBox.Show("ค้นหาแฟ้ม Artrn.dbf ในที่เก็บข้อมูล \"" + this._data_path + "\" ไม่พบ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //        return null;
        //    }

        //    List<artrn> artrns = new List<artrn>();
        //    try
        //    {
        //        this._conn.Open();

        //        DataTable dt = new DataTable();

        //        if(this._conn.State == ConnectionState.Open)
        //        {
        //            string sql = "Select * from artrn Where RTRIM(docnum)>='" + docnum_from.TrimEnd() + "' and RTRIM(docnum)<='" + docnum_to.TrimEnd() + "'";
        //            OleDbCommand cmd = new OleDbCommand(sql, this._conn);
        //            OleDbDataAdapter da = new OleDbDataAdapter(cmd);

        //            da.Fill(dt);
        //            this._conn.Close();

        //            artrns.AddRange(dt.ToArtrnList(this.GetTableSchemaInfoList("artrn")));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }

        //    return artrns;
        //}

        //public List<artrn> ArtrnBySql(string sql)
        //{
        //    if (!(Directory.Exists(this._data_path) && File.Exists(this._data_path + "artrn.dbf")))
        //    {
        //        MessageBox.Show("ค้นหาแฟ้ม Artrn.dbf ในที่เก็บข้อมูล \"" + this._data_path + "\" ไม่พบ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //        return null;
        //    }

        //    List<artrn> artrns = new List<artrn>();
        //    try
        //    {
        //        this._conn.Open();

        //        DataTable dt = new DataTable();

        //        if (this._conn.State == ConnectionState.Open)
        //        {
        //            OleDbCommand cmd = new OleDbCommand(sql, this._conn);
        //            OleDbDataAdapter da = new OleDbDataAdapter(cmd);

        //            da.Fill(dt);
        //            this._conn.Close();

        //            artrns.AddRange(dt.ToArtrnList(this.GetTableSchemaInfoList("artrn")));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }

        //    return artrns;
        //}

        //public List<artrn> ArtrnAll()
        //{
        //    if (!(Directory.Exists(this._data_path) && File.Exists(this._data_path + "artrn.dbf")))
        //    {
        //        MessageBox.Show("ค้นหาแฟ้ม Artrn.dbf ในที่เก็บข้อมูล \"" + this._data_path + "\" ไม่พบ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //        return null;
        //    }

        //    List<artrn> artrns = new List<artrn>();

        //    try
        //    {
        //        this._conn.Open();

        //        DataTable dt = new DataTable();

        //        if (this._conn.State == ConnectionState.Open)
        //        {
        //            OleDbCommand cmd = this._conn.CreateCommand(); //new OleDbCommand("", this._conn);
        //            cmd.CommandText = "Select * from artrn";
        //            OleDbDataAdapter da = new OleDbDataAdapter(cmd);

        //            da.Fill(dt);
        //            this._conn.Close();

        //            artrns = dt.ToArtrnList(this.GetTableSchemaInfoList("artrn"));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }

        //    return artrns;
        //}
    }

    public class ComboboxItem
    {
        private string _text = "";
        private object _value = null;

        public ComboboxItem(string displayed_text, object value)
        {
            this._text = displayed_text;
            this._value = value;
        }
        public string Text { get { return this._text; } }
        public object Value { get { return this._value; } }
        public override string ToString()
        {
            return this.Text;
        }
    }
}
