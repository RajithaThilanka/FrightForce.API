using FrightForce.Domain.Common.Services;

namespace FrightForce.Infractructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}