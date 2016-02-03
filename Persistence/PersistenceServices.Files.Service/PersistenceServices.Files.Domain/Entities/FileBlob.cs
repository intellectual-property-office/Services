using System;
using PersistenceServices.Files.Domain.Interfaces;

namespace PersistenceServices.Files.Domain.Entities
{
    public class FileBlob : IEntity
    {
        public FileBlob()
        {
            CreatedDateTime = DateTime.Now;
            BlobGuid = Guid.NewGuid();
        }

        public FileBlob(byte[] bytes, string contentType, string fileName)
        {
            CreatedDateTime = DateTime.Now;
            BlobGuid = Guid.NewGuid();
            Bytes = bytes;
            ContentType = contentType;
            FileName = fileName;
        }
        
        public int Id { get; set; }
        public Guid BlobGuid { get; set; }
        public Byte[] Bytes { get; set; }
        public String ContentType { get; set; }
        public String FileName { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}