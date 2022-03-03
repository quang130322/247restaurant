namespace Res247.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCovidInfoModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("common.CovidInfo", "TravelToOtherPlace", c => c.Boolean(nullable: false));
            AddColumn("common.CovidInfo", "HaveSymptoms", c => c.Boolean(nullable: false));
            AddColumn("common.CovidInfo", "MeetCovidPatients", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("common.CovidInfo", "MeetCovidPatients");
            DropColumn("common.CovidInfo", "HaveSymptoms");
            DropColumn("common.CovidInfo", "TravelToOtherPlace");
        }
    }
}
