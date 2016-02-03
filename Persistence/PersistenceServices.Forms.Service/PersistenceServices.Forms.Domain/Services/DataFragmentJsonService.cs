using System;
using System.Collections.Generic;
using System.Linq;
using IPO.PersistenceServices.SerializationService.Interfaces;
using Newtonsoft.Json.Linq;
using PersistenceServices.Forms.Domain.Exceptions;
using PersistenceServices.Forms.Domain.Interfaces;

namespace PersistenceServices.Forms.Domain.Services
{
    public class DataFragmentJsonService : IDataFragmentService
    {
        private readonly ISerializationService _serializationService;

        public DataFragmentJsonService(ISerializationService serializationService)
        {
            _serializationService = serializationService;
        }

        public string GetFragment(string serializedFormData, string fragmentName, KeyValuePair<string, string> fragmentNameFilter)
        {
            var formDataModel = DeserializeObject<JObject>(serializedFormData);
            JProperty dataFragment;

            if (fragmentNameFilter.Key == null)
            {
                dataFragment = GetFragmentProperty(formDataModel, fragmentName);
                return _serializationService.SerializeFormData(dataFragment.Value);
            }

            dataFragment = GetFilteredFragmentProperty(formDataModel, fragmentName, fragmentNameFilter);
            return _serializationService.SerializeFormData(dataFragment.Parent.ToObject<JObject>());
        }

        public string UpdateFragment(string serializedFormData, string serializedFragmentData, string fragmentName, KeyValuePair<string, string> fragmentNameFilter)
        {
            var formDataModel = DeserializeObject<JObject>(serializedFormData);
            var newFragment = DeserializeObject<JToken>(serializedFragmentData);
            JProperty dataFragment;

            if (fragmentNameFilter.Key == null)
            {

                dataFragment = GetFragmentProperty(formDataModel, fragmentName);
                dataFragment.Value = newFragment;
            }
            else
            {
                dataFragment = GetFilteredFragmentProperty(formDataModel, fragmentName, fragmentNameFilter);
                dataFragment.Parent.Replace(newFragment);
            }

            return _serializationService.SerializeFormData(formDataModel);
        }

        public string RemoveFragment(string serializedFormData, string fragmentName, KeyValuePair<string, string> fragmentNameFilter)
        {
            var formDataModel = DeserializeObject<JObject>(serializedFormData);
            JProperty dataFragment;

            if (fragmentNameFilter.Key == null)
            {
                dataFragment = GetFragmentProperty(formDataModel, fragmentName);
                dataFragment.Remove();
            }
            else
            {
                dataFragment = GetFilteredFragmentProperty(formDataModel, fragmentName, fragmentNameFilter);
                dataFragment.Parent.Remove();
            }

            return _serializationService.SerializeFormData(formDataModel);
        }

        private T DeserializeObject<T>(string serializedData)
        {
            T deserializedObject;

            try
            {
                deserializedObject = _serializationService.DeSerializeFormData<T>(serializedData);
            }
            catch (Exception ex)
            {
                throw new UnsupportedOperationException("Unsupported operation. Data must be in the JSON format", ex);
            }

            return deserializedObject;
        }

        private static JProperty GetFragmentProperty(JObject formDataObject, string fragmentName)
        {
            var fragmentProperty = formDataObject.Descendants().OfType<JProperty>().FirstOrDefault(p => p.Path.Replace(".$values", string.Empty).Split('.').Last().ToLower() == fragmentName.ToLower());

            if (fragmentProperty == null)
            {
                throw new NotFoundException(string.Format("Fragment {0}' not found", fragmentName));
            }

            return fragmentProperty;
        }

        private static JProperty GetFilteredFragmentProperty(JObject formDataObject, string fragmentName, KeyValuePair<string, string> fragmentNameFilter)
        {
            var fragmentProperty = formDataObject
                .Descendants().OfType<JProperty>()
                .Where(x => x.Path.Split('.').Last().ToLower() == fragmentNameFilter.Key.ToLower() && x.Value.ToString().ToLower() == fragmentNameFilter.Value.ToLower())
                .FirstOrDefault(p => p.Parent.Parent != null && p.Parent.Parent.Path.Replace(".$values", string.Empty).Split('.').Last().ToLower() == fragmentName.ToLower());

            if (fragmentProperty == null)
            {
                throw new NotFoundException(string.Format("Fragment not found for '{0}' with filter '{1}={2}'", fragmentName, fragmentNameFilter.Key, fragmentNameFilter.Value));
            }

            return fragmentProperty;
        }
    }
}