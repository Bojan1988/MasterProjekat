using MasterProjekat1.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterProjekat1.DBContext
{
    public class DataBaseContext : DbContext 
    {
        public DbSet<AnalitickiKonto> AnalitickaKonta { get; set; }
        public DbSet<GlavnaKnjiga> GlavneKnjige { get; set; }
        public DbSet<NalogZaKnjizenje> NaloziZaKnjizenje { get; set; }
        public DbSet<OrganizacionaJedinica> OrganizacioneJedinice { get; set; }
        public DbSet<PoslovnaGodina> PoslovneGodine { get; set; }
        public DbSet<PoslovniPartner> PoslovniPartneri { get; set; }
        public DbSet<Preduzece> Preduzeca { get; set; }
        public DbSet<Radnik> Radnici { get; set; }
        public DbSet<Valuta> Valute { get; set; }
        public DbSet<VrstaNaloga> VrsteNaloga { get; set; }
        public DbSet<Email> Emailovi { get; set; }
        public DbSet<Telefon> Telefoni { get; set; }
        public DbSet<Promena> Promene { get; set; }
        public DbSet<Racun> Racuni { get; set; }
     }
}
