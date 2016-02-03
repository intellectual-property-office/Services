using System;
using System.Threading.Tasks;
using PersistenceServices.Forms.Domain.Entities;
using PersistenceServices.Forms.Domain.Exceptions;
using PersistenceServices.Forms.Domain.Interfaces;

namespace PersistenceServices.Forms.Domain.Services
{
    public class FormsPersistenceService : IFormsPersistenceService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ILoggerService _loggingService;

        public FormsPersistenceService(IUnitOfWork unitOfWork, ILoggerService loggingService)
        {
            _unitOfWork = unitOfWork;

            _loggingService = loggingService;
        }

        public async Task<string> GetFormDataAsync(Guid formDataId)
        {
            var formDataEntity = await GetFormDataEntity(formDataId);

            if (formDataEntity == null)
            {
                throw new NotFoundException(string.Format("Form data not found for id {0}", formDataId));
            }

            return formDataEntity.SerializedFormData;
        }

        public async Task SaveFormDataAsync(Guid formDataId, string serializedFormData)
        {
            var formDataEntity = await GetFormDataEntity(formDataId);

            if (formDataEntity == null)
            {
                formDataEntity = new FormDataEntity(formDataId, serializedFormData);
                _unitOfWork.Repository<FormDataEntity>().Add(formDataEntity);
            }
            else
            {
                formDataEntity.UpdateFormData(serializedFormData);
                _unitOfWork.Repository<FormDataEntity>().Update(formDataEntity);
            }

            await _unitOfWork.SaveChanges();
        }

        public async Task RemoveFormDataAsync(Guid formDataId)
        {
            var formDataEntity = await GetFormDataEntity(formDataId);

            if (formDataEntity == null)
            {
                throw new NotFoundException(string.Format("Form data not found for id {0}", formDataId));
            }

            _unitOfWork.Repository<FormDataEntity>().Remove(formDataEntity);

            await _unitOfWork.SaveChanges();
        }

        private async Task<FormDataEntity> GetFormDataEntity(Guid formDataId)
        {
            _loggingService.Info(string.Format("Sending to database for ID '{0}'", formDataId));
            return await _unitOfWork.Repository<FormDataEntity>().GetAsync(fp => fp.FormDataId == formDataId);
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}