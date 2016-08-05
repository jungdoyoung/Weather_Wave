namespace Weather.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ErrorLogs",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        FILE_NAME = c.String(),
                        ERROR_LOG = c.String(),
                        GET_DATE = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OceanFileChecks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FILE_NAME = c.String(),
                        FILE_SIZE = c.Long(nullable: false),
                        TODAY = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Oceans",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        UTC = c.DateTime(nullable: false),
                        i = c.Single(nullable: false),
                        j = c.Single(nullable: false),
                        DENSITY = c.Single(nullable: false),
                        SSS = c.Single(nullable: false),
                        SST = c.Single(nullable: false),
                        Current_UV = c.Single(nullable: false),
                        Current_VV = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.WaveFileChecks",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        FILE_NAME = c.String(),
                        GET_DATE = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Waves",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        UTC = c.DateTime(nullable: false),
                        lat = c.Double(nullable: false),
                        lon = c.Double(nullable: false),
                        ICEC = c.Double(nullable: false),
                        SWDIR_Seq1 = c.Double(nullable: false),
                        SWDIR_Seq2 = c.Double(nullable: false),
                        WVDIR = c.Double(nullable: false),
                        MWSPER = c.Double(nullable: false),
                        SWPER_Seq1 = c.Double(nullable: false),
                        SWPER_Seq2 = c.Double(nullable: false),
                        WVPER = c.Double(nullable: false),
                        DIRPW = c.Double(nullable: false),
                        PERPW = c.Double(nullable: false),
                        HTSGW = c.Double(nullable: false),
                        SWELL_Seq1 = c.Double(nullable: false),
                        SWELL_Seq2 = c.Double(nullable: false),
                        WVHGT = c.Double(nullable: false),
                        UGRD = c.Double(nullable: false),
                        VGRD = c.Double(nullable: false),
                        WDIR = c.Double(nullable: false),
                        WIND = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.UTC, clustered: true, name: "myIndex")
                .Index(t => t.lat, name: "myNonIndex");
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Waves", "myNonIndex");
            DropIndex("dbo.Waves", "myIndex");
            DropTable("dbo.Waves");
            DropTable("dbo.WaveFileChecks");
            DropTable("dbo.Oceans");
            DropTable("dbo.OceanFileChecks");
            DropTable("dbo.ErrorLogs");
        }
    }
}
