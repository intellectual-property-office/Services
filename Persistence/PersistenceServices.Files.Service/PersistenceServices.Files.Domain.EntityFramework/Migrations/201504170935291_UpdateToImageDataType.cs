namespace PersistenceServices.Files.Domain.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateToImageDataType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FileBlobs", "Bytes", c => c.Binary(nullable: false, storeType: "image"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FileBlobs", "Bytes", c => c.Binary(nullable: false, maxLength: 8000));
        }
    }
}
