using MasterProjekat1.Model;
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

namespace MasterProjekat1.View
{
    /// <summary>
    /// Interaction logic for PregledFirme.xaml
    /// </summary>
    public partial class PregledFirme : Window
    {

        private Preduzece _pred;
        public PregledFirme(Preduzece pred)
        {
            InitializeComponent();
            _pred = pred;
            FillTextBoxes();
        }


        private void FillTextBoxes()
        {
            txtUnosNaziv.Text = _pred.Naziv;
            txtUnosNaziv.IsEnabled = false;
            txtAdresa.Text = _pred.Adresa;
            txtAdresa.IsEnabled = false;
            txtPIB.Text = _pred.PIB.ToString();
            txtPIB.IsEnabled = false;
            txtMaticniBr.Text = _pred.MaticniBroj.ToString();
            txtMaticniBr.IsEnabled = false;
            cbPDV.IsChecked = _pred.ObveznikPDV ? true : false;
            txtDelatnost.Text = _pred.SifraDelatnosti;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
