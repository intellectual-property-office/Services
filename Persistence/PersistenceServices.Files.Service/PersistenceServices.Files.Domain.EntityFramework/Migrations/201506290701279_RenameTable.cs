namespace PersistenceServices.Files.Domain.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.FileBlobs", newName: "FileData");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.FileData", newName: "FileBlobs");
        }
    }
}
