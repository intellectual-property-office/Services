using System;

namespace PersistenceServices.Files.Domain.Exceptions
{
    public class UnsupportedOperationException : Exception
    {
        public UnsupportedOperationException(string message) 
            : base(message)
        {            
        }

        public UnsupportedOperationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}