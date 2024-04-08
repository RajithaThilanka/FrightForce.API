using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FrightForce.API.Controllers.Rest;


    [ApiController]
    [Route(BaseApiPath + "/v{version:apiVersion}/[controller]")]

    public class ApiControllerBase : ControllerBase
    {
        private const string BaseApiPath = "/Api";

        private ISender _mediator = null!;

        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    } 
