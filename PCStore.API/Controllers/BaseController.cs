using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PCStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IMediator? _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>() ??
            throw new InvalidOperationException("IMediator service is not registered!");

    }
}
