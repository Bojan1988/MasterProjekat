using MasterProjekat1.DBContext;
using MasterProjekat1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterProjekat1.DBTablesCRUD
{
    class TelefonCRUD
    {
        private DataBaseContext _dbContext;

        public TelefonCRUD(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void TelefonCreate(Telefon tel)
        {
            _dbContext.Telefoni.Add(tel);
            _dbContext.SaveChanges();
        }
    }
}
