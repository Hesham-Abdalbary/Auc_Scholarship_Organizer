namespace Auc_Scholarship_Organizer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Auc : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentApplications",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        University = c.String(),
                        Major = c.String(),
                        Gpa = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Resume = c.String(),
                        StatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Status", t => t.StatusId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.StatusId);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ApplicationStatus = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        EmailAddress = c.String(),
                        Password = c.String(),
                        BirthDate = c.DateTime(),
                        NationalID = c.String(),
                        IsSystemAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentApplications", "Id", "dbo.Users");
            DropForeignKey("dbo.StudentApplications", "StatusId", "dbo.Status");
            DropIndex("dbo.StudentApplications", new[] { "StatusId" });
            DropIndex("dbo.StudentApplications", new[] { "Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Status");
            DropTable("dbo.StudentApplications");
        }
    }
}
