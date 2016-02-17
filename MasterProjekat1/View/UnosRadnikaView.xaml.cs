using MasterProjekat1.DBContext;
using MasterProjekat1.DBTablesCRUD;
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
    /// Interaction logic for UnosRadnikaView.xaml
    /// </summary>
    public partial class UnosRadnikaView : Window
    {
        private Preduzece pred;

        public UnosRadnikaView()
        {
            InitializeComponent();
        }

        Preduzece _pred;
        
        public UnosRadnikaView(Preduzece pred)
        {
            // TODO: Complete member initialization
            _pred = pred;
            InitializeComponent();
        }

        //dodati dugme sacuvaj i clear(add + clear all), i clear all
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int jmbg;
            if (!Int32.TryParse(txtJmbg.Text, out jmbg ))
            {
                throw new FormatException("Uneti JMBG ne sadrzi samo cifre");
            }
            int brojSati;
            if (!Int32.TryParse(txtBrojSati.Text, out brojSati))
            {
                throw new FormatException("Uneti broj sati ne sadrzi samo cifre");
            }
            int brojBodova;
            if (!Int32.TryParse(txtBrojBodova.Text, out brojBodova))
            {
                throw new FormatException("Uneti PIB ne sadrzi samo cifre");
            }

            try
            {
                var DBContext = new DataBaseContext();
                var radnikCRUD = new RadnikCRUD(DBContext);
                var radnik = new Radnik()
                {
                    Ime = txtIme.Text,
                    Prezime = txtPrezime.Text,
                    JMBG = jmbg,
                    StrucnaSprema = txtSprema.Text,
                    SifraRadnika = txtSifra.Text,
                    DatumZaposlenja = Convert.ToDateTime(datumZaposlenjaPicker.SelectedDate),
                    BrojRadnihSati = brojSati,
                    BrojBodova = brojBodova,
                    Preduzece_ID = _pred.ID
                };

                radnikCRUD.RadnikCreate(radnik);
                MessageBox.Show("Uspešno ste uneli podatke");
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

            this.Close();
        }

        public Radnik radnik { get; set; }
    }
}
