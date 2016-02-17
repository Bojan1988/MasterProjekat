using MasterProjekat1.States;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MasterProjekat1.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            this.Title = "Knjigovodstvena agencija";
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
          }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void novaFirma_Click(object sender, RoutedEventArgs e)
        {
            UnosIzmenaFirme u = new UnosIzmenaFirme(new NewFirmaState(), null);
            u.Show();            
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }  
            //https://www.youtube.com/watch?v=plKvM5IPEvU
            //https://www.youtube.com/watch?v=FQ5qtC2Z-rg

        //dopraviti progres bar

        BackgroundWorker worker;
        private void btnListaFirmi_Click(object sender, RoutedEventArgs e)
        {   
            
            if (App.Current.Windows.Count == 2)
            {
                worker = new BackgroundWorker();
                worker.WorkerReportsProgress = true;
                worker.DoWork += (obj, ea) => longtask(10);
                worker.ProgressChanged += new ProgressChangedEventHandler(progressReport);
                worker.RunWorkerAsync();
                worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerComplete);
            }
            //prgBar.Visibility = Visibility.Collapsed;
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            ListaFirmi lf = new ListaFirmi();
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
            //prgBar.Visibility = Visibility.Visible;
            lf.ShowDialog();
        }

        private void longtask(int times)
        {
            for (int i = 0; i < times; i++)
            {
                Thread.Sleep(1000);
                worker.ReportProgress(i * (100 / times));
            }
        }   

        public void progressReport(object sender, ProgressChangedEventArgs e)
        {
            prgBar.Value = e.ProgressPercentage;
        }

        public void workerComplete(object sender, RunWorkerCompletedEventArgs e)
        {
           
        }
       
    }
}
