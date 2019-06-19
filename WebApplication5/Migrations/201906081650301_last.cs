namespace WebApplication5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class last : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "SickBalance", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "AnnualBalance", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "SuddenBalance", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "SuddenBalance", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "AnnualBalance", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "SickBalance", c => c.Int(nullable: false));
        }
    }
}
