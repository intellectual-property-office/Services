using System;

namespace IPO.PersistenceServices.SerializationService.Tests.Models
{
    public class SerializationTestClass
    {
        public SerializationTestClass(string name, DateTime date)
        {
            Name = name;
            Date = date;
        }

        string Name { get; set; }

        DateTime Date { get; set; }
    }
}