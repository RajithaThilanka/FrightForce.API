using FrightForce.Domain.Base;

namespace FrightForce.Domain.Documents;

public class DocumentType : AuditableDomainEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;

    public DocumentType() { }

    private DocumentType(string name, string code)
    {
        Name = name;
        Code = code;
    }

    public static DocumentType Create(string name, string code)
    {
        return new DocumentType(name, code);
    }

    public DocumentType Update(string name, string code)
    {
        Name = name;
        Code = code;

        return this;
    }
}