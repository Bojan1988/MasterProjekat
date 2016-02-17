using MasterProjekat1.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasterProjekat1.Model;

namespace MasterProjekat1.DBTablesCRUD
{
     class PromenaCRUD
    {
         private DataBaseContext _dbContext;
         private AnalitickiKontoCRUD _analitickiKontoCRUD;

         public PromenaCRUD(DataBaseContext dbContext)
         {
             // TODO: Complete member initialization
             _dbContext = dbContext;
             _analitickiKontoCRUD = new AnalitickiKontoCRUD(_dbContext);
         }

         public void PromenasCreate(List<Promena> promene)
         {
             foreach(var promena in promene)
             { 
                _dbContext.Promene.Add(promena);
             }
             _dbContext.SaveChanges();
         }
         
         public List<Promena> PromeneRead(Guid kontoId)
         {
             var promene = _dbContext.Promene.Where(p => p.AnalitickiKonto.ID== kontoId).ToList();
             return promene;
         }

         public List<Promena> PromeneReadNalogID(Guid nalogID)
         {
             var promene = _dbContext.Promene.Where(p => p.NalogZaKnjizenje_ID == nalogID).ToList();
             return promene;
         }
    }
}
