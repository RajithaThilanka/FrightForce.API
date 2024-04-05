using FrightForce.Domain.Base;
using FrightForce.Domain.Identity;

namespace FrightForce.Domain.Documents;

public class Docket: AuditableDomainEntity<int>, ICompanyScoped<int>
{
    public Guid Guid { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; }
    public string ContainerName { get; private  set; }
    public bool IsLocked { get; private  set; } = false;
    public List<Document> Documents { get; private set; } = new List<Document>();
    public bool IsDocket { get; private set; } = true;
    public int CompanyId { get; set; }
    
    
    
    public static Docket Create(int companyId, string name,string companyCode)
    {
        string containerName = GenerateContainerName(name, companyCode);
        Docket docket = new Docket(companyId, name, containerName);
        return docket;
    }
    public void AddDocument(Document document)
    {
        Documents.Add(document);
    }
    private Docket(int companyId, string name, string containerName)
    {
        CompanyId = companyId;
        Name = name;
        ContainerName = containerName;
    }
    public void AddDocuments(List<Document> document)
    {
        Documents.AddRange(document);
    }
    
    private static string GenerateContainerName(string docketName, string companyCode)
    {

        string containerName = $"{companyCode}-{docketName}";
        containerName = containerName.Replace(" ", "");
        return containerName.ToLower();

    } 
}