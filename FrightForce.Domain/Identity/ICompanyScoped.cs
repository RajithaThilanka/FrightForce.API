namespace FrightForce.Domain.Identity;

public interface ICompanyScoped<TKey>
{
    public TKey CompanyId { get; set; }
}