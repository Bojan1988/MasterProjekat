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
using MasterProjekat1.Model;
using MasterProjekat1.States;

namespace MasterProjekat1.View
{
    /// <summary>
    /// Interaction logic for unosFirme.xaml
    /// </summary>
    public partial class UnosIzmenaFirme : Window
    {
        private IState _state;
        private Preduzece _pred;
      
        public UnosIzmenaFirme(IState state, Preduzece pred)
        {
            InitializeComponent();
            _state = state;
            _pred = pred;
            this.Title = _state.WindowTitle;
            if (_state.GetType().Equals(typeof(EditFirmaState)))
            {
                FillTextBoxesForEdit();
                HideUnnecesaryComponents();
                
            }

        }

        private void FillTextBoxesForEdit()
        {
            txtUnosNaziv.Text = _pred.Naziv;
            txtAdresa.Text = _pred.Adresa;
            txtPIB.Text = _pred.PIB.ToString();
            txtPIB.IsEnabled = false; 
            txtMaticniBr.Text = _pred.MaticniBroj.ToString();
            txtMaticniBr.IsEnabled = false;
            cbPDV.IsChecked = _pred.ObveznikPDV ? true : false;
            txtDelatnost.Text = _pred.SifraDelatnosti;
        }

        private void HideUnnecesaryComponents()
        {
            lblEmail.Visibility = Visibility.Visible;
            txtEmail.Text = "";
            txtEmail.IsEnabled = false;

            lblRacun.Visibility = Visibility.Visible;
            txtRacun.IsEnabled = false;
            txtRacun.Text = "";

            lblTelefon.Visibility = Visibility.Visible;
            txtTelefon.IsEnabled = false;
            txtTelefon.Text = "";
           
            InvalidateVisual();
            
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_pred == null)
                {
                    _pred = CreatePreduzece();
                }
                else
                {
                    EditPreduzece();
                }
                _state.Submit(_pred);
            }
            catch (FormatException fe) 
            {
                MessageBox.Show(fe.Message);
            }
            catch (ArgumentNullException ane)
            {
                MessageBox.Show(ane.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          }

        private void EditPreduzece()
        {
            bool isObveznik;
            if (cbPDV.IsChecked.HasValue)
            {
                isObveznik = cbPDV.IsChecked.Value;
            }
            else
            {
                throw new ArgumentNullException("Checkbox obveznika nije inicijalizovan");
            }
            _pred.Naziv = txtUnosNaziv.Text;
            _pred.Adresa = txtAdresa.Text;
            _pred.ObveznikPDV = isObveznik;
            _pred.SifraDelatnosti = txtDelatnost.Text;
        }

        private Preduzece CreatePreduzece()
        {
            int pib;
            if (!Int32.TryParse(txtPIB.Text, out pib))
            {
                throw new FormatException("Uneti PIB ne sadrzi samo cifre");
            }

            int maticniBroj;
            if (!Int32.TryParse(txtMaticniBr.Text, out maticniBroj))
            {
                throw new FormatException("Uneti maticni broj ne sadrzi samo cifre");
            }

            bool isObveznik;
            if (cbPDV.IsChecked.HasValue)
            {
                isObveznik = cbPDV.IsChecked.Value;
            }
            else
            {
                throw new ArgumentNullException("Checkbox obveznika nije inicijalizovan");
            }

            Preduzece pred = new Preduzece
            {
                Naziv = txtUnosNaziv.Text,
                PIB = pib,
                Adresa = txtAdresa.Text,
                MaticniBroj = Convert.ToInt32(maticniBroj),
                ObveznikPDV = isObveznik,
                SifraDelatnosti = txtDelatnost.Text,
                BrojNaloga = 0
            };



            Telefon telefon = new Telefon
            {
                Broj = txtTelefon.Text,
                Preduzece = pred
            };

            Email email = new Email
            {
                EmailAdresa = txtEmail.Text,
                Preduzece = pred
            };

            Racun racun = new Racun
            {
                BrojRacuna = Convert.ToInt32(txtRacun.Text),
                Preduzece = pred
            };

            ((List<Racun>)pred.Racuni).Add(racun);
            ((List<Email>)pred.Emailovi).Add(email);
            ((List<Telefon>)pred.Telefoni).Add(telefon);
            return pred;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            txtUnosNaziv.Text = "";
            txtAdresa.Text = "";
            txtPIB.Text = "";            
            txtEmail.Text = "";
            txtRacun.Text = "";
            txtTelefon.Text = "";
            txtMaticniBr.Text = "";
            cbPDV.IsChecked = false;
            txtDelatnost.Text = "";
        }
        //sacuvaj i izadji dugme, nakon cuvanja nove firme skace na listu firmi
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_pred == null)
                {
                    _pred = CreatePreduzece();
                }
                else
                {
                    EditPreduzece();
                }
                _state.Submit(_pred);
            }
            catch (FormatException fe)
            {
                MessageBox.Show(fe.Message);
            }
            catch (ArgumentNullException ane)
            {
                MessageBox.Show(ane.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            ListaFirmi w = new ListaFirmi();
            w.Show();
            this.Close();
        }
    }
}
