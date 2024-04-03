using Application.Utility;
using Domain.Interfaces.Security;
using MediatR;
using Microsoft.AspNetCore.Http;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Domain.Models;
using Domain.Return.DTO;
using CloudinaryDotNet;

namespace Application.Handlers.ImageUpload
{
    public class ImageUpload
    {
        public class Command : IRequest<Result<ImageReturnDTO>>
        {

            public IFormFile ImageFile { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<ImageReturnDTO>>
        {

            private readonly IImageRepository _imageRepository;
            private readonly IAccessUser _accessUser;
            private readonly ICloudinaryService _cloudinaryService;

            public Handler(IImageRepository imageRepository, IAccessUser accessUser, ICloudinaryService cloudinaryService)
            {

                _imageRepository = imageRepository;
                _accessUser = accessUser;
                _cloudinaryService = cloudinaryService;
            }

            public async Task<Result<ImageReturnDTO>> Handle(Command request, CancellationToken cancellationToken)
            {

                var (imageUrl, PublicId) = await _cloudinaryService.UploadImageAsync(request.ImageFile);

                var userId = _accessUser.GetUserId();
                var image = new Image
                {
                    Url = imageUrl,
                    FileName = request.ImageFile.FileName,
                    Size = request.ImageFile.Length,
                    ContentType = request.ImageFile.ContentType,
                    ApplicationUserId = userId,
                    PublicId = PublicId

                };


                await _imageRepository.CreateAsync(image);
                var test = await _accessUser.GetUser();

                return Result<ImageReturnDTO>.SuccessResult(new ImageReturnDTO { url = image.Url });
            }
        }
    }
}
