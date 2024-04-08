using FrightForce.Application.Common.Exceptions;
using FrightForce.Domain.Base;
using FrightForce.Domain.Documents;
using FrightForce.Domain.Documents.Events;
using FrightForce.Domain.Identity;
using Microsoft.AspNetCore.Http;

namespace FrightForce.Application.Services;

public class DocumentService:IDocumentService
{
    private readonly IDocketRepository _docketRepository;
    private readonly IDocumentRepository _documentRepository;
    private readonly IStorageProvider _storageProvider;
    private readonly IBusPublisher _busPublisher;
    private readonly ICurrentUserService _currentUserService;

    public DocumentService(
        ICurrentUserService currentUserService,
        IBusPublisher busPublisher, 
        IStorageProvider storageProvider,
        IDocumentRepository documentRepository,
        IDocketRepository docketRepository
        )
    {
        _currentUserService = currentUserService;
        _busPublisher = busPublisher;
        _storageProvider = storageProvider;
        _documentRepository = documentRepository;
        _docketRepository = docketRepository;
    }
    
     public async Task<Docket> CreateDocketAsync( int companyId, string name)
        {
            
            Docket docket = Docket.Create(companyId,  name,"compantcode");
            await _docketRepository.AddAsync(docket);

            List<Document> documents = new List<Document>();
            var doc = await _documentRepository.AddRangeAsync(documents);
            docket.AddDocuments(doc.ToList());
            await _docketRepository.UpdateAsync(docket);
            
            return docket;
        }
    public async Task<Docket> GetDocketAsync(int docketId, bool fetchDocuments)
    {
        if (fetchDocuments)
        {
            return await _docketRepository.FetchDocketWithDocumentsAsync(docketId);
        }
        else
        {
            return await _docketRepository.FindByIdAsync(docketId);

        }
    }
    public async Task<Document> CreateDocumentAsync(int docketId, int documentTypeId, string name)
    {

        Docket? docket = await _docketRepository.FindByIdAsync(docketId);

        if (docket == null)
        {
            throw new BusinessException.DocketNotFoundException("Docket Does Not Exists.");
        }

        Document document = Document.Create(name, documentTypeId, docket.ContainerName, docket.CompanyId);

        document = await _documentRepository.AddAsync(document);
        docket.AddDocument(document);

        await _docketRepository.UpdateAsync(docket);

        document.AddDomainEvent(new DocumentCreatedEvent());

        return document;

    }
    public async Task<Document> UploadNewDocumentAsync(int docketId, int documentTypeId, string name, IFormFile content)
    {
        Document document = await CreateDocumentAsync(docketId, documentTypeId, name);
        return await UploadFileAsync(docketId,document.Id, content, name, false);
    }

  

    public async Task<List<Document>> UploadMultipleDocumentsAsync(List<UploadDto> files, int docketId)
    {
        List<Document> documents = new List<Document>();
        Docket? docket = await _docketRepository.FindByIdAsync(docketId);
        
        if (docket == null)
        {
            throw new Exception("Docket Does Not Exists.");
        }
        
        foreach (var file in files)
        {
            Document document = Document.Create(file.name, file.documentTypeId, docket.ContainerName, docket.CompanyId);

            using (var stream = file.document.OpenReadStream())
            {
                await document.Upload(stream, _storageProvider, file.document.ContentType);
            }
            documents.Add(document);


        }
        var doc = await _documentRepository.AddDocumentsRangeAsync(documents);
        await _busPublisher.SendAsync(new DocumentUploadedIntegrationEvent(
            docketId, 
            doc.Select(d => d.Id).ToList(),
            "", _currentUserService.CurrentCompanyId));
        docket.AddDocuments(doc.ToList());
        await _docketRepository.UpdateAsync(docket);
        return documents;
    }


    public async Task<Document> UploadFileAsync(int docketId,int documentId, IFormFile content, string? name, bool isRenaming)
    {
        Document? document = await _documentRepository.FetchDocumentAsync(documentId);
        
            
        using (var file = content.OpenReadStream())
        {
            await document.Upload(file, _storageProvider, content.ContentType);
        }

        if (isRenaming)
        {
            document.UpdateName(name);
        }
        document.FileType=content.ContentType;
        document = await _documentRepository.UpdateDocumentAsync(document,true);
        document.AddDomainEvent(new DocumentUploadedEvent());
        await _busPublisher.SendAsync(new DocumentUploadedIntegrationEvent(
            docketId,
            new List<int> { documentId },
            "", 
            _currentUserService.CurrentCompanyId));
        return document;

    }
    
    public async Task<Document> GetDocumentsAsync(int docketId, int documentId)
    {
        try
        {
            Document document = await _documentRepository.FetchDocumentAsync(documentId);
            return document;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Document?> GetDocumentByIdAsync(int id)
    {
        try
        {
            return await _documentRepository.FetchDocumentAsync(id);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    
    public async Task<DownloadDto> DownloadFileAsync(int docketId, int documentId)
    {
        Docket? docket = await _docketRepository.FetchDocketWithDocumentsAsync(docketId);
        
        Document? document = docket.Documents.SingleOrDefault(x => x.Id == documentId);
        
        byte[] fileBytes = await document.Download(_storageProvider);
        return new DownloadDto { Name = document.Name, FileBytes = fileBytes,FileType = document.FileType};

    }
}