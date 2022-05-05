using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqLabs
{
    public partial class Frm考試 : Form
    {
        public Frm考試()
        {
            InitializeComponent();

            students_scores = new List<Student>()
                                         {
                                            new Student{ Name = "aaa", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Male"},
                                            new Student{ Name = "bbb", Class = "CS_102", Chi = 80, Eng = 80, Math = 100, Gender = "Male" },
                                            new Student{ Name = "ccc", Class = "CS_101", Chi = 60, Eng = 50, Math = 75, Gender = "Female" },
                                            new Student{ Name = "ddd", Class = "CS_102", Chi = 80, Eng = 70, Math = 85, Gender = "Female" },
                                            new Student{ Name = "eee", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Female" },
                                            new Student{ Name = "fff", Class = "CS_102", Chi = 80, Eng = 80, Math = 80, Gender = "Female" },

                                          };
            
            
            
            
            
            
           

        }

        List<Student> students_scores;
        List<MyScore> random_scores = new List<MyScore>();

        public class MyScore
        {
            public string Name { get; set; }
            public int Score { get; set; }

        }

        public class Student
        {
            public string Name { get; set; }
            public string Class { get;  set; }
            public int Chi { get; set; }
            public int Eng { get; internal set; }
            public int Math { get;  set; }
            public string Gender { get; set; }

            
        }
      

        private void button36_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            chart1.Series.Add("1");
            chart1.Series.Add("2");
            chart1.Series.Add("3");
            var q = from s in students_scores
                    select s;
            
            chart1.DataSource = q.ToList();
            chart1.Series[0].XValueMember = "Name";
            chart1.Series[0].YValueMembers = "Chi";
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series[0].Name = "國文成績";
            chart1.Series[1].XValueMember = "Name";
            chart1.Series[1].YValueMembers = "Eng";
            chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series[1].Name = "英文成績";
            chart1.Series[2].XValueMember = "Name";
            chart1.Series[2].YValueMembers = "MATH";
            chart1.Series[2].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series[2].Name = "數學成績";

        }

        private void button37_Click(object sender, EventArgs e)
        {
            //個人 sum, min, max, avg
            chart1.Series.Clear();
           
            chart1.Series.Add("1");
            chart1.Series.Add("2");
            chart1.Series.Add("3");
            chart1.Series.Add("4");
           
            

            //各科 sum, min, max, avg
            var q = from s in students_scores
                    let Max = new int[] { s.Chi, s.Eng, s.Math }.Max()
                    let Min = new int[] { s.Chi, s.Eng, s.Math }.Min()
                    let Average = new int[] { s.Chi, s.Eng, s.Math }.Average()
                    select new { Name = s.Name, TotalScore = (s.Math + s.Chi + s.Eng) ,Max ,Min,Average};
            chart1.DataSource = q.ToList();
            chart1.Series[0].XValueMember = "Name";
            chart1.Series[0].YValueMembers = "TotalScore";
            chart1.Series[0].Name = "總分";
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series[1].XValueMember = "Name";
            chart1.Series[1].YValueMembers = "Max";
            chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series[1].Name = "最高分";
            chart1.Series[2].XValueMember = "Name";
            chart1.Series[2].YValueMembers = "Min";
            chart1.Series[2].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series[2].Name = "最低分";
            chart1.Series[3].XValueMember = "Name";
            chart1.Series[3].YValueMembers = "average";
            chart1.Series[3].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series[3].Name = "平均值";
        }
        IEnumerable<MyScore> q2 = null;
        private void button33_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();

            chart1.Series.Add("1");
            random_scores.Clear();
            Random random = new Random();
            for(int i = 0; i < 100; i++)
            {
                random_scores.Add(new MyScore());

                random_scores[i].Name = (i + 1).ToString();
                random_scores[i].Score = random.Next(0, 100);
                Application.DoEvents();
            }

            var q = from n in random_scores.AsEnumerable()
                    group n by ScoreClass(n.Score) into g
                    select new { name = g.Key, 人數 = g.Count() };
            


            chart1.DataSource = q.ToList();
            dataGridView1.DataSource = q.ToList();
            chart1.Series[0].XValueMember = "name";
            chart1.Series[0].YValueMembers = "人數";
            chart1.Series[0].Name = "分數群組";

            // split=> 分成 三群 '待加強'(60~69) '佳'(70~89) '優良'(90~100) 
            // print 每一群是哪幾個 ? (每一群 sort by 分數 descending)
            q2 = from n in random_scores.AsEnumerable()
                 where ScoreClass(n.Score) == dataGridView1.CurrentCell.Value.ToString()
                 orderby n.Score descending
                     select n;

            dataGridView2.DataSource = q2.ToList();

        }

        private string ScoreClass(int score)
        {
            if (score < 60)
            {
                return "不及格";
            }
            else if (score >= 60 && score < 70)
            {
                return "待加強";
            }
            else if (score >= 70 && score < 90)
            {
                return "佳";
            }
            else 
            {
                return "優良";
            }
        }

        private void button35_Click(object sender, EventArgs e)
        {
            // 統計 :　所有隨機分數出現的次數/比率; sort ascending or descending
            // 63     7.00%
            // 100    6.00%
            // 78     6.00%
            // 89     5.00%
            // 83     5.00%
            // 61     4.00%
            // 64     4.00%
            // 91     4.00%
            // 79     4.00%
            // 84     3.00%
            // 62     3.00%
            // 73     3.00%
            // 74     3.00%
            // 75     3.00%
        }

        private void button34_Click(object sender, EventArgs e)
        {
            LINQHomewWork.NorthwindEntities db = new LINQHomewWork.NorthwindEntities();

            // 年度最高銷售金額 年度最低銷售金額
            // 那一年總銷售最好 ? 那一年總銷售最不好 ?  
            var q = (from o in db.Order_Details.AsEnumerable()
                    group o by o.Order.OrderDate.Value.Year into g
                    let a = g.Sum(n => n.Quantity * (float)n.UnitPrice * (1 - n.Discount))
                    orderby a descending
                    select new {銷售最好=g.Key ,年銷售 = $"{a:c2}" }).Take(1);
            
            
            dataGridView1.DataSource = q.ToList();
            // 那一個月總銷售最好 ? 那一個月總銷售最不好 ?

            // 每年 總銷售分析 圖
            // 每月 總銷售分析 圖
        }

        private void chart1_Click(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView2.DataSource = q2.ToList();
        }
    }
}
