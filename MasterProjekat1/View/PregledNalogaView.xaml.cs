using CustomDocumentPaginator;
using MasterProjekat1.DBContext;
using MasterProjekat1.DBTablesCRUD;
using MasterProjekat1.Model;
using MasterProjekat1.Model.Adapters;
using MasterProjekat1.Model.ViewModels;
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
    public partial class PregledNalogaView : Window
    {

        private readonly DataBaseContext context;
        private readonly GlavnaKnjiga glavnaKnjiga;
        private readonly PoslovnaGodina poslovnaGodina;

        public PregledNalogaView(Preduzece pred)
        {
            InitializeComponent();
            this.Title = "Pregled naloga " + pred.Naziv;
            context = new DataBaseContext();
            GlavnaKnjigaCRUD gk = new GlavnaKnjigaCRUD(context);
            glavnaKnjiga = gk.GlavnaKnjigaRead(pred.ID);
            PoslovnaGodinaCRUD pg = new PoslovnaGodinaCRUD(context);
            poslovnaGodina = pg.PoslovnaGodinaRead(glavnaKnjiga.ID, DateTime.Now.Year);
        }

        //pretraga po broju naloga btn
        private void btnTrazi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var unetiBrojNaloga = Convert.ToInt32(BrojNalogaTxt.Text);
                if (unetiBrojNaloga < 0) MessageBox.Show("Uneli ste negativan broj");
                var promenaCRUD = new PromenaCRUD(context);

                var brojNaloga = poslovnaGodina.NalogZaKnjizenje.FirstOrDefault(pg => pg.BrojNaloga == unetiBrojNaloga);

                var vrstaNalogaNaziv = brojNaloga.VrstaNaloga.Naziv;
                txtVrstaNaloga.Text = vrstaNalogaNaziv.ToString();
                lblDatum.Content = brojNaloga.DatumKnjizenja.ToShortDateString(); 
                                
                var promene = promenaCRUD.PromeneReadNalogID(brojNaloga.ID);

                var promeneView = new List<AnalitikaViewModel>();

                foreach (var promena in promene)
                {
                    var promenaView = AnalitikaAdapter.promenaDbToPromenaView(promena);
                    promeneView.Add(promenaView);
                }

                //ispis ukupnih vrednosti
                var dugujeUk = 0.0m;
                var potrazujeUk = 0.0m;

                foreach (var promenaView in promeneView)
                {
                    dugujeUk += promenaView.Duguje;
                    potrazujeUk += promenaView.Potrazuje;

                    lblDuguje.Content = dugujeUk.ToString();
                    lblPotrazuje.Content = potrazujeUk.ToString();
                }

                NalogDataGrid.ItemsSource = promeneView;
                NalogDataGrid.Items.Refresh();

            }catch (FormatException fe)
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

        private void btnIzadji_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();

            if (printDialog.ShowDialog() == false)
            {
                return;
            }

            string documentTitle = "Nalog za knjiženje";

            Size pageSize = new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);

            CustomDataGridDocumentPaginator paginator = new CustomDataGridDocumentPaginator(NalogDataGrid, documentTitle, pageSize, new Thickness(30, 20, 30, 20));
            printDialog.PrintDocument(paginator, "Grid");
        }
    }
}
