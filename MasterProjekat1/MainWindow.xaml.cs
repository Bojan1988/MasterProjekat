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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MasterProjekat1.View;
using MasterProjekat1.DBContext;


namespace MasterProjekat1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Title = "Knjigovodstvena agencija";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //if (LogNameBox.Text == "Bojan" && LogPassBox.Password == "pass")
            //{
               // LogNameBox.Text = "Hvala";
                //MessageBox.Show("Dobrodosli!");
                Window1 w = new Window1();
                w.Show();
                this.Close();

            //}
        }

    }
}
