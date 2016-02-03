using System;

namespace PersistenceServices.Files.Domain.Interfaces
{
    public interface ILoggerService
    {
        void Debug(string message);
        void Error(Exception x);
        void Error(string message);
        void Error(string message, Exception x);
        void Fatal(Exception x);
        void Fatal(string message);
        void Info(string message);
        void Warn(string message);
    }
}