namespace NDCWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserPasswordHistory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserPwdManger",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        username = c.String(maxLength: 50),
                        password = c.String(maxLength: 200),
                        ModifyDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserPwdManger");
        }
    }
}
