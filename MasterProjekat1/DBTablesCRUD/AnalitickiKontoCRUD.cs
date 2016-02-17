using MasterProjekat1.DBContext;
using MasterProjekat1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterProjekat1.DBTablesCRUD
{
    public class AnalitickiKontoCRUD
    {
        private DataBaseContext _dbContext;

        public AnalitickiKontoCRUD(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Guid AnalitickiKontoCreate(AnalitickiKonto analitickiKonto)
        {
            _dbContext.AnalitickaKonta.Add(analitickiKonto);
            _dbContext.SaveChanges();
            return analitickiKonto.ID;
        }

        public AnalitickiKonto AnalitickiKontoRead(string Sifra)
        {
            return _dbContext.AnalitickaKonta.FirstOrDefault(a => a.Sifra == Sifra);
        }
    }
}
