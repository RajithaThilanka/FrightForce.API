namespace FrightForce.Domain.Documents;

public interface IStorageClient
{
    Task<FileUploadResponse> UploadFileAsync(Stream stream, string fileName, string containerName);
    Task<byte[]> DownloadFileAsync(string reference);
    Task<string> DeleteFileAsync(string fileName, string containerName);
}