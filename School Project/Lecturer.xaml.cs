using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Data;
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
using Microsoft.VisualBasic;
using System.Windows.Shapes;

namespace School_Project
{
    /// <summary>
    /// Interaction logic for Lecturer.xaml
    /// </summary>
    public partial class Lecturer : MetroWindow
    {
        private int lecturer_id;
        private SqlConnectionDB conn;
        public Lecturer(int lecturer_id)
        {
            InitializeComponent();
            this.lecturer_id = lecturer_id;
            conn = new SqlConnectionDB();
            fillcourses();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            string select_title = "SELECT tch_name FROM teacher where tch_id=" + lecturer_id;
            string select_degree = "SELECT tch_degree FROM teacher WHERE tch_id=" + lecturer_id;
            string dgree = conn.ListOF(select_degree).ElementAt(0);
            if ((dgree.ToLower())=="doctor")
                title.Content = "D." + conn.ListOF(select_title).ElementAt(0);
            else title.Content = "I." + conn.ListOF(select_title).ElementAt(0);
        }
        
        private void fillcourses() {
            string query = "SELECT cource_name from cource,teacher,teachs " +
                "WHERE tch_id=" + lecturer_id + " and tch_id=teacher_id and cource_id=course_id";
            //MessageBox.Show(query);
            coursescombo.ItemsSource = conn.ListOF(query);
        }
        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string course=coursescombo.SelectedItem.ToString();
            string select_course_id = "SELECT cource_id from cource WHERE cource_name='"+course+"'";
            string query = "SELECT std_name FROM students,teacher,cource,enroll " +
                 "WHERE tch_id="+lecturer_id+" and cource_id="+conn.SelectID(select_course_id)+
                 " and tch_id=en_tch_id and cource_id=en_cr_id and std_id=en_std_id";
            studentslist.ItemsSource = conn.ListOF(query);
        }

        private void studentslist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string std = studentslist.SelectedItem.ToString();
            string cr = coursescombo.SelectedItem.ToString();
            try
            {
                double mark = Convert.ToDouble(Interaction.InputBox("Enrer The Mark for the student "+std,"Enter a Mark","The Mark",100,100));
                if (mark < 0 || mark > 100)
                    throw new FormatException();
                string ispass = "";
                if (mark > 60)
                    ispass = "Passed";
                else ispass = "Failed";

                string select_std = "SELECT std_id FROM students WHERE std_name='"+std+"'";
                string select_cr="SELECT cource_id FROM cource WHERE cource_name='"+cr+"'";
                string query = "INSERT INTO marks VALUES("+conn.SelectID(select_std)
                    +","+conn.SelectID(select_cr)+","+mark+",'"+ispass+"')";
                
                if (conn.insertDB(query))
                {
                    MessageBox.Show("Mark inserted");
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Enter a Real Mark");
            }
        }

        private void singout_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            new Window1().Show();
        }

        private void book_btn_Click(object sender, RoutedEventArgs e)
        {
            string cr = coursescombo.SelectedItem.ToString();
            if (book_txt.Text.Length > 0 &&cr.Length>0)
            {
                string select_cr = "SELECT cource_id FROM cource WHERE cource_name='" + cr + "'";
                string query = "UPDATE cource set cource_book='"+ book_txt.Text + "' WHERE cource_id="+conn.SelectID(select_cr);
                
                if (conn.updateDB(query))
                {
                    MessageBox.Show("Book is Added to the course "+cr);
                }
            }
        }

        private void vid_button_Click(object sender, RoutedEventArgs e)
        {
            if(vid_link.Text.Length>0 && vid_name.Text.Length > 0)
            {
                string query = "INSERT INTO videos (vid_name,vid_link) VALUES('"+vid_name.Text+"','"+vid_link.Text+"')";
                if (conn.insertDB(query))
                {
                    MessageBox.Show("Video is Added");
                }
            }
        }
    }
}
