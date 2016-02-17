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
using MasterProjekat1.DBContext;
using MasterProjekat1.Model.ViewModels;
using MasterProjekat1.Model.Adapters;
using Microsoft.Reporting.WinForms;
using CustomDocumentPaginator;

namespace MasterProjekat1.View
{
    /// <summary>
    /// Interaction logic for AnalitickiKontoView.xaml
    /// </summary>
    public partial class AnalitickiKontoView : Window
    {
        private readonly GlavnaKnjiga glavnaKnjiga;
        private readonly DataBaseContext context;

        public AnalitickiKontoView(Preduzece pred)
        {
            InitializeComponent();
            context = new DataBaseContext();
            GlavnaKnjigaCRUD gk = new GlavnaKnjigaCRUD(context);
            glavnaKnjiga = gk.GlavnaKnjigaRead(pred.ID);
            this.Title = "Analitička kartica " + pred.Naziv.ToString();
    
            PocetniDatumPicker.SelectedDate = DateTime.Today;
            KrajnjiDatumPicker.SelectedDate = DateTime.Today;
            //dodati na itemsource
        }

        //pretraga promena po odabranom kontu od datuma do datuma
        private void btnTrazi_Click(object sender, RoutedEventArgs e)
        {
               
            var unetaSifra = KontoTxt.Text;
            var pocetniDatum = PocetniDatumPicker.SelectedDate;
            var krajnjiDatum = KrajnjiDatumPicker.SelectedDate;

            var promenaCRUD = new PromenaCRUD(context);

            var konto = glavnaKnjiga.AnalitickiKonto.FirstOrDefault(ak => ak.Sifra == unetaSifra);

            var promene = promenaCRUD.PromeneRead(konto.ID);            
            promene = promene.Where(p => p.DatumValute >= pocetniDatum && p.DatumValute <= krajnjiDatum).ToList();

            var promeneView = new List<AnalitikaViewModel>();

            foreach (var promena in promene)
            {
                var promenaView = AnalitikaAdapter.promenaDbToPromenaView(promena);
                promeneView.Add(promenaView);
            }

            var dugujeTotal = 0.0m;
            var potrazujeTotal = 0.0m;
            var saldo = 0.0m;
            var dugujeUkupno = 0.0m;
            var potrazujeUkupno = 0.0m;

            //ispis ukupnih vrednosti
            foreach(var promenaView in promeneView)
            {
                dugujeTotal += promenaView.Duguje;
                potrazujeTotal += promenaView.Potrazuje;
                saldo = dugujeTotal - potrazujeTotal;

                //txtSaldoDuguje.Text = Convert.ToString(saldo);

                promenaView.TotalDuguje = dugujeTotal;
                promenaView.TotalPotrazuje = potrazujeTotal;
                
                //ispis salda u zavisnosti od predznaka
                if(saldo >= 0)
                {
                    promenaView.SaldoDuguje = saldo;
                    promenaView.SaldoPotrazuje = 0.0m;
                    txtSaldoDuguje.Text = Convert.ToString(saldo);
                    txtSaldoPotrazuje.Text = Convert.ToString(0.0m);
                }
                else
                {
                    promenaView.SaldoDuguje = 0.0m;
                    promenaView.SaldoPotrazuje = saldo;
                    txtSaldoPotrazuje.Text = Convert.ToString(-1*saldo+"-");
                    txtSaldoDuguje.Text = Convert.ToString(0.0m);
                }

                dugujeUkupno += promenaView.Duguje;
                txtDuguje.Text = Convert.ToString(dugujeUkupno);

                potrazujeUkupno += promenaView.Potrazuje;
                txtPotrazuje.Text = Convert.ToString(potrazujeUkupno);

                txtTotalDuguje.Text = Convert.ToString(dugujeTotal);
                txtTotalPotrazuje.Text = Convert.ToString(potrazujeTotal);
            }

            PromeneDataGrid.ItemsSource = promeneView;
            PromeneDataGrid.Items.Refresh();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();

            if (printDialog.ShowDialog() == false)
            {
                return;
            }

            string documentTitle = "Analiticki konto";

            Size pageSize = new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);

            CustomDataGridDocumentPaginator paginator = new CustomDataGridDocumentPaginator(PromeneDataGrid, documentTitle, pageSize, new Thickness(30, 20, 30, 20));
            printDialog.PrintDocument(paginator, "Grid");

            //printDialog.PrintVisual(PromeneDataGrid, "My First Print Job");
        }

        


    }
}
