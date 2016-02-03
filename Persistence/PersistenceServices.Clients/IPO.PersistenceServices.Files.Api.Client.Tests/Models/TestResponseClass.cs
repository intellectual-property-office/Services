using System;

namespace Persistence.Files.Client.Tests.Models
{
    public class TestResponseClass
    {
        public Boolean Success { get; set; }
        public Guid Guid { get; set; }
        public string Error { get; set; }
        public string ContentType { get; set; }
        public Byte[] Bytes { get; set; }
    }
}