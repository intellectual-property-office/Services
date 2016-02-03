using System;

namespace PersistenceServices.Files.Domain.Exceptions
{
    public class LargeFileException : Exception
    {
        public LargeFileException(string message) 
            : base(message)
        {            
        }

        public LargeFileException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}