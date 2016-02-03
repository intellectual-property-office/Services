namespace PersistenceServices.Files.Domain.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreatedDateField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FileBlobs", "CreatedDateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FileBlobs", "CreatedDateTime");
        }
    }
}
