namespace FakeTwitter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TweetLengthTo120 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tweets", "Text", c => c.String(nullable: false, maxLength: 120));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tweets", "Text", c => c.String(nullable: false, maxLength: 280));
        }
    }
}
