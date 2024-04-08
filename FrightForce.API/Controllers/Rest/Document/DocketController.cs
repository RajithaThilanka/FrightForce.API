using FrightForce.API.Controllers.Rest;
using FrightForce.Application.Features.Document.Docket.Commands;
using FrightForce.Application.Features.Document.Docket.Queries;
using Microsoft.AspNetCore.Mvc;

namespace FrightForce.API.Controllers.Document;

public class DocketController:ApiControllerBase
{

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> CreateDocument(
        [FromBody] CreateDocketCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }
    [HttpPost]
    [Route("{docketId:int}/documents")]
    public async Task<IActionResult> CreateDocument(
        [FromBody] CreateDocumentCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }
    [HttpGet]
    [Route("{docketId:int}")]
    public async Task<IActionResult> GetDocket([FromRoute] int docketId,
        [FromQuery(Name = "fetchDocuments")] bool fetchDocuments)
    {
        var docket = await Mediator.Send(new GetDocketQuery
        {
            DocketId = docketId,
            FetchDocuments = fetchDocuments
        });
        return Ok(docket);
    }
    [HttpGet]
    [Route("{docketId:int}/documents/{documentId:int}")]
    public async Task<IActionResult> GetAllDocuments([FromRoute] int docketId, int documentId)
    {

        var result = await Mediator.Send(new GetAllDocumentsQuery { DocketId = docketId, DocumentId = documentId, });

        return Ok(result);

    }

    [HttpPost]
    [Route("{docketId:int}/documents/upload")]
    public async Task<IActionResult> UploadNewDocuments([FromRoute] int docketId, [FromForm] UploadMultipleDocumentsCommand command)
    {
        if(docketId != command.DocketId)
        {
            return BadRequest();
        }
        var result = await Mediator.Send(command);
        return Ok(result);

    }


    [HttpGet]
    [Route("{docketId:int}/documents/{documentId:int}/download")]
    public async Task<IActionResult> DownloadDocument([FromRoute] int docketId, int documentId)
    {
        var result = await Mediator.Send(new DownloadDocumentQuery
        {
            DocketId = docketId,
            DocumentId = documentId,
        });
        if (result == null)
        {
            return NotFound();
        }
        else
        {
            return Ok(result);
        }
    }
}