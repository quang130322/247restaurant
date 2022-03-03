namespace Res247.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCancelReasonToOrderModel1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("common.Orders", "CancelReason", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("common.Orders", "CancelReason", c => c.String(nullable: false));
        }
    }
}
