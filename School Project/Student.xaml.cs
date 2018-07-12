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
    public partial class Student : MetroWindow
    {
        SqlConnectionDB conn;
        int id;
        public Student(int id)
        {
            InitializeComponent();
            this.id = id;
            
            conn = new SqlConnectionDB();
            string vid_query = "SELECT vid_name FROM videos";
            listView.ItemsSource = conn.ListOF(vid_query);
            Info.IsSelected = true;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            string query = "SELECT vid_link FROM videos where vid_name='"+listView.SelectedItem+"'";
            try {
                Uri link = new Uri(conn.ListOF(query).ElementAt(0));
                new Window2(link).Show();
            }
            catch (UriFormatException)
            {
                MessageBox.Show("Invalid Link");
            }
        }

        private void ItemCliked(object sender, MouseButtonEventArgs e)
        {
            ListView list = (ListView)sender;
        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Info.IsSelected)
            {
               
                String Query = "select * from students where std_id =" + id;
                SqlConnectionDB.connection.Open();
                MySqlCommand cmd = new MySqlCommand(Query, SqlConnectionDB.connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                Id_label.Content = "" + id;
                Name_Label.Content = reader.GetString(1);
                identity.Content = reader.GetString(3);
                Major_label.Content = reader.GetString(4);
                Year_label.Content = reader.GetString(5);
                SqlConnectionDB.connection.Close();
            }
            else if (Marks.IsSelected )
            {
                
                string query = "select cource_name as Course,mark_value as Mark,pass as State from marks,cource,students where std_id=mark_std_id and cource_id=mark_cource_id and mark_std_id="+id;
                DataSet data = new DataSet();
                conn.selectDB(ref data, query);
                markstable.ItemsSource = data.Tables[0].DefaultView;
                SqlConnectionDB.connection.Open();
                string avg = "SELECT AVG(mark_value) from marks,cource,students where std_id=mark_std_id and cource_id=mark_cource_id and std_id=" + id;
                
                MySqlCommand cmd = new MySqlCommand(avg, SqlConnectionDB.connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                    if (!reader.IsDBNull(0))
                        av.Content = reader.GetString(0) + "";
                
                    
                SqlConnectionDB.connection.Close();

            }
            if (courses.IsSelected)
            {
                DataSet addcourse_data = new DataSet();
                string addcourse_query = "select cource_name as Course,tch_name as Teacher from cource,teacher,teachs where cource_id=course_id and tch_id=teacher_id";
                conn.selectDB(ref addcourse_data, addcourse_query);
                addcourse.ItemsSource = addcourse_data.Tables[0].DefaultView;
                RefreshData();
            }
        }

        
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            
            string Query = "select std_password from students where std_id =" + id+" and std_password='"+oldpass.Password+"'";
            
            if (conn.DoseExists(Query) && newpass.Password.Equals(newpass1.Password))
            {
                string update = "update students set std_password='" + newpass.Password + "' where std_id=" + id;
                if (conn.updateDB(update))
                {
                    MessageBox.Show("Password is Updated");

                }
            }
            else MessageBox.Show("Worong Password or The new Password Not The Same");

        }

        private void singout_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            new Window1().Show();
        }

        private void addcourse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try {
                try {
                    DataRowView row = (DataRowView)addcourse.SelectedItem;
                    if (MessageBox.Show("Do U want to Add the Course " + row.Row[0] + " With the Teacher " + row.Row[1], "Alert", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        string selectcourse = "Select cource_id from cource where cource_name='" + row.Row[0] + "'";
                        string selecttch = "Select tch_id from teacher where tch_name='" + row.Row[1] + "'";
                        string query = "INSERT INTO enroll VALUES(" + id + "," + conn.SelectID(selectcourse) + "," + conn.SelectID(selecttch) + ")";
                        string exists_query = "SELECT * FROM enroll where en_std_id=" + id + " and en_cr_id=" + conn.SelectID(selectcourse) + " and en_tch_id=" + conn.SelectID(selecttch) + "";
                        if (!conn.DoseExists(exists_query))
                        {
                            if (conn.insertDB(query))
                            {
                                MessageBox.Show("Course is Added");
                                RefreshData();
                            }
                        } else MessageBox.Show("this course is already enrolled");
                    }
                }catch(IndexOutOfRangeException)
                {

                }
            }
            catch (NullReferenceException)
            {

            }
        }
        private void RefreshData()
        {
            string mycourses_query = "SELECT cource_name as Course,tch_name as Teacher,'The Book' as EBook " +
                    "from students,cource,teacher,enroll WHERE " +
                    "std_id=" + id + " and std_id=en_std_id " +
                    "and cource_id=en_cr_id and tch_id=en_tch_id ";
          
            DataSet mycourses_data = new DataSet();
            conn.selectDB(ref mycourses_data, mycourses_query);
            mycourses.ItemsSource = mycourses_data.Tables[0].DefaultView;
        }
        
        private void mycourses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (mycourses.CurrentColumn.Header.ToString() == "EBook")
            {
                try {
                    string cr = ((DataRowView)e.AddedItems[0]).Row[0].ToString();
                    
                    string selectcourse = "Select cource_id from cource where cource_name='" + cr + "'";
                    string query = "Select cource_book from cource where cource_id=" + conn.SelectID(selectcourse);
                    //MessageBox.Show(conn.ListOF(query).ElementAt(0));
                    try {
                        Uri link = new Uri(conn.ListOF(query).ElementAt(0));
                        new Window2(link).Show();
                    }
                    catch (UriFormatException)
                    {
                        MessageBox.Show("Book is not here Yet");
                    }
                    catch (ArgumentException) {

                   }
                
                }
                catch (IndexOutOfRangeException)
                {

                }
            }
        }
    }

}


        
     
