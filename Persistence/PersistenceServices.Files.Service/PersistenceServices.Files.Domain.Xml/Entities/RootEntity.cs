using System.Collections.Generic;
using PersistenceServices.Files.Domain.Entities;
using PersistenceServices.Files.Domain.Xml.Interfaces;

namespace PersistenceServices.Files.Domain.Xml.Entities
{
    public class RootEntity : IRootEntity
    {
        public List<FileBlob> FileBlobs { get; set; }
    }
}