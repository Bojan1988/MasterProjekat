using MasterProjekat1.DBContext;
using MasterProjekat1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterProjekat1.DBTablesCRUD
{
    class PreduzeceCRUD
    {
        private DataBaseContext _dbContext;
        private RacunCRUD _racunCRUD;
        private TelefonCRUD _telefonCRUD;
        private EmailCRUD _emailCRUD;
        private GlavnaKnjigaCRUD _glavnaKnjigaCRUD;

        public PreduzeceCRUD(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
            _racunCRUD = new RacunCRUD(_dbContext);
            _telefonCRUD = new TelefonCRUD(_dbContext);
            _emailCRUD = new EmailCRUD(_dbContext);
            _glavnaKnjigaCRUD = new GlavnaKnjigaCRUD(_dbContext);
        }

        public void PreduzeceCreate(Preduzece pred)
        {
            var glavnaKnjiga = new GlavnaKnjiga
            {
                AnalitickiKonto = new List<AnalitickiKonto>(),
                PoslovnaGodina = new List<PoslovnaGodina>(),
                Preduzece = pred
            };

            _dbContext.Preduzeca.Add(pred);
            _racunCRUD.RacunCreate(pred.Racuni.ElementAt(pred.Racuni.Count() - 1));
            _telefonCRUD.TelefonCreate(pred.Telefoni.ElementAt(pred.Telefoni.Count() - 1));
            _emailCRUD.EmailCreate(pred.Emailovi.ElementAt(pred.Emailovi.Count() - 1));
            _glavnaKnjigaCRUD.GlavnaKnjigaCreate(glavnaKnjiga);
            _dbContext.SaveChanges();
        }

        public Preduzece PreduzeceRead(Guid preduzeceId)
        {
            return _dbContext.Preduzeca.FirstOrDefault(p => p.ID == preduzeceId);
        }

        public void PreduzeceUpdate(Preduzece pred)
        {
            var oldPred = _dbContext.Preduzeca.Where(p => p.ID == pred.ID).FirstOrDefault();
            UpdatePreduzece(pred, oldPred);
            _dbContext.SaveChanges();
        }

        private static void UpdatePreduzece(Preduzece pred, Preduzece oldPred)
        {
            oldPred.Naziv = pred.Naziv;
            oldPred.Adresa = pred.Adresa;
            oldPred.SifraDelatnosti = pred.SifraDelatnosti;
            oldPred.ObveznikPDV = pred.ObveznikPDV;
            oldPred.BrojNaloga = pred.BrojNaloga;
        }
    }
}
