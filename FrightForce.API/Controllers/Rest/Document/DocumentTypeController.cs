using FrightForce.API.Controllers.Rest;
using FrightForce.Application.Features.Document.DocumentType.Queries;
using Microsoft.AspNetCore.Mvc;

namespace FrightForce.API.Controllers.Document;

public class DocumentTypeController: ApiControllerBase
{                                                   
     [HttpGet]
     public async Task<IActionResult> GetDocumentTypes()
     {
         var result = await Mediator.Send(new GetAllDocumentTypesQuery());
         return Ok(result);
     }

     [HttpGet("{id}")]
     public async Task<IActionResult> GetDocumentType(int id)
     {
            var result = await Mediator.Send(new GetDocumentTypeById(id));
            return Ok(result);
     }

}