using System;
using System.Threading.Tasks;
using IPO.PersistenceServices.RestClient.Interfaces;
using IPO.PersistenceServices.Files.Api.Client.Interfaces;

namespace IPO.PersistenceServices.Files.Api.Client
{
    public class FilesPersistenceClient : IFilesPersistenceClient
    {
        private readonly IRestClient _client;

        private const string BasePath = "Api/V1/File";

        public FilesPersistenceClient(IRestClient client)
        {
            _client = client;
        }

        public async Task<T2>SaveFileDataAsync<T1, T2>(T1 fileBlobServiceRequestDto)
        {
            var path = string.Format("{0}", BasePath);

            var fileBlobServiceResponseDto = await _client.PostItemAsync<T1, T2>(path, fileBlobServiceRequestDto);
            return fileBlobServiceResponseDto;
        }

        public async Task<T> GetFileDataAsync<T>(Guid fileGuid)
        {
            var path = string.Format("{0}/{1}", BasePath, fileGuid);

            var fileBlobServiceResponseDto = await _client.GetItemAsync<T>(path);
            return fileBlobServiceResponseDto;
        }

        public async Task RemoveFileDataAsync(Guid fileGuid)
        {
            var path = string.Format("{0}/{1}", BasePath, fileGuid);
            await _client.DeleteItemAsync(path);
        }
    }
}