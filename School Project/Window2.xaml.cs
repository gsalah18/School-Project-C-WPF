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
using System.Windows.Shapes;

namespace School_Project
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : MetroWindow
    {
        private Uri link;
        public Window2(Uri link)
        {
            InitializeComponent();
            this.link = link;

            web.Navigate(link);
            
            
        
        }

     
        private void Clossing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            web.Dispose();
        }
    }
}
