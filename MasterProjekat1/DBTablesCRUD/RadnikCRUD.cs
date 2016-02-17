using MasterProjekat1.DBContext;
using MasterProjekat1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterProjekat1.DBTablesCRUD
{
    class RadnikCRUD
    {
        private DataBaseContext _dbContext;
        private Preduzece pred; 

        public RadnikCRUD (DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Guid RadnikCreate(Radnik radnik)
        {
            _dbContext.Radnici.Add(radnik);
            _dbContext.SaveChanges();
            return radnik.ID;
        }

        public Radnik RadnikRead(int jmbg)
        {
            return _dbContext.Radnici.FirstOrDefault(a => a.JMBG == jmbg);
        }

    }
}
