namespace MasterProjekat1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migr13 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnalitickiKontoes",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        Sifra = c.String(),
                        Naziv = c.String(),
                        GlavnaKnjiga_ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.GlavnaKnjigas", t => t.GlavnaKnjiga_ID, cascadeDelete: true)
                .Index(t => t.GlavnaKnjiga_ID);
            
            CreateTable(
                "dbo.GlavnaKnjigas",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        Preduzece_ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Preduzeces", t => t.Preduzece_ID, cascadeDelete: true)
                .Index(t => t.Preduzece_ID);
            
            CreateTable(
                "dbo.PoslovnaGodinas",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        Godina = c.Int(nullable: false),
                        GlavnaKnjiga_ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.GlavnaKnjigas", t => t.GlavnaKnjiga_ID, cascadeDelete: true)
                .Index(t => t.GlavnaKnjiga_ID);
            
            CreateTable(
                "dbo.NalogZaKnjizenjes",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        BrojNaloga = c.Int(nullable: false),
                        DatumNaloga = c.DateTime(nullable: false),
                        DugujeUk = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RedniBroj = c.Int(nullable: false),
                        PotrazujeUk = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DatumKnjizenja = c.DateTime(nullable: false),
                        PoslovnaGodina_ID = c.Guid(nullable: false),
                        VrstaNaloga_ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PoslovnaGodinas", t => t.PoslovnaGodina_ID, cascadeDelete: true)
                .ForeignKey("dbo.VrstaNalogas", t => t.VrstaNaloga_ID, cascadeDelete: true)
                .Index(t => t.PoslovnaGodina_ID)
                .Index(t => t.VrstaNaloga_ID);
            
            CreateTable(
                "dbo.Promenas",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        RedniBroj = c.Int(nullable: false),
                        DatumValute = c.DateTime(nullable: false),
                        DugujePotrazuje = c.Int(nullable: false),
                        Opis = c.String(),
                        Iznos = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AnalitickiKonto_ID = c.Guid(nullable: false),
                        NalogZaKnjizenje_ID = c.Guid(nullable: false),
                        OrganizacionaJedinica_ID = c.Guid(),
                        PoslovniPartner_ID = c.Guid(),
                        Radnik_ID = c.Guid(),
                        Valuta_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AnalitickiKontoes", t => t.AnalitickiKonto_ID, cascadeDelete: true)
                .ForeignKey("dbo.NalogZaKnjizenjes", t => t.NalogZaKnjizenje_ID, cascadeDelete: false)
                .ForeignKey("dbo.OrganizacionaJedinicas", t => t.OrganizacionaJedinica_ID)
                .ForeignKey("dbo.PoslovniPartners", t => t.PoslovniPartner_ID)
                .ForeignKey("dbo.Radniks", t => t.Radnik_ID)
                .ForeignKey("dbo.Valutas", t => t.Valuta_ID)
                .Index(t => t.AnalitickiKonto_ID)
                .Index(t => t.NalogZaKnjizenje_ID)
                .Index(t => t.OrganizacionaJedinica_ID)
                .Index(t => t.PoslovniPartner_ID)
                .Index(t => t.Radnik_ID)
                .Index(t => t.Valuta_ID);
            
            CreateTable(
                "dbo.OrganizacionaJedinicas",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        Adresa = c.String(),
                        Naziv = c.String(),
                        ObveznikPDV = c.Boolean(nullable: false),
                        PIB = c.Int(nullable: false),
                        SifraDelatnosti = c.String(),
                        Preduzece_ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Preduzeces", t => t.Preduzece_ID, cascadeDelete: true)
                .Index(t => t.Preduzece_ID);
            
            CreateTable(
                "dbo.Preduzeces",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        Naziv = c.String(),
                        Adresa = c.String(),
                        BrojNaloga = c.Int(nullable: false),
                        PIB = c.Int(nullable: false),
                        MaticniBroj = c.Int(nullable: false),
                        ObveznikPDV = c.Boolean(nullable: false),
                        SifraDelatnosti = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PoslovniPartners",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        Adresa = c.String(),
                        Naziv = c.String(),
                        ObveznikPDV = c.Boolean(nullable: false),
                        PIB = c.Int(nullable: false),
                        SifraDelatnosti = c.String(),
                        MaticniBroj = c.Int(nullable: false),
                        Preduzece_ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Preduzeces", t => t.Preduzece_ID, cascadeDelete: true)
                .Index(t => t.Preduzece_ID);
            
            CreateTable(
                "dbo.Radniks",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        Ime = c.String(),
                        Prezime = c.String(),
                        JMBG = c.Int(nullable: false),
                        StrucnaSprema = c.String(),
                        SifraRadnika = c.String(),
                        DatumZaposlenja = c.DateTime(nullable: false),
                        BrojRadnihSati = c.Int(nullable: false),
                        BrojBodova = c.Int(nullable: false),
                        Preduzece_ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Preduzeces", t => t.Preduzece_ID, cascadeDelete: true)
                .Index(t => t.Preduzece_ID);
            
            CreateTable(
                "dbo.VrstaNalogas",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Emails",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        EmailAdresa = c.String(),
                        Preduzece_ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Preduzeces", t => t.Preduzece_ID, cascadeDelete: true)
                .Index(t => t.Preduzece_ID);
            
            CreateTable(
                "dbo.Racuns",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        BrojRacuna = c.Int(nullable: false),
                        Preduzece_ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Preduzeces", t => t.Preduzece_ID, cascadeDelete: true)
                .Index(t => t.Preduzece_ID);
            
            CreateTable(
                "dbo.Telefons",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        Broj = c.String(),
                        Preduzece_ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Preduzeces", t => t.Preduzece_ID, cascadeDelete: true)
                .Index(t => t.Preduzece_ID);
            
            CreateTable(
                "dbo.Valutas",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        DatumValute = c.DateTime(nullable: false),
                        Oznaka = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Promenas", "Valuta_ID", "dbo.Valutas");
            DropForeignKey("dbo.Telefons", "Preduzece_ID", "dbo.Preduzeces");
            DropForeignKey("dbo.Racuns", "Preduzece_ID", "dbo.Preduzeces");
            DropForeignKey("dbo.Emails", "Preduzece_ID", "dbo.Preduzeces");
            DropForeignKey("dbo.GlavnaKnjigas", "Preduzece_ID", "dbo.Preduzeces");
            DropForeignKey("dbo.NalogZaKnjizenjes", "VrstaNaloga_ID", "dbo.VrstaNalogas");
            DropForeignKey("dbo.Promenas", "Radnik_ID", "dbo.Radniks");
            DropForeignKey("dbo.Radniks", "Preduzece_ID", "dbo.Preduzeces");
            DropForeignKey("dbo.Promenas", "PoslovniPartner_ID", "dbo.PoslovniPartners");
            DropForeignKey("dbo.PoslovniPartners", "Preduzece_ID", "dbo.Preduzeces");
            DropForeignKey("dbo.Promenas", "OrganizacionaJedinica_ID", "dbo.OrganizacionaJedinicas");
            DropForeignKey("dbo.OrganizacionaJedinicas", "Preduzece_ID", "dbo.Preduzeces");
            DropForeignKey("dbo.Promenas", "NalogZaKnjizenje_ID", "dbo.NalogZaKnjizenjes");
            DropForeignKey("dbo.Promenas", "AnalitickiKonto_ID", "dbo.AnalitickiKontoes");
            DropForeignKey("dbo.NalogZaKnjizenjes", "PoslovnaGodina_ID", "dbo.PoslovnaGodinas");
            DropForeignKey("dbo.PoslovnaGodinas", "GlavnaKnjiga_ID", "dbo.GlavnaKnjigas");
            DropForeignKey("dbo.AnalitickiKontoes", "GlavnaKnjiga_ID", "dbo.GlavnaKnjigas");
            DropIndex("dbo.Telefons", new[] { "Preduzece_ID" });
            DropIndex("dbo.Racuns", new[] { "Preduzece_ID" });
            DropIndex("dbo.Emails", new[] { "Preduzece_ID" });
            DropIndex("dbo.Radniks", new[] { "Preduzece_ID" });
            DropIndex("dbo.PoslovniPartners", new[] { "Preduzece_ID" });
            DropIndex("dbo.OrganizacionaJedinicas", new[] { "Preduzece_ID" });
            DropIndex("dbo.Promenas", new[] { "Valuta_ID" });
            DropIndex("dbo.Promenas", new[] { "Radnik_ID" });
            DropIndex("dbo.Promenas", new[] { "PoslovniPartner_ID" });
            DropIndex("dbo.Promenas", new[] { "OrganizacionaJedinica_ID" });
            DropIndex("dbo.Promenas", new[] { "NalogZaKnjizenje_ID" });
            DropIndex("dbo.Promenas", new[] { "AnalitickiKonto_ID" });
            DropIndex("dbo.NalogZaKnjizenjes", new[] { "VrstaNaloga_ID" });
            DropIndex("dbo.NalogZaKnjizenjes", new[] { "PoslovnaGodina_ID" });
            DropIndex("dbo.PoslovnaGodinas", new[] { "GlavnaKnjiga_ID" });
            DropIndex("dbo.GlavnaKnjigas", new[] { "Preduzece_ID" });
            DropIndex("dbo.AnalitickiKontoes", new[] { "GlavnaKnjiga_ID" });
            DropTable("dbo.Valutas");
            DropTable("dbo.Telefons");
            DropTable("dbo.Racuns");
            DropTable("dbo.Emails");
            DropTable("dbo.VrstaNalogas");
            DropTable("dbo.Radniks");
            DropTable("dbo.PoslovniPartners");
            DropTable("dbo.Preduzeces");
            DropTable("dbo.OrganizacionaJedinicas");
            DropTable("dbo.Promenas");
            DropTable("dbo.NalogZaKnjizenjes");
            DropTable("dbo.PoslovnaGodinas");
            DropTable("dbo.GlavnaKnjigas");
            DropTable("dbo.AnalitickiKontoes");
        }
    }
}
