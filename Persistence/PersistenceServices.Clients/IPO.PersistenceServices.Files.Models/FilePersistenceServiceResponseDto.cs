using System;

namespace IPO.PersistenceServices.Files.Models
{
    public class FilePersistenceServiceResponseDto
    {
        public Boolean Success { get; set; }
        public Guid Guid { get; set; }
        public string Error { get; set; }
        public string ContentType { get; set; }
        public Byte[] Bytes { get; set; }
        public String FileName { get; set; }
    }
}