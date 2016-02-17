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
using MasterProjekat1.DBContext;
using System.Collections.ObjectModel;
using MasterProjekat1.Model;
using MasterProjekat1.States;

namespace MasterProjekat1.View
{
    /// <summary>
    /// Interaction logic for ListaFirmi.xaml
    /// </summary>
    public partial class ListaFirmi : Window
    {
        public ObservableCollection<string> _imenaFirmi { get; set; }
        public ListaFirmi()
        {
            InitializeComponent();
            listBoxNazivi.ItemsSource = _imenaFirmi;
            _imenaFirmi = new ObservableCollection<string>();
            using (DataBaseContext pre = new DataBaseContext())
            {
                var imenaFirmiList = pre.Preduzeca.Select(p => p.Naziv).ToList();
                foreach (var ime in imenaFirmiList)
                    _imenaFirmi.Add(ime);
            }
            listBoxNazivi.ItemsSource = _imenaFirmi;
            listBoxNazivi.UpdateLayout();
        }


        //dugme knjizenje
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxNazivi.SelectedIndex != -1)
            {
                Preduzece selected;
                using (DataBaseContext dbc = new DataBaseContext())
                {
                     selected = dbc.Preduzeca.Where(p => p.Naziv == listBoxNazivi.SelectedItem.ToString()).FirstOrDefault();
                     FirmaView fv = new FirmaView(new FirmaViewState(), selected);
                     fv.Show();
                     this.Close();
                }
            }
            else
            {
                MessageBox.Show("Niste odabrali preduzece.");
            }
        }

        //dugme izmeni podatke
        public void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (listBoxNazivi.SelectedIndex != -1)
            {
                Preduzece selected;
                using (DataBaseContext dbc = new DataBaseContext())
                {
                    selected = dbc.Preduzeca.Where(p => p.Naziv == listBoxNazivi.SelectedItem.ToString()).FirstOrDefault();
                    UnosIzmenaFirme u = new UnosIzmenaFirme(new EditFirmaState(), selected);
                    u.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Niste odabrali preduzece.");
            }
            
        }



        //dugme pocetna
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }




        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            UnosIzmenaFirme u = new UnosIzmenaFirme(new NewFirmaState(), null);
            u.Show();
            this.Close();
        }

        private void btnPregledPodataka_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxNazivi.SelectedIndex != -1)
            {
                Preduzece selected;
                using (DataBaseContext dbc = new DataBaseContext())
                {
                    selected = dbc.Preduzeca.Where(p => p.Naziv == listBoxNazivi.SelectedItem.ToString()).FirstOrDefault();
                    PregledFirme pf = new PregledFirme(selected);
                    pf.Show();
                    this.Close();
                }
            }
            else
            {
               MessageBox.Show("Niste odabrali preduzece");
            }
        }

       
    }

}
