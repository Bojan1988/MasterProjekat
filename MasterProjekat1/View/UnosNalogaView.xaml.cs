using MasterProjekat1.DBContext;
using MasterProjekat1.DBTablesCRUD;
using MasterProjekat1.Enums;
using MasterProjekat1.Model;
using MasterProjekat1.Model.Adapters;
using MasterProjekat1.Model.ViewModels;
using MasterProjekat1.States;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;



namespace MasterProjekat1.View
{
    /// <summary>
    /// Interaction logic for UnosNalogaView.xaml
    /// </summary>
    public partial class UnosNalogaView : Window, INotifyPropertyChanged 
    {
        private IState _state;
        private Preduzece _pred;
        private ObservableCollection<PromenaViewModel> promeneProp;
        private DataBaseContext dbc;
        private PromenaViewModel selektovanaPromena;

        public UnosNalogaView(IState state, Preduzece pred)
        {
            InitializeComponent();
            _state = state;
            _pred = pred;
            dbc = new DataBaseContext();
            this.Title = "Unos naloga "+_pred.Naziv.ToString();
            int redniBroj = 1;
            promeneProp = new ObservableCollection<PromenaViewModel>();
            promeneProp.CollectionChanged += promeneProp_CollectionChanged;
            promeneProp.Add(new PromenaViewModel(_pred) {  RedniBroj = redniBroj });
            dgPromene.ItemsSource = PromeneProp;
            

            //var brNal = pred.BrojNaloga+1;
            //txtBrNaloga.Text = brNal.ToString();
            //txtBrNaloga.Text = _pred.BrojNaloga.ToString();

            dgPromene.KeyUp += (object sender, KeyEventArgs e) =>
            {
                if (e.Key == Key.Enter)
                {
                    redniBroj += 1;
                    dgPromene.Items.Refresh();
                    promeneProp.Add(new PromenaViewModel(pred) { RedniBroj = redniBroj });
                    dgPromene.Items.Refresh();

                    var dugujeUk = 0.0m;
                    var potrazujeUk = 0.0m;
                    
                    foreach(var promena in promeneProp)
                    {
                        dugujeUk += promena.Duguje;
                        potrazujeUk += promena.Potrazuje;
                    }
                   
                    DugujeUkupnoLbl.Content = Convert.ToString(dugujeUk);
                    PotrazujeUkupnoLbl.Content = Convert.ToString(potrazujeUk);                  
                }
            };

             
        }

        private void promeneProp_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (PromenaViewModel item in e.OldItems)
                {
                    //Removed items
                    item.PropertyChanged -= EntityViewModelPropertyChanged;
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (PromenaViewModel item in e.NewItems)
                {
                    //Added items
                    item.PropertyChanged += EntityViewModelPropertyChanged;
                }
            }
        }

        public void EntityViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //This will get called when the property of an object inside the collection changes
            if (e.PropertyName == "Potrazuje" || e.PropertyName == "Duguje")
            {
                var dugujeUk = 0.0m;
                var potrazujeUk = 0.0m;

                foreach (var promena in promeneProp)
                {
                    dugujeUk += promena.Duguje;
                    potrazujeUk += promena.Potrazuje;
                }

                DugujeUkupnoLbl.Content = Convert.ToString(dugujeUk);
                PotrazujeUkupnoLbl.Content = Convert.ToString(potrazujeUk);
            }
        }

        public PromenaViewModel SelektovanaPromena
        {
            get
            {
                return selektovanaPromena;
            }

            set
            {
                selektovanaPromena = value;
            }
        }
        public ObservableCollection<PromenaViewModel> PromeneProp
        {
            get { return promeneProp; }
            set { promeneProp = value; }
        }

        private void btnNazad_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void InitializeDropDownMenuItems()
        {
            var VrsteNalogaCrud = new VrstaNalogaCRUD(new DataBaseContext());
            var SveVrsteNaloga = VrsteNalogaCrud.GetAllVrstas();
            
            VrsteNaloga.ItemsSource = SveVrsteNaloga.ToDictionary(vn => vn.ID, vn => vn.Naziv);
        }

        private void btnUnos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var DBContext = new DataBaseContext();

                var promene = new List<Promena>();
                
                var dugujeUk = 0.0m;

                var potrazujeUk = 0.0m;

                var glavnaKnjigaCRUD = new GlavnaKnjigaCRUD(DBContext);

                var glavnaKnjiga = glavnaKnjigaCRUD.GlavnaKnjigaRead(_pred.ID);

                var analitickiKontoCRUD = new AnalitickiKontoCRUD(DBContext);
                
                var preduzeceCRUD = new PreduzeceCRUD(DBContext);

                foreach(var promenaView in promeneProp)
                {
                    var konto = analitickiKontoCRUD.AnalitickiKontoRead(promenaView.AnalitickiKonto);

                    if(konto == null)
                    {
                        konto = new AnalitickiKonto()
                        {
                            GlavnaKnjiga_ID = glavnaKnjiga.ID,
                            Sifra = promenaView.AnalitickiKonto
                        };
                        konto.ID = analitickiKontoCRUD.AnalitickiKontoCreate(konto);
                    }
                                        
                    var promena = PromenaAdapter.PromenaViewToPromenaDb(promenaView);

                    promena.AnalitickiKonto_ID = konto.ID;
                    
                    //proveriti vec dodatu promenu
                    promene.Add(promena);

                    if(promena.DugujePotrazuje == DugujePotrazuje.Duguje)
                    {
                        dugujeUk += promena.Iznos;
                    }
                    else
                    {
                        potrazujeUk += promena.Iznos;
                    }
                }

                if(dugujeUk != potrazujeUk)
                {
                    throw new Exception("Iznos ukupnog dugovanja i iznos ukupne potraznje nisu jednaki");
                }
                
                var godina = promene.First().DatumValute.Year;

                var poslovnaGodinaCRUD = new PoslovnaGodinaCRUD(DBContext);
                var poslovnaGodina = poslovnaGodinaCRUD.PoslovnaGodinaRead(glavnaKnjiga.ID, godina);

                var nalogZaKnjizenjeCRUD = new NalogCRUD(DBContext);

                var vrstaNalogaCRUD = new VrstaNalogaCRUD(DBContext);
                var nazivVrste = VrsteNaloga.SelectionBoxItem.ToString();
                var vrstaNaloga = vrstaNalogaCRUD.VrstaRead(nazivVrste);
                
                var brojNaloga = Convert.ToInt32(txtBrNaloga.Text);
               
                //pickerDatumKnjizenja.SelectedDate = DateTime.Today;
                //pickerDatumKnjizenja.DisplayDate
                //datumKnjizenja ce se koristiti kao datum dokumenta a datumNaloga ostaje u istoj upotrebi tj. datumValute u promeni

                var nalogZaKnjizenje = new NalogZaKnjizenje
                {
                    Promene = promene,
                    VrstaNaloga_ID = vrstaNaloga.ID,
                    PoslovnaGodina_ID = poslovnaGodina.ID,
                    DatumNaloga = Convert.ToDateTime(promene.FirstOrDefault().DatumValute),
                    DatumKnjizenja = Convert.ToDateTime(pickerDatumKnjizenja.SelectedDate),
                    DugujeUk = dugujeUk,
                    PotrazujeUk = potrazujeUk,
                    BrojNaloga = brojNaloga
                };

                nalogZaKnjizenjeCRUD.NalogCreate(nalogZaKnjizenje);
                MessageBox.Show("Uspesno ste uneli podatke!");

                _pred = preduzeceCRUD.PreduzeceRead(_pred.ID);

                this.Close();
                UnosNalogaView unv = new UnosNalogaView(_state, _pred);
                unv.Show();

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

         //PrintDialog pd = new PrintDialog()PreviewStylusUpEvent(NalogZaKnjizenje);


        private void DatePicker_Loaded(object sender, RoutedEventArgs e)
        {
            DatePicker dp = sender as DatePicker;
            if (dp != null)
            {
                dp.ClearValue(DatePicker.DisplayDateProperty);
            }
        }


        private ObservableCollection<PromenaViewModel> CreateNalog()
        {
            return promeneProp;
        }

        //pregled proknjizenog naloga
        private void btnPregledNaloga_Click(object sender, RoutedEventArgs e)
        {
            ////this.Close();
            PregledNalogaView pnv = new PregledNalogaView(_pred);
            pnv.Show();
        }


        //pregled naloga pre knjizenja
        private void btnPregled_Click(object sender, RoutedEventArgs e)
        {
            //Window pu = new ProveraNalogaView();
            //pu.Show();
        }       

        private  GlavnaKnjiga   glavnaKnjiga;
        private  PoslovnaGodina poslovnaGodina;
        public void txtBrNaloga_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtBrNaloga.Text)) 
            {
                MessageBox.Show("Unesite novi broj naloga");
                return;
            }
            else
            {
                 var unetiBrojNaloga = Convert.ToInt32(txtBrNaloga.Text);
                 var pred = _pred;
                 GlavnaKnjigaCRUD gk = new GlavnaKnjigaCRUD(dbc);
                 glavnaKnjiga = gk.GlavnaKnjigaRead(pred.ID);
                 PoslovnaGodinaCRUD pg = new PoslovnaGodinaCRUD(dbc);
                 poslovnaGodina = pg.PoslovnaGodinaRead(glavnaKnjiga.ID, DateTime.Now.Year);



                 if (poslovnaGodina.NalogZaKnjizenje.Any(p => p.BrojNaloga == unetiBrojNaloga))
                 {
                     MessageBox.Show("Broj naloga " + unetiBrojNaloga + " postoji.");
                 }
                 else
                 {
                     return;
                 } 
            }
        }

        private void btnPrintPreview_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {   
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPrintPreview_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            PromeneProp.Remove((PromenaViewModel)this.dgPromene.SelectedItem);
            //PromeneProp.Remove(SelektovanaPromena);
            //if (e.Key == Key.Delete)
            //{
            //    this.dgPromene.Items.Remove(this.dgPromene.SelectedItem);
            //}

            //var promene = new List<Promena>();

            //foreach (var promenaView in promeneProp)
            //{
            //    var promena = PromenaAdapter.PromenaViewToPromenaDb(promenaView);
            //}
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
