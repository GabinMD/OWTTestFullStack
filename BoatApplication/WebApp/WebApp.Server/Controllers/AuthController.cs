using BoatApplication.Domain.Identity.Commands.Login;
using BoatApplication.Domain.Identity.Commands.RefreshToken;
using BoatApplication.Domain.Identity.Commands.RegisterUser;
using BoatApplication.Domain.Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using static BoatApplication.Domain.Common.Models.BaseAPIResponse;

namespace WebApp.Server.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        [SwaggerResponse(200, typeof(AuthResponse))]
        [SwaggerResponse(400, typeof(AuthResponse))]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var response = await _mediator.Send(command);
            if (response.Status == EStatus.Error)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpPost("refresh-token")]
        [SwaggerResponse(200, typeof(AuthResponse))]
        [SwaggerResponse(400, typeof(AuthResponse))]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
        {
            var response = await _mediator.Send(command);
            if (response.Errors != null)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpPost("register")]
        [SwaggerResponse(200, typeof(AuthResponse))]
        [SwaggerResponse(400, typeof(AuthResponse))]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            var response = await _mediator.Send(command);
            if (response.Errors != null)
                return BadRequest(response);
            return Ok(response);
        }
    }
}
