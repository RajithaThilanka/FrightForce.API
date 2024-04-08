using Microsoft.AspNetCore.Http;

namespace FrightForce.Domain.Documents;

public record UploadDto(IFormFile document, int documentTypeId,string? name);