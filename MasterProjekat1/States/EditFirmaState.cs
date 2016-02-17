using MasterProjekat1.DBContext;
using MasterProjekat1.DBTablesCRUD;
using MasterProjekat1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MasterProjekat1.States
{
    class EditFirmaState : IState
    {
        public string WindowTitle
        {
            get { return "Izmena podataka firme"; }
        }

        public void Submit(Preduzece pred)
        {
            var preduzeceCRUD = new PreduzeceCRUD(new DataBaseContext());
            preduzeceCRUD.PreduzeceUpdate(pred);
            MessageBox.Show("Uspesno ste izmenili podatke!");
        }

        
    }
}
