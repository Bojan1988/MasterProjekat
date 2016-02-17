using MasterProjekat1.DBContext;
using MasterProjekat1.DBTablesCRUD;
using MasterProjekat1.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MasterProjekat1.Model.ViewModels
{
    public class PromenaViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly DataBaseContext context;
        private readonly GlavnaKnjiga glavnaKnjiga;

        private DateTime datumValute;
        private string konto;
        private Preduzece pred;
        private decimal potrazuje;
        private decimal duguje;

        public PromenaViewModel(Preduzece pred)
        {
            context = new DataBaseContext();
            GlavnaKnjigaCRUD gk = new GlavnaKnjigaCRUD(context);
            glavnaKnjiga = gk.GlavnaKnjigaRead(pred.ID);
            this.pred = pred;

            datumValute = DateTime.Now;
        }

        [Key]
        public int RedniBroj { get; set; }

        public string AnalitickiKonto
        {
            get
            {
                return konto;
            }

            set
            {
                if (!glavnaKnjiga.AnalitickiKonto.Exists(x => x.Sifra == value))
                {
                    MessageBox.Show("Naziv konta nije upisan");
                    if (value.StartsWith("43") || value.StartsWith("20")) 
                    {
                        var sifraPartnera = value.Substring(3);
                        if (!glavnaKnjiga.AnalitickiKonto.Exists(x => x.Sifra == value))
                            /*sifra partnera ne postoji u bazi za datu firmu sa kojom se radi*/
                        {
                            UnosPoslovnogPartnera upp = new UnosPoslovnogPartnera(pred);
                            upp.Show();
                        }
                    }
                    if (value.StartsWith("52"))
                    {
                        var sifraRadnika = value.Substring(3);
                        if(!glavnaKnjiga.AnalitickiKonto.Exists(x => x.Sifra == value))
                        {
                            UnosRadnikaView urv = new UnosRadnikaView(pred);
                            urv.Show();
                        }
                    }

                }
                konto = value;
            }
        }

        public string Opis { get; set; }
                
        public DateTime DatumValute
        {
            get { return datumValute; }

            set { this.datumValute = value; }
        }

        //private void NotifyPropertyChanged(string info)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(info));
        //    }
        //}

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }

        public decimal Duguje
        {
            get
            {
                return duguje;
            }

            set
            {
                duguje = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Duguje"));
            }
        }

        public decimal Potrazuje
        {
            get
            {
                return potrazuje;
            }

            set
            {
                potrazuje = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Potrazuje"));
            }
        }
    }
}
