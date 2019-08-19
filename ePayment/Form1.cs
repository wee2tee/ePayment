using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DbfManager;
using System.Globalization;
using System.IO;
using DbfManager.DbfModel;
using System.Data.OleDb;
using DbfManager;

namespace ePayment
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists("Gen_Class"))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Gen_Class");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            if(fd.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = fd.SelectedPath;

                DbfContext dbf_context = new DbfContext(this.textBox1.Text);
                var filename_list = dbf_context.GetDbfFileName();

                this.checkedListBox1.Items.Clear();
                this.checkedListBox1.Items.Add(new ComboboxItem("*All*", "_all"));
                filename_list.ForEach(f =>
                {
                    this.checkedListBox1.Items.Add(new ComboboxItem(f, f));
                });

                for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
                {
                    //this.checkedListBox1.SetItemChecked(i, true);
                }
            }
        }

        

        private void btnQuery_Click(object sender, EventArgs e)
        {
            DbfContext db_context = new DbfContext(this.textBox1.Text);
            //this.dataGridView1.DataSource = db_context.GetDataBySql(this.txtSql.Text).CastTo<stcrd>(db_context.GetTableSchemaInfoList("stcrd"));
            this.dataGridView1.DataSource = db_context.GetDataBySql<stcrd>(this.txtSql.Text, "stcrd");
            this.lblTotalRow.Text = this.dataGridView1.Rows.Count.ToString();
        }

        private void btnGenClass_Click(object sender, EventArgs e)
        {
            if (this.txtNameSpace.Text.Trim().Length == 0 || this.txtNameSpace.Text.Trim().Contains(" "))
            {
                MessageBox.Show("Please specify Name Space correctly!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.checkedListBox1.CheckedItems.Cast<ComboboxItem>().ToList().ForEach(i =>
            {
                try
                {
                    string class_name = ((string)i.Value).ToLower();

                    DbfContext db_context = new DbfContext(this.textBox1.Text);
                    var data = db_context.GetTableSchemaInfoList(class_name).ToList();
                    //this.dataGridView1.DataSource = data;

                    StreamWriter sw = File.CreateText(@"Gen_Class\" + class_name + ".cs");
                    sw.WriteLine("using System;");
                    sw.WriteLine("using System.Collections.Generic;");
                    sw.WriteLine("using System.Linq;");
                    sw.WriteLine("using System.Text;" + Environment.NewLine);
                    sw.WriteLine("namespace " + this.txtNameSpace.Text.Trim() + Environment.NewLine + "{");
                    sw.WriteLine("\tpublic class " + class_name + " {");

                    data.ForEach(d =>
                    {
                        string data_type = "string";
                        switch (d.DATA_TYPE)
                        {
                            case 5:
                                data_type = "double";
                                break;
                            case 11:
                                data_type = "bool";
                                break;
                            case 129:
                                data_type = "string";
                                break;
                            case 131:
                                data_type = "decimal";
                                break;
                            case 133:
                                data_type = "DateTime?";
                                break;
                            default:
                                data_type = "string";
                                break;
                        }

                        sw.WriteLine("\t\tpublic " + data_type + " " + d.COLUMN_NAME + " { get; set;}");
                    });

                    sw.WriteLine("\t}");
                    sw.WriteLine("}");
                    sw.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnTableSchema_Click(object sender, EventArgs e)
        {
            DbfContext db_context = new DbfContext(this.textBox1.Text);
            var table_schema = db_context.GetTableSchemaInfoList("artrn");
            this.dataGridView1.DataSource = table_schema;
        }

        private void btnDynamicCast_Click(object sender, EventArgs e)
        {
            //DbfContext dbf_context = new DbfContext(this.textBox1.Text);

            //OleDbCommand cmd = dbf_context.Connection.CreateCommand();
            //cmd.CommandText = "Select * from stcrd";

            //DataTable dt = new DataTable();

            //dbf_context.Connection.Open();
            //using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
            //{
            //    da.Fill(dt);
            //}
            //dbf_context.Connection.Close();

            //var x = dt.CastTo<stcrd>(dbf_context.GetTableSchemaInfoList("stcrd"));

            //this.dataGridView1.DataSource = x;

            DbfContext dbf_context = new DbfContext(this.textBox1.Text);

            artrnrm x = new artrnrm
            {
                //docnum = "IV1234567",
                //seqnum = "xxx",
                //remark = "ทดสอบ add remark from outside"
            };
            dbf_context.AddRecord("artrnrm", x);

            armas ar = new armas
            {
                //cuscod = "สบจ",
                //cusnam = "สะเบยเจย",
                //paytrm = 120,
                //lasivc = DateTime.Now,
                //credat = DateTime.Now.AddMonths(-2),
                //chgdat = DateTime.Now.AddMonths(-1).AddDays(+4),
                //inactdat = DateTime.Now.AddDays(3)
            };
            dbf_context.AddRecord("armas", ar);

            MessageBox.Show("Add data completed.");
        }
    }
}
