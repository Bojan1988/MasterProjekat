using MasterProjekat1.DBContext;
using MasterProjekat1.DBTablesCRUD;
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
    /// Interaction logic for ProveraNalogaView.xaml
    /// </summary>
    public partial class ProveraNalogaView : Window
    {
        public ProveraNalogaView(NalogZaKnjizenje nalogZaKnjizenje)
        {
            InitializeComponent();

            var _nalogZaKnjizenje = nalogZaKnjizenje;

        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            //var DBContext = new DataBaseContext();
            //var nalogZaKnjizenjeCRUD = new NalogCRUD(DBContext);
            //nalogZaKnjizenjeCRUD.NalogCreate(nalogZaKnjizenje);
            //MessageBox.Show("Uspesno ste uneli podatke!");
            
            this.Close();

            //UnosNalogaView unos = new UnosNalogaView(new UnosNalogaState(), _pred);
            //unos.Show();
        }
    }
}
