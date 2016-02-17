using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasterProjekat1.Model.ViewModels;

namespace MasterProjekat1.Model.Adapters
{
    public class AnalitikaAdapter
    {
        public static AnalitikaViewModel promenaDbToPromenaView(Promena promena)
        {
            var analitikaViewModel = new AnalitikaViewModel()
            {
                AnalitickiKonto = promena.AnalitickiKonto.Sifra,
                BrojNaloga = promena.NalogZaKnjizenje.BrojNaloga,
                Opis = promena.Opis,
                DatumNaloga = promena.NalogZaKnjizenje.DatumNaloga.ToShortDateString(),
                Duguje = promena.DugujePotrazuje == Enums.DugujePotrazuje.Duguje ? promena.Iznos : 0,
                Potrazuje = promena.DugujePotrazuje == Enums.DugujePotrazuje.Potrazuje ? promena.Iznos : 0,
                TotalDuguje = 0,
                TotalPotrazuje = 0,
                SaldoDuguje = 0,
                SaldoPotrazuje = 0,

            };
            return analitikaViewModel;
        }
    }
}
