namespace WebApplication5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init3 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.AspNetUsers", name: "DManager_Id", newName: "MngrID");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_DManager_Id", newName: "IX_MngrID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_MngrID", newName: "IX_DManager_Id");
            RenameColumn(table: "dbo.AspNetUsers", name: "MngrID", newName: "DManager_Id");
        }
    }
}
