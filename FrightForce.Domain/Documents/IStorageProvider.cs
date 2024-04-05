namespace FrightForce.Domain.Documents;

public interface IStorageProvider
{
    public IStorageClient GetClient(StorageType providerType);
}