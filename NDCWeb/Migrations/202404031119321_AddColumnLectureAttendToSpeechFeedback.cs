namespace NDCWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnLectureAttendToSpeechFeedback : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FeedbackSpeaker", "LectureAttend", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FeedbackSpeaker", "LectureAttend");
        }
    }
}
