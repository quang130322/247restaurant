namespace Res247.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCancelReasonToOrderModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("common.Orders", "CancelReason", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("common.Orders", "CancelReason");
        }
    }
}
