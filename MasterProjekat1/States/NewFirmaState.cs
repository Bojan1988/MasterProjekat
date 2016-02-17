using MasterProjekat1.DBContext;
using MasterProjekat1.DBTablesCRUD;
using MasterProjekat1.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MasterProjekat1.States
{
    public class NewFirmaState : IState
    {
        public string WindowTitle
        {
            get { return "Unos firme"; }
        }

        public void Submit(Preduzece pred)
        {
            //using (DataBaseContext dbc = new DataBaseContext())
            //{
            //    dbc.Preduzeca.Add(pred);
            //    dbc.Racuni.Add(pred.Racuni.ElementAt(pred.Racuni.Count() - 1));
            //    dbc.Emailovi.Add(pred.Emailovi.ElementAt(pred.Emailovi.Count() - 1));
            //    dbc.Telefoni.Add(pred.Telefoni.ElementAt(pred.Telefoni.Count() - 1));
            //    dbc.SaveChanges();  
            //}

            var preduzeceCRUD = new PreduzeceCRUD(new DataBaseContext());
            preduzeceCRUD.PreduzeceCreate(pred);
            MessageBox.Show("Uspesno ste uneli podatke!");
        }
    }
}
