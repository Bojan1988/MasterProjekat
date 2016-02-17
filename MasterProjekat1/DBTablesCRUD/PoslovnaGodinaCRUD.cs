using MasterProjekat1.DBContext;
using MasterProjekat1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterProjekat1.DBTablesCRUD
{
    public class PoslovnaGodinaCRUD
    {
        private DataBaseContext _dbContext;
        

        public PoslovnaGodinaCRUD(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void PoslovnaGodinaCreate(PoslovnaGodina pGodina)
        {
            _dbContext.PoslovneGodine.Add(pGodina);
            _dbContext.SaveChanges();
        }

        public PoslovnaGodina PoslovnaGodinaRead(Guid GlavnaKnjiga_ID, int Godina)
        {
           return _dbContext.PoslovneGodine.FirstOrDefault(pg => pg.GlavnaKnjiga_ID == GlavnaKnjiga_ID
               && pg.Godina == Godina);
        }

    }
}
