using MasterProjekat1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MasterProjekat1.States
{
    class UnosNalogaState : IState
    {
        public string WindowTitle
        {
            get { return "Unos naloga"; }

        }

        public void Submit(Preduzece pred)
        {

            MessageBox.Show("Unos naloga state " + pred.Naziv.ToString());
           
        }
    }
}
