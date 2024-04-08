namespace FrightForce.Domain.Documents;

public class DownloadDto
{
    public string Name { get; set; }
    public string FileType { get; set; }
    public byte[] FileBytes { get; set; }
}