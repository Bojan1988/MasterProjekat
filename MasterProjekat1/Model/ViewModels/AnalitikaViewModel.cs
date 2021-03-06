﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterProjekat1.Model.ViewModels
{
    public class AnalitikaViewModel
    {
        public string AnalitickiKonto { get; set; }

        public int BrojNaloga { get; set; }

        public string Opis { get; set;}

        public string DatumNaloga { get; set; }

        public decimal Duguje { get; set; }

        public decimal Potrazuje { get; set; }

        public decimal TotalDuguje { get; set; }

        public decimal TotalPotrazuje { get; set; }

        public decimal SaldoDuguje { get; set; }

        public decimal SaldoPotrazuje { get; set; }
    }
}
