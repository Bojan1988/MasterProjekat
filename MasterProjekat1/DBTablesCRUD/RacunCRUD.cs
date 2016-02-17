using MasterProjekat1.DBContext;
using MasterProjekat1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterProjekat1.DBTablesCRUD
{
    class RacunCRUD
    {
        private DataBaseContext _dbContext;

        public RacunCRUD(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void RacunCreate(Racun racun)
        {
            _dbContext.Racuni.Add(racun);
            _dbContext.SaveChanges();
        }
    }
}
