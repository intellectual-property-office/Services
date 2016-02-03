namespace PersistenceServices.Files.Domain.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFileBlob : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FileBlobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BlobGuid = c.Guid(nullable: false),
                        Bytes = c.Binary(nullable: false, maxLength: 8000),
                        ContentType = c.String(nullable: false, maxLength: 20),
                        FileName = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FileBlobs");
        }
    }
}
