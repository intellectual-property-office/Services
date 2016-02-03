using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using PersistenceServices.Forms.Domain.Entities;

namespace PersistenceServices.Forms.Domain.EntityFramework.Maps
{
    class PersistenceEntityMap : EntityTypeConfiguration<FormDataEntity>
    {
        public PersistenceEntityMap()
        {
            HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(m => m.FormDataId).IsRequired();
            Property(m => m.SerializedFormData).IsRequired();
            Property(m => m.CreatedDateTime).IsRequired();
            Property(m => m.ModifiedDateTime).IsRequired();
            ToTable("FormData");
        }
    }
}