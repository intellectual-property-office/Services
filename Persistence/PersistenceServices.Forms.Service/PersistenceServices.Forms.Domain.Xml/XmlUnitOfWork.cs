using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using PersistenceServices.Forms.Domain.Entities;
using PersistenceServices.Forms.Domain.Interfaces;
using PersistenceServices.Forms.Domain.Xml.Entities;
using PersistenceServices.Forms.Domain.Xml.Interfaces;

namespace PersistenceServices.Forms.Domain.Xml
{
    public class XmlUnitOfWork : IUnitOfWork
    {
        private readonly string _filename;
        private XmlSerializer _xmlSerializer;
        private IRootEntity _rootEntity;

        private IRepository<FormDataEntity> _formDataRepository;

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
            return (IRepository<T>) (_formDataRepository ?? (_formDataRepository = new XmlRepository<FormDataEntity>(_rootEntity.FormDataEntities.ToList())));
        }

        public async Task<int> SaveChanges()
        {
            IEnumerable<FormDataEntity> entityList = new List<FormDataEntity>();
            await Task.Run(() => entityList = _formDataRepository.GetAllAsync().Result.ToList());
            _rootEntity.FormDataEntities = entityList.OrderBy(e => e.Id).ToList();
            WriteEntitiesToFile();

            return 1;
        }

        public void Dispose()
        {
            _xmlSerializer = null;
            _rootEntity = null;
        }

        private void InitializeXmlFile()
        {
            _rootEntity = new RootEntity { FormDataEntities = new List<FormDataEntity>() };
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