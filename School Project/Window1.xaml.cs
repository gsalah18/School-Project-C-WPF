using MahApps.Metro.Controls;
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
using MySql.Data.MySqlClient;
using System.Windows.Shapes;

namespace School_Project
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : MetroWindow
    {
        int id;
        string password;
        SqlConnectionDB connection;
        public Window1()
        {
            InitializeComponent();
            connection = new SqlConnectionDB();
            FocusManager.SetFocusedElement(this, textBox);
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void Key(object sender, KeyEventArgs e)
        {
            if (e.Key==System.Windows.Input.Key.Enter)
            {
                Login();
            }
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Go(object sender, RoutedEventArgs e)
        {
            Login();
        }
        private void Login()
        {
            try
            {
                id = Convert.ToInt32(textBox.Text);
                password = passwordBox.Password;
                string query1 = "SELECT * FROM students where std_id=" + id + " and std_password='" + password + "'";
                string query2 = "SELECT * FROM maneger where mang_id=" + id + " and mang_password='" + password + "'";
                string query3= "SELECT * FROM teacher where tch_id=" + id + " and tch_password='" + password + "'";
                if (connection.Login(query1))
                {
                    new Student(id).Show();
                    this.Hide();
                }
                else if (connection.Login(query2))
                {
                    new teacher().Show();
                    this.Hide();
                }else if (connection.Login(query3)){
                    new Lecturer(id).Show();
                    this.Hide();
                }
                else MessageBox.Show("Wrong Id or Number");
            }
            catch (FormatException)
            {
                MessageBox.Show("Enter Correct Values");
            }
        }
    }
}
