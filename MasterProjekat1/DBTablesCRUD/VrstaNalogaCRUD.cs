using MasterProjekat1.DBContext;
using MasterProjekat1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterProjekat1.DBTablesCRUD
{
    class VrstaNalogaCRUD
    {

        private DataBaseContext _dbContext;

        public VrstaNalogaCRUD(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<VrstaNaloga> GetAllVrstas()
        {
            return _dbContext.VrsteNaloga.ToList();
        }


        public VrstaNaloga VrstaRead(string nazivVrste)
        {
            return _dbContext.VrsteNaloga.FirstOrDefault(vn => vn.Naziv.Contains(nazivVrste)); 
        }
    }
}
