
using Application.Handlers.ImageUpload;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MovieApi.Controllers
{
    public class ImageController : BaseApiController
    {
        private readonly IMediator _mediator;

        public ImageController(IMediator mediator, ILogger<ImageController> logger)
            : base(logger)
        {
            _mediator = mediator;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return BadRequest("No file provided or file is empty.");
            }

            var command = new ImageUpload.Command
            {
                ImageFile = imageFile,

            };

            var result = await _mediator.Send(command);



            return HandleResults(result);
        }

    }
}
