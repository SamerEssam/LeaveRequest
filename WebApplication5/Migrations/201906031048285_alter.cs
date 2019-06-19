namespace WebApplication5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Requests", "RequesterID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Requests", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Requests", new[] { "RequesterID" });
            DropIndex("dbo.Requests", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Requests", "RequesterID");
            RenameColumn(table: "dbo.Requests", name: "ApplicationUser_Id", newName: "RequesterID");
            AlterColumn("dbo.Requests", "RequesterID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Requests", "RequesterID", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Requests", "RequesterID");
            AddForeignKey("dbo.Requests", "RequesterID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requests", "RequesterID", "dbo.AspNetUsers");
            DropIndex("dbo.Requests", new[] { "RequesterID" });
            AlterColumn("dbo.Requests", "RequesterID", c => c.String(maxLength: 128));
            AlterColumn("dbo.Requests", "RequesterID", c => c.String(maxLength: 128));
            RenameColumn(table: "dbo.Requests", name: "RequesterID", newName: "ApplicationUser_Id");
            AddColumn("dbo.Requests", "RequesterID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Requests", "ApplicationUser_Id");
            CreateIndex("dbo.Requests", "RequesterID");
            AddForeignKey("dbo.Requests", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Requests", "RequesterID", "dbo.AspNetUsers", "Id");
        }
    }
}
