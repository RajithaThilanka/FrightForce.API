using System.Text.Json.Serialization;
using FrightForce.Domain.Base;
using FrightForce.Domain.Identity;

namespace FrightForce.Domain.Documents;

public class Document: AuditableDomainEntity<int>, ICompanyScoped<int>
{
    public Guid Guid { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; }
    public string? FileType { get; set; }
    public string ContainerName { get; private set; }
    public string? AzureBlobStorageUrl { get; private set; }
    [JsonIgnore] public ICollection<Docket> Dockets { get; private set; } = new List<Docket>();
    public int DocumentTypeId { get; private set; }
    public int CompanyId { get; set; }
    
    //private constructor
    private Document(string name, int documentTypeId, string containerName,int companyId)
    {
        Name = name;
        DocumentTypeId = documentTypeId;
        ContainerName = containerName;
        CompanyId = companyId;
    }
    
   
    public async Task<string> Upload(Stream stream, IStorageProvider provider, string fileType)
    {
        IStorageClient storageClient = provider.GetClient(StorageType.AzureBlob);
        FileUploadResponse res = null; 

        if (this.AzureBlobStorageUrl == null)
        {
            res = await storageClient.UploadFileAsync(stream, this.Name, this.ContainerName);
            this.AzureBlobStorageUrl = res?.Uri; 
        }
        return res.Uri;
    }
    public static Document Create(string name, int documentTypeId, string containerName,int companyId)
    {
        return new Document(name, documentTypeId, containerName,companyId);
    }
    public void UpdateName(string name)
    {
        Name = name;
    }
    public async Task<byte[]> Download(IStorageProvider provider)
    {
        IStorageClient storageClient = provider.GetClient(StorageType.AzureBlob);
        var file = await storageClient.DownloadFileAsync(this.AzureBlobStorageUrl);
        return file;
    }
}