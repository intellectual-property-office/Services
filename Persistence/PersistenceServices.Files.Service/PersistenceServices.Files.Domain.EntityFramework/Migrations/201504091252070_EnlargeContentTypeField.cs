namespace PersistenceServices.Files.Domain.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnlargeContentTypeField : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FileBlobs", "ContentType", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FileBlobs", "ContentType", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
