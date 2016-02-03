using System.Data.Entity.ModelConfiguration;
using PersistenceServices.Files.Domain.Entities;

namespace PersistenceServices.Files.Domain.EntityFramework.Maps
{
    public class FileBlobMap : EntityTypeConfiguration<FileBlob>
    {
        public FileBlobMap()
        {
            ToTable("FileData");
            HasKey(d => d.Id);
            Property(d => d.BlobGuid).IsRequired().HasColumnType("uniqueidentifier");
            Property(d => d.Bytes).IsRequired().HasColumnType("image");
            Property(d => d.ContentType).IsRequired().HasMaxLength(50).HasColumnType("nvarchar");
            Property(d => d.FileName).IsRequired().HasMaxLength(200).HasColumnType("nvarchar");
        }
    }
}