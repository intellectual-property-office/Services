using System;

namespace PersistenceServices.Forms.Domain.Interfaces
{
    public interface IEntity
    {
        int Id { get; set; }

        DateTime CreatedDateTime { get; set; }

        DateTime ModifiedDateTime { get; set; }
    }
}