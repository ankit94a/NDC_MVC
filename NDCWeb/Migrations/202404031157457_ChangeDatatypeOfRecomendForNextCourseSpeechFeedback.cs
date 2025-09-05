namespace NDCWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDatatypeOfRecomendForNextCourseSpeechFeedback : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FeedbackSpeaker", "RecomendForNextCourse", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FeedbackSpeaker", "RecomendForNextCourse", c => c.Boolean(nullable: false));
        }
    }
}
