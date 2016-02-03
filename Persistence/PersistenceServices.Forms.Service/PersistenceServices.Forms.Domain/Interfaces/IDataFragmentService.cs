using System.Collections.Generic;

namespace PersistenceServices.Forms.Domain.Interfaces
{
    public interface IDataFragmentService
    {
        string GetFragment(string serializedFormData, string fragmentName, KeyValuePair<string, string> fragmentFilter);

        string UpdateFragment(string serializedFormData, string serializedFragmentData, string fragmentName, KeyValuePair<string, string> fragmentFilter);

        string RemoveFragment(string serializedFormData, string fragmentName, KeyValuePair<string, string> fragmentFilter);
    }
}