using System.Collections.Generic;

namespace PersistenceServices.Forms.Domain.Tests.Models
{
    public class TestSerializableClass
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<TestSerializableOwnerClass> Owners { get; set; } 
    }
}