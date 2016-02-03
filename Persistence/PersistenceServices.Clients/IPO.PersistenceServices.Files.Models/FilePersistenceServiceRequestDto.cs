using System;

namespace IPO.PersistenceServices.Files.Models
{
    public class FilePersistenceServiceRequestDto
    {
        public Byte[] Bytes { get; set; }
        public String ContentType { get; set; }
        public String FileName { get; set; }
    }
}