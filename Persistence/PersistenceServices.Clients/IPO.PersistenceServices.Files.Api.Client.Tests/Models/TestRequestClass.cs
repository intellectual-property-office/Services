using System;

namespace Persistence.Files.Client.Tests.Models
{
    public class TestRequestClass
    {
        public Byte[] Bytes { get; set; }
        public String ContentType { get; set; }
        public String FileName { get; set; }
    }
}