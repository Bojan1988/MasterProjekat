using MasterProjekat1.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterProjekat1.Model
{
   public class Promena
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Guid ID
        {
            get;
            set;
        }

       
        public virtual int RedniBroj
        {
            get;
            set;
        }
       

        public virtual DateTime DatumValute 
        {
            get;
            set;
        }

        public virtual DugujePotrazuje DugujePotrazuje
        {
            get;
            set;
        }

        public virtual string Opis
        {
            get;
            set;
        }

        public virtual decimal Iznos
        {
            get;
            set;
        }


        public virtual Radnik Radnik
        {
            get;
            set;
        }

        public virtual PoslovniPartner PoslovniPartner
        {
            get;
            set;
        }

        public virtual OrganizacionaJedinica OrganizacionaJedinica
        {
            get;
            set;
        }


        public Guid AnalitickiKonto_ID { get; set; }
        [ForeignKey("AnalitickiKonto_ID")]
        public virtual AnalitickiKonto AnalitickiKonto
        {
            get;
            set;
        }

        public Guid NalogZaKnjizenje_ID { get; set; }
        [ForeignKey("NalogZaKnjizenje_ID")]
        public virtual NalogZaKnjizenje NalogZaKnjizenje
        {
            get;
            set;
        }

    }
}
