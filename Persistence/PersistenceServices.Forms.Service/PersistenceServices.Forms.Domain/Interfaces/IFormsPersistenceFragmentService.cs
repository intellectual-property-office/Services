using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersistenceServices.Forms.Domain.Interfaces
{
    public interface IFormsPersistenceFragmentService : IDisposable
    {
        Task<string> GetFormDataFragmentAsync(Guid formDataId, string fragmentName, KeyValuePair<string, string> fragmentFilter);

        Task SaveFormDataFragmentAsync(Guid formDataId, string fragmentName, KeyValuePair<string, string> fragmentFilter, string serializedFragmentData);

        Task RemoveFormDataFragmentAsync(Guid formDataId, string fragmentName, KeyValuePair<string, string> fragmentFilter);
    }
}