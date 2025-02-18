using BoatApplication.Application.Boat.Queries.GetBoat;
using BoatApplication.Application.Boats.Commands.CreateBoat;
using BoatApplication.Application.Boats.Commands.DeleteBoat;
using BoatApplication.Application.Boats.Commands.PurgeBoats;
using BoatApplication.Application.Boats.Commands.UpdateBoat;
using BoatApplication.Application.Boats.Models;
using BoatApplication.Application.Boats.Queries.GetBoats;
using BoatApplication.Domain.Common.Models;
using BoatApplication.Domain.Identity.Commands.Login;
using BoatApplication.Domain.Identity.Commands.RefreshToken;
using BoatApplication.Domain.Identity.Commands.RegisterUser;
using BoatApplication.Domain.Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApp.Server.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [Authorize]
    public class BoatController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BoatController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private async Task<IActionResult> HandleRequest<T>(IRequest<T> request)
            where T : BaseAPIResponse
        {
            var response = await _mediator.Send(request);
            if (response.Status == BaseAPIResponse.EStatus.Error)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpGet("boats")]
        [SwaggerResponse(200, typeof(GetBoatsResponse))]
        [SwaggerResponse(400, typeof(GetBoatsResponse))]
        public async Task<IActionResult> Boats([FromQuery] GetBoatsQuery query)
        {
            return await HandleRequest(query);
        }

        [HttpGet("boat")]
        [SwaggerResponse(200, typeof(GetBoatResponse))]
        [SwaggerResponse(400, typeof(GetBoatResponse))]
        public async Task<IActionResult> Boat([FromQuery] GetBoatQuery query)
        {
            return await HandleRequest(query);
        }

        [HttpPost("create")]
        [SwaggerResponse(200, typeof(GetBoatResponse))]
        [SwaggerResponse(400, typeof(GetBoatResponse))]
        public async Task<IActionResult> Create([FromBody] CreateBoatCommand command)
        {
            return await HandleRequest(command);
        }

        [HttpPost("update")]
        [SwaggerResponse(200, typeof(BaseAPIResponse))]
        [SwaggerResponse(400, typeof(BaseAPIResponse))]
        public async Task<IActionResult> Update([FromBody] UpdateBoatCommand command)
        {
            return await HandleRequest(command);
        }

        [HttpDelete("delete")]
        [SwaggerResponse(200, typeof(BaseAPIResponse))]
        [SwaggerResponse(400, typeof(BaseAPIResponse))]
        public async Task<IActionResult> Delete([FromQuery] DeleteBoatCommand command)
        {
            return await HandleRequest(command);
        }

        [HttpGet("purge")]
        [SwaggerResponse(200, typeof(PurgeBoatResponse))]
        [SwaggerResponse(400, typeof(PurgeBoatResponse))]
        public async Task<IActionResult> Purge()
        {
            return await HandleRequest(new PurgeBoatsCommand());
        }
    }
}
