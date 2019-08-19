using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DbfManager.DbfModel;

namespace DbfManager
{
    public static class XHelper
    {
        //public static artrn ToArtrn(this DataRow row, List<TableSchemaInfo> table_schema)
        //{
        //    artrn a = new artrn();
        //    List<string> cols_name = row.Table.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower()).ToList();

        //    foreach (var t in table_schema)
        //    {
        //        if(!cols_name.Contains(t.COLUMN_NAME.ToLower()))
        //        {
        //            continue;
        //        }

        //        try
        //        {
        //            object field_data = null;

        //            switch (t.DATA_TYPE)
        //            {
        //                case 5:
        //                    field_data = row.Field<double>(t.COLUMN_NAME.ToLower()); //"double";
        //                    break;
        //                case 11:
        //                    field_data = row.Field<bool>(t.COLUMN_NAME.ToLower()); //"bool";
        //                    break;
        //                case 129:
        //                    field_data = row.Field<string>(t.COLUMN_NAME.ToLower()); //"string";
        //                    break;
        //                case 131:
        //                    field_data = row.Field<decimal>(t.COLUMN_NAME.ToLower()); //"decimal";
        //                    break;
        //                case 133:
        //                    field_data = row.Field<DateTime?>(t.COLUMN_NAME.ToLower()); //"DateTime?";
        //                    break;
        //                default:
        //                    field_data = row.Field<string>(t.COLUMN_NAME.ToLower()); //"string";
        //                    break;
        //            }

        //            a.GetType().GetProperty(t.COLUMN_NAME.ToLower()).SetValue(a, field_data, null);
        //        }
        //        catch (Exception ex)
        //        {
        //            //Console.WriteLine(" ==> " + ex.Message);
        //        }
        //    }

        //    return a;
        //}

        //public static List<artrn> ToArtrnList(this DataTable datatable, List<TableSchemaInfo> table_schema)
        //{
        //    List<artrn> artrns = new List<artrn>();

        //    foreach (DataRow row in datatable.Rows)
        //    {
        //        artrns.Add(row.ToArtrn(table_schema));
        //    }

        //    return artrns;
        //}

        public static T CastTo<T>(this DataRow row, List<TableSchemaInfo> table_schema)
        {
            var a = (T)Activator.CreateInstance(typeof(T));

            List<string> cols_name = row.Table.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower()).ToList();

            foreach (var t in table_schema)
            {
                if (!cols_name.Contains(t.COLUMN_NAME.ToLower()))
                {
                    continue;
                }

                try
                {
                    object field_data = null;

                    switch (t.DATA_TYPE)
                    {
                        case 5:
                            field_data = row.Field<double>(t.COLUMN_NAME.ToLower()); //"double";
                            break;
                        case 11:
                            field_data = row.Field<bool>(t.COLUMN_NAME.ToLower()); //"bool";
                            break;
                        case 129:
                            field_data = row.Field<string>(t.COLUMN_NAME.ToLower()); //"string";
                            break;
                        case 131:
                            field_data = row.Field<decimal>(t.COLUMN_NAME.ToLower()); //"decimal";
                            break;
                        case 133:
                            field_data = row.Field<DateTime?>(t.COLUMN_NAME.ToLower()); //"DateTime?";
                            break;
                        default:
                            field_data = row.Field<string>(t.COLUMN_NAME.ToLower()); //"string";
                            break;
                    }

                    a.GetType().GetProperty(t.COLUMN_NAME.ToLower()).SetValue(a, field_data, null);
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(" ==> " + ex.Message);
                }
            }

            return a;
        }

        public static List<T> CastTo<T>(this DataTable datatable, List<TableSchemaInfo> table_schema)
        {
            var a = (List<T>)Activator.CreateInstance(typeof(List<T>));

            foreach (DataRow row in datatable.Rows)
            {
                a.Add(row.CastTo<T>(table_schema));
            }

            return a;
        }

        public static bool AddRecord(this DbfContext db_context, string table_name, object obj_to_add)
        {
            try
            {
                OleDbCommand cmd = db_context.Connection.CreateCommand();
                cmd.CommandText = "Insert into " + table_name + " (";

                var obj_props = obj_to_add.GetType().GetProperties().Select(p => p.Name);
                // ** Loop through field name
                int prop_count = 0;
                obj_props.ToList().ForEach(p =>
                {
                    cmd.CommandText += p;
                    if (++prop_count < obj_props.Count())
                        cmd.CommandText += ", ";
                });
                cmd.CommandText += ") ";

                // ** Values section
                cmd.CommandText += "Values (";
                prop_count = 0;
                obj_props.ToList().ForEach(p =>
                {
                    if(obj_to_add.GetType().GetProperty(p).PropertyType == typeof(DateTime?) && obj_to_add.GetType().GetProperty(p).GetValue(obj_to_add, null) == null)
                    {
                        cmd.CommandText += "{}";
                    }
                    else
                    {
                        cmd.CommandText += "?";
                    }

                    if (++prop_count < obj_props.Count())
                        cmd.CommandText += ", ";
                });
                cmd.CommandText += ")";

                //Console.WriteLine(" => " + cmd.CommandText);

                // ** Add parameters section
                cmd.Parameters.Clear();
                obj_props.ToList().ForEach(p =>
                {
                    if(!(obj_to_add.GetType().GetProperty(p).PropertyType == typeof(DateTime?) && obj_to_add.GetType().GetProperty(p).GetValue(obj_to_add, null) == null))
                    {
                        var obj_value = obj_to_add.GetType().GetProperty(p).GetValue(obj_to_add, null);
                        cmd.Parameters.AddWithValue("@" + p, obj_value);
                    }
                });

                db_context.Connection.Open();
                cmd.ExecuteNonQuery();
                db_context.Connection.Close();
                return true;
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
