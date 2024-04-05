namespace FrightForce.Domain.Documents;

public class DocketDocument
{
    public int DocketId { get; set; }
    public Docket Docket { get; set; }

    public int DocumentId { get; set; }
    public Document Document { get; set; }
}