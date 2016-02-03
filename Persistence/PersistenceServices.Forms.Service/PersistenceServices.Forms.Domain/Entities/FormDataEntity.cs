using System;
using PersistenceServices.Forms.Domain.Interfaces;

namespace PersistenceServices.Forms.Domain.Entities
{
    public class FormDataEntity : IEntity
    {
        public FormDataEntity()
        {
            CreatedDateTime = DateTime.Now;
            ModifiedDateTime = DateTime.Now;
        }

        public FormDataEntity(Guid formDataId, string serializedFormData)
        {
            FormDataId = formDataId;
            SerializedFormData = serializedFormData;
            CreatedDateTime = DateTime.Now;
            ModifiedDateTime = DateTime.Now;
        }

        public void UpdateFormData(string serializedFormData)
        {
            SerializedFormData = serializedFormData;
            ModifiedDateTime = DateTime.Now;
        }

        public int Id { get; set; }

        public Guid FormDataId { get; set; }

        public string SerializedFormData { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime ModifiedDateTime { get; set; }
    }
}