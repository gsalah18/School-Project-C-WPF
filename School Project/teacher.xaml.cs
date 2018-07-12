using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.Data;
using MahApps.Metro.Controls;

namespace School_Project
{
    /// <summary>
    /// Interaction logic for teacher.xaml
    /// </summary>
    public partial class teacher : MetroWindow
    {
        SqlConnectionDB conn;
        public teacher()
        {
            InitializeComponent();
            conn = new SqlConnectionDB();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }
        
        

        private void add_student_Click(object sender, RoutedEventArgs e)
        {
            try {
                int Id = Convert.ToInt32(Id_Txt.Text);
                string name = Name_Txt.Text;
                int Card = Convert.ToInt32(Card_Txt.Text);
                string major = Majoe_Txt.Text;
                int Year = Convert.ToInt32(BYear_Txt.Text);
                if (full(name) && full(major))
                {
                    string query = "Insert Into students values (" + Id + "," + "'" + name + "','" + Card + "'" +
                        "," + Card + ",'" + major + "'," + Year + ")";
                    if (conn.insertDB(query))
                        MessageBox.Show("تم اضافه الطالب");

                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Id and Id_Card and Birth year must be Integers");
            }
            catch (OverflowException)
            {
                MessageBox.Show("ID Card is Wrong");
            }

        }
        private Boolean full(string str)
        {
            return str.Length > 0;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string id = textBox.Text;
            string name = textBox1.Text;
            if (full(id) && full(name))
            {
                string query = "Insert Into cource (cource_id,cource_name) values (" + id + "," + "'" + name + "')";
                if (conn.insertDB(query))
                    MessageBox.Show("تم اضافه الماده");

            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string t_id = tch_id.Text;
            string t_name = tch_name.Text;
            string t_dgree = tch_degree.Text;
           
            if (full(t_dgree) && full(t_name) &&full(t_id))
            {
                string query = "Insert Into teacher (tch_id,tch_name,tch_degree) values (" + t_id + "," + "'" + t_name + "','"+ t_dgree+"')";
                if (conn.insertDB(query))
                    MessageBox.Show("تم اضافه الاستاذ");
            }

        }

        private void TabChanged(object sender, SelectionChangedEventArgs e)
        {
            if (teachs.IsSelected)
            {
                string teacher = "SELECT tch_name from teacher";
                string course = "SELECT cource_name from cource";
                teachers.ItemsSource = conn.ListOF(teacher);
                courses.ItemsSource = conn.ListOF(course);

            }
        }

        private void singout_Click(object sender, RoutedEventArgs e)
        {
            new Window1().Show();
            this.Hide();
        }

        private void AddTeachs_Click(object sender, RoutedEventArgs e)
        {
            if (teachers.SelectedIndex >=0 && courses.SelectedIndex >=0)
            {
                string str_teacher = teachers.SelectedItem.ToString();
                string str_course = courses.SelectedItem.ToString();
                string select_tch ="SELECT tch_id from teacher where tch_name='"+str_teacher+"'";
                string select_cr = "SELECT cource_id from cource where cource_name='" + str_course + "'";
                int tch_id = conn.SelectID(select_tch);
                int course_id = conn.SelectID(select_cr);
                string exist_query = "SELECT * FROM teachs WHERE teacher_id="+tch_id+" and course_id="+course_id+"";
                string query = "INSERT INTO teachs VALUES("+tch_id+","+course_id+")";
                if (!conn.DoseExists(exist_query))
                {
                    if (conn.insertDB(query))
                        MessageBox.Show("Information is Added");
                }
                else MessageBox.Show("This teacher already teachs the course");
                //MessageBox.Show(tch_id + " " + course_id);
            }
        }
    }
}
