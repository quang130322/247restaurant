namespace Res247.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderAddressToOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("common.Orders", "OrderAddress", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("common.Orders", "OrderAddress");
        }
    }
}
