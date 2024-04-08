using FrightForce.Domain.Documents;

namespace FrightForce.Infractructure.FileStorage;

public class StorageProvider: IStorageProvider
{
    private readonly IStorageClient _azureBlobClient;

    public StorageProvider(IStorageClient azureBlobClient)
    {
        _azureBlobClient = azureBlobClient;
    }
    
    public IStorageClient GetClient(StorageType providerType)
    {
        switch (providerType)
        {
            case StorageType.AzureBlob:
                return _azureBlobClient;
            default:
                return _azureBlobClient;

        }
    }
}
    