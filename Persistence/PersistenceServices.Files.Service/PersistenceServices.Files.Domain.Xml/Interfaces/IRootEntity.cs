using System.Collections.Generic;
using PersistenceServices.Files.Domain.Entities;

namespace PersistenceServices.Files.Domain.Xml.Interfaces
{
    public interface IRootEntity
    {
        List<FileBlob> FileBlobs { get; set; }
    }
}