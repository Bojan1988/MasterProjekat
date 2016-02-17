using MasterProjekat1.DBContext;
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
    /// Interaction logic for UnosNazivaKontaView.xaml
    /// </summary>
    public partial class UnosNazivaKontaView : Window
    {
        DataBaseContext dbc;
        private GlavnaKnjiga glavnaKnjiga;
        private string value;

        public UnosNazivaKontaView(GlavnaKnjiga glavnaKnjiga, string value)
        {
            
            InitializeComponent();
            this.glavnaKnjiga = glavnaKnjiga;
            this.value = value;
          
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            var novaAnalitika = new AnalitickiKonto();
            novaAnalitika.Naziv = value;
            

            using (dbc)
            {
                dbc.AnalitickaKonta.Add(novaAnalitika);
                dbc.SaveChanges();
            }
            
           //sacuvati vrednost naziva u analiticki konto
            this.Close();
            
        }
    }
}
