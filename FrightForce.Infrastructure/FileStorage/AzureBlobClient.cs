using Azure.Storage;
using Azure.Storage.Blobs;
using FrightForce.Domain.Documents;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FrightForce.Infractructure.FileStorage;

public class AzureBlobClient: IStorageClient
{
     private readonly string _connectionString;
    private readonly string _accountKey;
    private readonly string _accountName;
    private readonly ILogger<AzureBlobClient> _logger;

    public AzureBlobClient(IOptions<FileClientConnectionStrings> connectionStrings, ILogger<AzureBlobClient> logger)
    {
        _connectionString = connectionStrings.Value.BlobConnectionString;
        _accountKey = connectionStrings.Value.AccountKey;
        _accountName = connectionStrings.Value.AccountName;
        _logger = logger;
    }
    public async Task<FileUploadResponse> UploadFileAsync(Stream stream, string fileName, string containerName)
        {
            try
            {
                var blobClient = await CreateBlobClient(fileName, containerName);


                var Blobinfo = await blobClient.UploadAsync(stream, true);
                return new FileUploadResponse
                {
                    Uri = blobClient.Uri.ToString()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Occurred AzureBlobClient uploadFileAsync", ex.Message);
                throw new Exception(ex.Message);
            }


        }
    

        public async Task<byte[]> DownloadFileAsync(string reference)
        {
            try
            {
                Uri uri = new Uri(reference);
                var blobClient = await CreateBlobClientByUri(uri);
                var stream = new MemoryStream();
                await blobClient.DownloadToAsync(stream);
                byte[] data = stream.ToArray();
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Occurred When Downloading blob -{reference}", ex.Message);
                throw new Exception(ex.Message);
            }

        }



        public async Task<string> DeleteFileAsync(string fileName, string containerName)
        {
            try
            {
                BlobClient blobClient = await CreateBlobClient(fileName, containerName);
                var res = await  blobClient.DeleteAsync();
                return res.ClientRequestId;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured AzureBlobClient DeleteFileAsync", ex.Message);
                throw new Exception(ex.Message);
            }
        }

        private async Task<BlobClient> CreateBlobClient(string fileName, string containerName)
        {
            BlobServiceClient _blobServiceClient = new BlobServiceClient(_connectionString);
            var ContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await ContainerClient.CreateIfNotExistsAsync();
            var blobClient = ContainerClient.GetBlobClient(fileName);
            return blobClient;
        }

        private async Task<BlobClient> CreateBlobClientByUri(Uri uri)
        {
            BlobClient blobclient = new BlobClient(uri, new StorageSharedKeyCredential(_accountName, _accountKey));
            return blobclient;
        }
}