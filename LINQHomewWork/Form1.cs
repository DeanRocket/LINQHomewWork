using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LINQHomewWork
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ordersTableAdapter1.Fill(nwDataSet1.Orders);
            productsTableAdapter1.Fill(nwDataSet1.Products);

            var q = from o in nwDataSet1.Orders
                    select o.OrderDate.Year;
            foreach (int item in q.Distinct())
            {
                comboBox1.Items.Add(item);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var q = from o in nwDataSet1.Orders
                    where ! o.IsShippedDateNull() && !o.IsShipRegionNull() && !o.IsShipPostalCodeNull()
                    select o;
            dataGridView1.DataSource = q.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var q = from o in nwDataSet1.Orders
                    where  !o.IsShippedDateNull() && !o.IsShipRegionNull() && !o.IsShipPostalCodeNull() && o.ShippedDate.Year == (int)comboBox1.SelectedItem
                    select o;
            dataGridView1.DataSource = q.ToList();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from f in files
                    where f.Extension == ".log"
                    select f;
            dataGridView1.DataSource = q.ToList();


            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from f in files
                    where f.CreationTime.Year <=2021
                    select f;
            dataGridView1.DataSource = q.ToList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from f in files
                    where f.Length >50000
                    select f;
            dataGridView1.DataSource = q.ToList();
        }

      
      public class NewCount {

            private int _count = 0;
            public int count
            {
                
                get
                {
                    return _count;
                }
                set
                {
                    if (value < 0)
                    {
                        _count = 0;
                    }
                    else
                    {
                        _count = value;
                    }
                }
            }
                 }

        NewCount timer = new NewCount();
        private void button12_Click(object sender, EventArgs e)  //上一頁
        {
            timer.count -= 1;
            int i = int.Parse(textBox1.Text);
            dataGridView1.DataSource = null;

            var q = nwDataSet1.Products.Where(n => n.ProductID <= i * timer.count && n.ProductID > i * (timer.count - 1)).Select(n => n);
            dataGridView1.DataSource = q.ToList();

            
        }
        private void button13_Click(object sender, EventArgs e)  //下一頁
        {
            
            int i = int.Parse(textBox1.Text);
            dataGridView1.DataSource = null;
            
            var q = nwDataSet1.Products.Where(n => n.ProductID > i* timer.count && n.ProductID <= i*(timer.count + 1)).Select(n => n);
            dataGridView1.DataSource = q.ToList();

            timer.count += 1;


        }

        private void textBox1_TextChanged(object sender, EventArgs e) //如果重新選擇每頁幾筆 從0開始
        {
            dataGridView1.DataSource = null;
           
               
                var q = nwDataSet1.Products.Where(n => n.ProductID <= 0).Select(n => n);
                dataGridView1.DataSource = q.ToList();

            timer.count = 0;
        }
    }
}
