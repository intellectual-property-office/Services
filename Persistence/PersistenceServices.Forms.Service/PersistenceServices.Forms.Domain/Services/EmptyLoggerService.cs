using System;
using PersistenceServices.Forms.Domain.Interfaces;

namespace PersistenceServices.Forms.Domain.Services
{
    public class EmptyLoggerService : ILoggerService
    {
        public void Debug(string message)
        {
        }

        public void Error(Exception x)
        {
        }

        public void Error(string message)
        {
        }

        public void Error(string message, Exception x)
        {
        }

        public void Fatal(Exception x)
        {
        }

        public void Fatal(string message)
        {
        }

        public void Info(string message)
        {
        }

        public void Warn(string message)
        {
        }
    }
}