using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LINQHomewWork
{
    public partial class HomeWork2 : Form
    {
        public HomeWork2()
        {
            InitializeComponent();
            productPhotoTableAdapter1.Fill(awDataSet1.ProductPhoto);
            var q = awDataSet1.ProductPhoto.Select(n => n.ModifiedDate.Year).OrderBy(n=>n).Distinct();
            foreach (int item in q)
            {
                comboBox3.Items.Add(item);
            }
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;

            
            
            
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            var q = awDataSet1.ProductPhoto.Select(n => n);
            dataGridView1.DataSource = q.ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            var q = awDataSet1.ProductPhoto.Where(n =>n.ModifiedDate >= dateTimePicker1.Value 
            && n.ModifiedDate <=dateTimePicker2.Value).Select(n=>n);
            dataGridView1.DataSource = q.ToList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            var q = awDataSet1.ProductPhoto.Where(n => n.ModifiedDate.Year == (int)comboBox3.SelectedItem).Select(n => n);
            dataGridView1.DataSource = q.ToList();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            var q = awDataSet1.ProductPhoto.Where(n => n.ModifiedDate.Month <= ((comboBox2.SelectedIndex+1)*3) && n.ModifiedDate.Month > comboBox2.SelectedIndex*3 ).Select(n => n);
            dataGridView1.DataSource = q.ToList();
             MessageBox.Show($"總共有{q.Count()}筆");
          

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
          
            string a = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            
            var q = awDataSet1.ProductPhoto.Where(n=>n.ProductPhotoID ==int.Parse(a)).Select(n =>n);

            byte[] bytes = q.ToList()[0].ThumbNailPhoto;
           
            MemoryStream ms = new MemoryStream(bytes);
            pictureBox1.Image = Image.FromStream(ms);
            
           


        }
    }
}
