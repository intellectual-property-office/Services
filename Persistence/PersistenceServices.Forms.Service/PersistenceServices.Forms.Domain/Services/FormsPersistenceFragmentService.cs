using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PersistenceServices.Forms.Domain.Entities;
using PersistenceServices.Forms.Domain.Exceptions;
using PersistenceServices.Forms.Domain.Interfaces;

namespace PersistenceServices.Forms.Domain.Services
{
    public class FormsPersistenceFragmentService : IFormsPersistenceFragmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDataFragmentService _dataFragmentService;

        public FormsPersistenceFragmentService(IUnitOfWork unitOfWork, IDataFragmentService dataFragmentService)
        {
            _unitOfWork = unitOfWork;
            _dataFragmentService = dataFragmentService;
        }

        public async Task<string> GetFormDataFragmentAsync(Guid formDataId, string fragmentName, KeyValuePair<string, string> fragmentFilter)
        {
            var formDataEntity = await GetFormDataEntity(formDataId);

            return _dataFragmentService.GetFragment(formDataEntity.SerializedFormData, fragmentName, fragmentFilter);
        }

        public async Task SaveFormDataFragmentAsync(Guid formDataId, string fragmentName, KeyValuePair<string, string> fragmentFilter, string serializedFragmentData)
        {
            var formDataEntity = await GetFormDataEntity(formDataId);
            var serializedFormData = _dataFragmentService.UpdateFragment(formDataEntity.SerializedFormData, serializedFragmentData, fragmentName, fragmentFilter);

            formDataEntity.UpdateFormData(serializedFormData);
            _unitOfWork.Repository<FormDataEntity>().Update(formDataEntity);
            
            await _unitOfWork.SaveChanges();
        }

        public async Task RemoveFormDataFragmentAsync(Guid formDataId, string fragmentName, KeyValuePair<string, string> fragmentFilter)
        {
            var formDataEntity = await GetFormDataEntity(formDataId);
            var serializedFormData = _dataFragmentService.RemoveFragment(formDataEntity.SerializedFormData, fragmentName, fragmentFilter);
            
            formDataEntity.UpdateFormData(serializedFormData);
            _unitOfWork.Repository<FormDataEntity>().Update(formDataEntity);
            
            await _unitOfWork.SaveChanges();
        }

        private async Task<FormDataEntity> GetFormDataEntity(Guid formDataId)
        {
            var formDataEntity = await _unitOfWork.Repository<FormDataEntity>().GetAsync(fp => fp.FormDataId == formDataId);

            if (formDataEntity == null)
            {
                throw new NotFoundException(string.Format("Form data not found for id {0}", formDataId));
            }

            return formDataEntity;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}