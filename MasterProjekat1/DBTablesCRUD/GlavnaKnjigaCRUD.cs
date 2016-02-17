using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MasterProjekat1.DBTablesCRUD;
using MasterProjekat1.DBContext;
using MasterProjekat1.Model;

namespace MasterProjekat1.DBTablesCRUD
{
    class GlavnaKnjigaCRUD
    {
        private DataBaseContext _dbContext;
        private PoslovnaGodinaCRUD _poslovnaGodinaCRUD;

        public GlavnaKnjigaCRUD(DataBaseContext dbContext)
        {
            
            _dbContext = dbContext;
            _poslovnaGodinaCRUD = new PoslovnaGodinaCRUD(_dbContext);
        }

        public void GlavnaKnjigaCreate(GlavnaKnjiga glavnaKnjiga)
        {
            var poslovnaGodina = new PoslovnaGodina
            {
                GlavnaKnjiga = glavnaKnjiga,
                Godina = DateTime.Now.Year,
                NalogZaKnjizenje = new List<NalogZaKnjizenje>()
            };

            ((List<PoslovnaGodina>)glavnaKnjiga.PoslovnaGodina).Add(poslovnaGodina);

            _dbContext.GlavneKnjige.Add(glavnaKnjiga);
            _poslovnaGodinaCRUD.PoslovnaGodinaCreate(poslovnaGodina);
            _dbContext.SaveChanges();
        }

        public GlavnaKnjiga GlavnaKnjigaRead(Guid Preduzece_ID)
        {
               var GlavnaKnjiga = _dbContext.GlavneKnjige.FirstOrDefault(gk => gk.Preduzece_ID == Preduzece_ID);
           
            return GlavnaKnjiga;
        }
    }
}
