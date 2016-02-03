using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using PersistenceServices.Files.Domain.Entities;
using PersistenceServices.Files.Domain.Interfaces;
using PersistenceServices.Files.Domain.Xml.Entities;
using PersistenceServices.Files.Domain.Xml.Interfaces;

namespace PersistenceServices.Files.Domain.Xml
{
    public class XmlUnitOfWork : IUnitOfWork
    {
        private readonly string _filename;
        private readonly XmlSerializer _xmlSerializer;
        private IRootEntity _rootEntity;

        private IRepository<FileBlob> _fileDataRepository;

        public XmlUnitOfWork(string filename)
        {
            if (filename == null)
            {
                throw new ArgumentNullException("filename");
            }

            _filename = filename;
            _xmlSerializer = new XmlSerializer(typeof(RootEntity));

            if (!File.Exists(filename))
            {
                InitializeXmlFile();
            }

            using (var streamReader = new StreamReader(filename))
            {
                _rootEntity = (IRootEntity)_xmlSerializer.Deserialize(streamReader);
                streamReader.Close();
            }
        }

        public IRepository<T> Repository<T>() where T : IEntity
        {
            return (IRepository<T>)(_fileDataRepository ?? (_fileDataRepository = new XmlRepository<FileBlob>(_rootEntity.FileBlobs.ToList())));
        }

        public async Task<int> SaveChanges()
        {
            IEnumerable<FileBlob> entityList = new List<FileBlob>();
            await Task.Run(() => entityList = _fileDataRepository.GetAllAsync().Result.ToList());
            _rootEntity.FileBlobs = entityList.OrderBy(e => e.Id).ToList();
            WriteEntitiesToFile();

            return 1;
        }

        public void Dispose()
        {
            //_xmlSerializer = null;
            //_rootEntity = null;
        }

        private void InitializeXmlFile()
        {
            _rootEntity = new RootEntity { FileBlobs = new List<FileBlob>() };
            WriteEntitiesToFile();
        }

        private void WriteEntitiesToFile()
        {
            using (var streamWriter = new StreamWriter(_filename))
            {
                _xmlSerializer.Serialize(streamWriter, _rootEntity);
                streamWriter.Close();
            }
        }
    }
}