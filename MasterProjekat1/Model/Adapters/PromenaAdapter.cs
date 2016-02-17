using MasterProjekat1.Enums;
using MasterProjekat1.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterProjekat1.Model.Adapters
{
    public class PromenaAdapter
    {
        public static Promena PromenaViewToPromenaDb(PromenaViewModel promenaViewModel)
        {
            if (promenaViewModel.Duguje != 0 && promenaViewModel.Potrazuje != 0)
                throw new ArgumentException();

            var promena = new Promena
            {
                RedniBroj = promenaViewModel.RedniBroj,
                Opis = promenaViewModel.Opis,
                DatumValute = promenaViewModel.DatumValute,
                Iznos = promenaViewModel.Potrazuje == 0 ? promenaViewModel.Duguje : promenaViewModel.Potrazuje,
                DugujePotrazuje = promenaViewModel.Potrazuje == 0 ? DugujePotrazuje.Duguje : DugujePotrazuje.Potrazuje,
            };

            return promena;
        }
    }
}
