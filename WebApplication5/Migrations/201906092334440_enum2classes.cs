namespace WebApplication5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class enum2classes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Requests", "VacType_id", "dbo.VacationTypes");
            DropIndex("dbo.Requests", new[] { "VacType_id" });
            RenameColumn(table: "dbo.Requests", name: "VacType_id", newName: "VacTypeID");
            CreateTable(
                "dbo.RequestStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Requests", "ReqStatusID", c => c.Int(nullable: false));
            AddColumn("dbo.VacationTypes", "LeaveType", c => c.String());
            AlterColumn("dbo.Requests", "VacTypeID", c => c.Int(nullable: false));
            CreateIndex("dbo.Requests", "VacTypeID");
            CreateIndex("dbo.Requests", "ReqStatusID");
            AddForeignKey("dbo.Requests", "ReqStatusID", "dbo.RequestStatus", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Requests", "VacTypeID", "dbo.VacationTypes", "Id", cascadeDelete: true);
            DropColumn("dbo.Requests", "ReqStatus");
            DropColumn("dbo.VacationTypes", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.VacationTypes", "Type", c => c.String());
            AddColumn("dbo.Requests", "ReqStatus", c => c.Int(nullable: false));
            DropForeignKey("dbo.Requests", "VacTypeID", "dbo.VacationTypes");
            DropForeignKey("dbo.Requests", "ReqStatusID", "dbo.RequestStatus");
            DropIndex("dbo.Requests", new[] { "ReqStatusID" });
            DropIndex("dbo.Requests", new[] { "VacTypeID" });
            AlterColumn("dbo.Requests", "VacTypeID", c => c.Int());
            DropColumn("dbo.VacationTypes", "LeaveType");
            DropColumn("dbo.Requests", "ReqStatusID");
            DropTable("dbo.RequestStatus");
            RenameColumn(table: "dbo.Requests", name: "VacTypeID", newName: "VacType_id");
            CreateIndex("dbo.Requests", "VacType_id");
            AddForeignKey("dbo.Requests", "VacType_id", "dbo.VacationTypes", "id");
        }
    }
}
