using System.Data.Entity.Migrations;

namespace PersistenceServices.Forms.Domain.EntityFramework.Migrations
{
    public partial class Initial_create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FormData",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FormDataId = c.Guid(nullable: false),
                        SerializedFormData = c.String(nullable: false),
                        CreatedDateTime = c.DateTime(nullable: false),
                        ModifiedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FormData");
        }
    }
}
