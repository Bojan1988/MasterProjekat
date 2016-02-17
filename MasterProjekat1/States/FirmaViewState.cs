using MasterProjekat1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterProjekat1.States
{
    class FirmaViewState : IState
    {
        public string WindowTitle
        {
            get { return "Knjizenje firme"; }
        }

        //public void Submit(Preduzece pred)
        //{
        //    throw new NotImplementedException();
        //}


        public void Submit(Preduzece pred)
        {
            throw new NotImplementedException();
        }
    }
}
