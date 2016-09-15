namespace BeastLords.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddChatInfoTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChatInfoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        signalRChatID = c.String(),
                        name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ChatInfoes");
        }
    }
}
