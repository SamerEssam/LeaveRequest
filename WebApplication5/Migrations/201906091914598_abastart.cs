namespace WebApplication5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abastart : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VacationTypes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.Requests", "VacType_id", c => c.Int());
            CreateIndex("dbo.Requests", "VacType_id");
            AddForeignKey("dbo.Requests", "VacType_id", "dbo.VacationTypes", "id");
            DropColumn("dbo.Requests", "VacType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Requests", "VacType", c => c.Int(nullable: false));
            DropForeignKey("dbo.Requests", "VacType_id", "dbo.VacationTypes");
            DropIndex("dbo.Requests", new[] { "VacType_id" });
            DropColumn("dbo.Requests", "VacType_id");
            DropTable("dbo.VacationTypes");
        }
    }
}
