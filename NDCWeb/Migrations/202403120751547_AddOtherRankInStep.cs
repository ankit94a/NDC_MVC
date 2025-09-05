namespace NDCWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOtherRankInStep : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InStepRegistration", "OtherRank", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.InStepRegistration", "OtherRank");
        }
    }
}
