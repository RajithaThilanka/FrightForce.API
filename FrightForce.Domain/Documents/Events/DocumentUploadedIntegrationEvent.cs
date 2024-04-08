using FrightForce.Domain.Base;

namespace FrightForce.Domain.Documents.Events;

public record DocumentUploadedIntegrationEvent(int DocketId,  List<int>DocumentIds, string AccesRoles,int CompanyId) : IIntegrationEvent; 