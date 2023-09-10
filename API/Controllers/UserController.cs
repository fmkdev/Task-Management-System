using APi.Filters;
using Application.Commands.Users.CreateUserCommand;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediatr;

        public UserController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpPut("{userId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(BaseResponse))]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand request)
        {
            var response = await _mediatr.Send(new CreateUserCommand(request.name, request.email));
            return response.Succeeded ? Ok(response) : BadRequest(response);
        }
    }
}
