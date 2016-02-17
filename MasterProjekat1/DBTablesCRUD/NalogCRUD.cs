using MasterProjekat1.DBContext;
using MasterProjekat1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterProjekat1.DBTablesCRUD
{
    class NalogCRUD
    {

        private DataBaseContext _dbContext;
        private PromenaCRUD _promenaCRUD;
        private PreduzeceCRUD _preduzeceCRUD;

        public NalogCRUD(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
            _promenaCRUD = new PromenaCRUD(_dbContext);
            _preduzeceCRUD = new PreduzeceCRUD(_dbContext);
        }

        public void NalogCreate(NalogZaKnjizenje nalogZaKnjizenje)
        {
            _dbContext.NaloziZaKnjizenje.Add(nalogZaKnjizenje);

            nalogZaKnjizenje.Promene = nalogZaKnjizenje.Promene.Where(p => p.Iznos != 0).ToList();

            foreach(var promena in nalogZaKnjizenje.Promene)
            {
                promena.NalogZaKnjizenje_ID = nalogZaKnjizenje.ID;
            }

            _promenaCRUD.PromenasCreate(nalogZaKnjizenje.Promene);

            var pred = _preduzeceCRUD.PreduzeceRead(nalogZaKnjizenje.PoslovnaGodina.GlavnaKnjiga.Preduzece_ID);
            pred.BrojNaloga = pred.BrojNaloga + 1;
            _preduzeceCRUD.PreduzeceUpdate(pred);

            _dbContext.SaveChanges();
        }

        public NalogZaKnjizenje NalogRead(Guid nalogID)
        {
            return _dbContext.NaloziZaKnjizenje.FirstOrDefault(a => a.ID == nalogID);
        }

        public bool NalogPostoji(int brojNaloga)
        {
            bool containsItem = _dbContext.NaloziZaKnjizenje.Any(item => item.BrojNaloga == brojNaloga);
            return true;
        }
    }
}
