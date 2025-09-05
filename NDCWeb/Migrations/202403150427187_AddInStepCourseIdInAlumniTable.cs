namespace NDCWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInStepCourseIdInAlumniTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AlumniMaster", "InStepCourseId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AlumniMaster", "InStepCourseId");
        }
    }
}
