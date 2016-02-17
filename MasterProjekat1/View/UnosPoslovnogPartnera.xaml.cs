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
    /// Interaction logic for UnosPoslovnogPartnera.xaml
    /// </summary>
    public partial class UnosPoslovnogPartnera : Window
    {
        private Preduzece _pred;
        
        public UnosPoslovnogPartnera(Preduzece pred)
        {
            _pred = pred;
            InitializeComponent();
        }

        //dodati dugme sacuvaj i clear(add + clear all), i clear all
        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var DBContext = new DataBaseContext();

                var poslovniPartnerCRUD = new PoslovniPartnerCRUD(DBContext);

                int maticniBroj;
                if (!Int32.TryParse(tbMaticniBroj.Text, out maticniBroj))
                {
                    throw new FormatException("Uneti maticni broj ne sadrzi samo cifre");
                }
                int pib;
                if (!Int32.TryParse(tbPIB.Text, out pib))
                {
                    throw new FormatException("Uneti PIB ne sadrzi samo cifre");
                }
                bool tbPDV = false;

                var poslovniPartner = new PoslovniPartner()
                {
                    Naziv = tbNaziv.Text,
                    Adresa = tbAdresa.Text,
                    MaticniBroj = maticniBroj,
                    PIB = pib,
                    ObveznikPDV = Convert.ToBoolean(tbPDV),
                    SifraDelatnosti = tbSifra.Text,
                    Preduzece_ID = _pred.ID
                };
                
                //MessageBox.Show("Test test " + poslovniPartner.Naziv, poslovniPartner.Adresa);
                poslovniPartnerCRUD.PoslovniPartnerCreate(poslovniPartner);
                MessageBox.Show("Uspesno ste uneli podatke");
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
    }
}
