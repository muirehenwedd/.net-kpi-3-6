using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender? _mediator;

    internal ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}