using System.Collections.Generic;
using PersistenceServices.Forms.Domain.Entities;

namespace PersistenceServices.Forms.Domain.Xml.Interfaces
{
    public interface IRootEntity
    {
        List<FormDataEntity> FormDataEntities { get; set; }
    }
}