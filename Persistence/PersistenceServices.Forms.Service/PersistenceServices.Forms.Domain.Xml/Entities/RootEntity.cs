using System.Collections.Generic;
using PersistenceServices.Forms.Domain.Entities;
using PersistenceServices.Forms.Domain.Xml.Interfaces;

namespace PersistenceServices.Forms.Domain.Xml.Entities
{
    public class RootEntity : IRootEntity
    {
        public List<FormDataEntity> FormDataEntities { get; set; }
    }
}