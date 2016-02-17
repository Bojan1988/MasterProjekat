using MasterProjekat1.DBContext;
using MasterProjekat1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterProjekat1.DBTablesCRUD
{
    class EmailCRUD
    {
        private DataBaseContext _dbContext;

        public EmailCRUD(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void EmailCreate(Email email)
        {
            _dbContext.Emailovi.Add(email);
            _dbContext.SaveChanges();
        }
    }
}
