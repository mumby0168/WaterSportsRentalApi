using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SplashRentals.Application.Commands.Assets;

namespace SplashRentals.Web.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class AssetController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AssetController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAsset command) => 
            Ok(await _mediator.Send(command));
    }
}