using MasterProjekat1.DBContext;
using MasterProjekat1.Model;
using MasterProjekat1.States;
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
    /// Interaction logic for FirmaView.xaml
    /// </summary>
    public partial class FirmaView : Window
    {

        private IState _state;
        private readonly Preduzece _pred;

        public FirmaView(IState state, Preduzece pred)
        {
            _state = state;
            _pred = pred;

            InitializeComponent();
            
            try
            {
                this.Title = "Firma " + _pred.Naziv.ToString() + " pregled";
                //MessageBox.Show(String.Format("FirmaView {0} {1}", pred.Naziv.ToString(), _state.ToString()));
            }
            catch (EncoderFallbackException e)
            {
                MessageBox.Show("Greska, errorlist " + e);
            }


            lblChange(pred);
        }

        public Preduzece PredSel
        {
            get { return _pred; }
        }


        public void lblChange(Preduzece pred)
        {
            lblNazivFirme.Content = pred.Naziv;
        }

        private void itmUnosNaloga_Click(object sender, RoutedEventArgs e)
        {
            Preduzece selected = _pred;
            UnosNalogaView unv = new UnosNalogaView(new UnosNalogaState(), _pred);
            unv.Show();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            AnalitickiKontoView ak = new AnalitickiKontoView(_pred);
            ak.Show();
        }

        
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            
            PregledNalogaView pnv = new PregledNalogaView(_pred);
            pnv.Show();
        }
        
    }

}
