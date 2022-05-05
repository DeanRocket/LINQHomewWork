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
    public partial class HomeWork3 : Form
    {
        LINQHomewWork.NorthwindEntities db = new LINQHomewWork.NorthwindEntities();
        IEnumerable<Order> pp;

        IEnumerable<Product> qq;
        IEnumerable<System.IO.FileInfo> ff;
        public HomeWork3()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int[] nums= { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            TreeNode node = null;
            foreach (int item in nums)
            {

                if (treeView1.Nodes[SelectNum(item)] == null)
                {
                    
                node = treeView1.Nodes.Add(SelectNum(item), SelectNum(item));
                    node.Nodes.Add(item.ToString());
                    //MessageBox.Show(treeView1.Nodes[SelectNum(item)].ToString());
                }
                else
                node.Nodes.Add(item.ToString());
            }

          
        }

        private string SelectNum(int n)
        {
            if (n <= 3)
            {return "small"; }
            else if (n <= 7)
            { return "medium"; }
            else
            { return "big"; }
            

        }
        
        private void button38_Click(object sender, EventArgs e)
        {
            ff = null;
            pp = null;
            qq = null;
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();
            
            var q = from f in files
                    group f by WhoIsBig(f.Length) into g
                   select new { g.Key, myGroup = g };
            
            dataGridView1.DataSource = q.ToList();
            
            treeView1.Nodes.Clear();
            TreeNode node;
            ff = files.Where(n => WhoIsBig(n.Length) == dataGridView1.CurrentCell.Value.ToString()).OrderByDescending(n => n.Length);
            dataGridView2.DataSource = ff.ToList();
            foreach (var group in q)
            {
               node = treeView1.Nodes.Add(group.Key);
                foreach (var item in group.myGroup)
                {
                    node.Nodes.Add(item.ToString());
                }
            }
            

        }

        private string WhoIsBig(long f)
        {
            if (f < 100000)
            {
                return "小檔案(小於1MB)";
            }
            else
                return "大檔案(超過1MB)";
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


           


        }

        private void button6_Click(object sender, EventArgs e)
        {
            ff = null;
            pp = null;
            qq = null;
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from f in files
                    group f by YearTime(f.CreationTime.Year) into g
                    select new { g.Key, myGroup = g };

            dataGridView1.DataSource = q.ToList();

            treeView1.Nodes.Clear();
            TreeNode node;
            ff = files.Where(n => YearTime(n.CreationTime.Year) == dataGridView1.CurrentCell.Value).OrderByDescending(n => n.CreationTime.Year).ThenBy(n=>n.CreationTime.Month);
            dataGridView2.DataSource = ff.ToList();
            foreach (var group in q)
            {
                node = treeView1.Nodes.Add(group.Key.ToString());
                foreach (var item in group.myGroup)
                {
                    node.Nodes.Add(item.ToString());
                }
            }

        }

        private object YearTime(int year)
        {
            if (year > 2021)
            {
                return "新檔案";
            }
            else {
                return "舊檔案";
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView2.DataSource = null;
              if (pp != null)
                dataGridView2.DataSource = pp.ToList();
            if (ff != null)
                dataGridView2.DataSource = ff.ToList();
            if (qq != null)
                dataGridView2.DataSource = qq.ToList();
            
        }
        
        
        
       
       

        private void button8_Click(object sender, EventArgs e)
        {
            ff = null;
            pp = null;
            qq = null;
            var q = from p in db.Products.AsEnumerable()
                    where p.UnitPrice != null
                    group p by PriceBig(p.UnitPrice) into g
                    select new {價格分類 = g.Key };

            
            dataGridView1.DataSource = q.ToList();

            qq = from p in db.Products.AsEnumerable()
                 where p.UnitPrice != null && PriceBig(p.UnitPrice) == dataGridView1.CurrentCell.Value.ToString()
                 orderby p.UnitPrice
                 select p;
         
            dataGridView2.DataSource = qq.ToList();    
            


        }

        private string PriceBig(decimal? p)
        {
            if (p < 30)
            {
                return "便宜";
            }
            else if (p < 80)
            {
                return "普通";
            }
            else
            {
                return "貴";

            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            ff = null;
            pp = null;
            qq = null;
            var q = from o in db.Orders.AsEnumerable()
                    group o by o.OrderDate.Value.Year into g
                    select new { 年分 = g.Key };
            dataGridView1.DataSource = q.ToList();
           
            pp = from o in db.Orders.AsEnumerable()
                 where  o.OrderDate.Value.Year == (int)dataGridView1.CurrentCell.Value
                 orderby o.OrderDate.Value.Year
                 select o;
            dataGridView2.DataSource = pp.ToList();

        }

        private void button10_Click(object sender, EventArgs e)
        {
            ff = null;
            pp = null;
            qq = null;
            var q = from o in db.Orders.AsEnumerable()
                    group o by o.OrderDate.Value.ToString("Y") into g
                    select new { 訂單時間 = g.Key };
            dataGridView1.DataSource = q.ToList();

             pp = from o in db.Orders.AsEnumerable()
                 where o.OrderDate.Value.ToString("Y") == dataGridView1.CurrentCell.Value.ToString()
                 orderby o.OrderDate.Value.Year,o.OrderDate.Value.Month
                 select o;
            dataGridView2.DataSource = pp.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var q = db.Order_Details.AsEnumerable().Sum(n => (float)n.UnitPrice * (1 - n.Discount) * n.Quantity);
            MessageBox.Show($"總金額{q:C2}");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var q = (from o in db.Order_Details.AsEnumerable()
                    group o by $"{o.Order.Employee.LastName}{o.Order.Employee.FirstName}" into g
                    let a = g.Sum(n => (float)n.UnitPrice * (1 - n.Discount) * n.Quantity)
                    orderby a descending
                    select  new { Name = g.Key, TotalPrice = $" {a:C2}" }).Take(5);
                    

            dataGridView1.DataSource = q.ToList();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var q = (from p in db.Products

                     orderby p.UnitPrice descending
                     select new { p.Category.CategoryName, p.ProductID, p.ProductName, p.UnitPrice }).Take(5);
            dataGridView1.DataSource = q.ToList();
           
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var q = db.Products.Any(n => n.UnitPrice > 300);
            MessageBox.Show("有沒有大於價錢300的產品:"+q);
        }
    }
    
}

