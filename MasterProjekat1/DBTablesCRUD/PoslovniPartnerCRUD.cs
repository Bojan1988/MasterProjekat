using MasterProjekat1.DBContext;
using MasterProjekat1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterProjekat1.DBTablesCRUD
{
    class PoslovniPartnerCRUD
    {
        private DataBaseContext _dbContext;

        public PoslovniPartnerCRUD(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Guid PoslovniPartnerCreate(PoslovniPartner poslovniPartner)
        {
            _dbContext.PoslovniPartneri.Add(poslovniPartner);
            _dbContext.SaveChanges();
            return poslovniPartner.ID;
        }

        public PoslovniPartner PoslovnvniPartnerRead(int PIB)
        {
            return _dbContext.PoslovniPartneri.FirstOrDefault(a => a.PIB == PIB);
        }
    }
}
