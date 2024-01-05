﻿using Application.Utility;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MovieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        private IMediator _mediator;
        private readonly ILogger<BaseApiController> _logger;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        public BaseApiController(ILogger<BaseApiController>?logger = null)
        {
            _logger = logger;
        }

        protected ActionResult HandleResults<T>(Result<T> result)
        {
            
            if (result.Success && result.Value == null)
            {
                string errorMessage = string.IsNullOrEmpty(result.Message) ? "Data not found" : result.Message;
                _logger.LogWarning("HandleResults - Non-successful result: {ErrorMessage}", errorMessage);
                return NotFound(new { Error = errorMessage });
            }
            else if (result.Success && result.Value != null)
            {
                _logger.LogInformation("HandleResults - Successful result");
                return Ok(result.Value);
            }
            else if(result.Error == ErrorCode.Unathorized)
            {
                _logger.LogWarning("HandleResults - Unauthorized result : {ErrorMessage}", result.Message);
                return Unauthorized(new {Erro = result.Message});
            }
            return BadRequest(result.Error);


        }
    }

}
