using Application.Utility;
using Domain.Models;
using Domain.Return.DTO;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.Account
{
    public class UpdateProfile
    {
        public class UpdateProfileCommand : IRequest<Result<UserDTO>>
        {
            public UpdateProfileDTO UpdateDetails { get; set; }
        }

        public class UpdateProfileHandler : IRequestHandler<UpdateProfileCommand, Result<UserDTO>>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly ILogger<UpdateProfileHandler> _logger;

            public UpdateProfileHandler(UserManager<ApplicationUser> userManager, ILogger<UpdateProfileHandler> logger)
            {
                _userManager = userManager;
                _logger = logger;
            }

            public async Task<Result<UserDTO>> Handle(UpdateProfileCommand command, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(command.UpdateDetails.Email);
                if (user == null)
                {
                    return Result<UserDTO>.Failure("User not found", ErrorCode.Unauthorized);
                }

                
                user.UserName = command.UpdateDetails.UserName;
                user.FirstName = command.UpdateDetails.FirstName;
                user.LastName = command.UpdateDetails.LastName;

                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                {
                    _logger.LogWarning("Error updating user: {Errors}", updateResult.Errors);
                    return Result<UserDTO>.Failure("Error updating user profile", ErrorCode.GeneralError);
                }

                
                var updatedUserDTO = new UserDTO
                {
                    Email = user.Email,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    ImageUrl = user.Image.Url
                };

                return Result<UserDTO>.SuccessResult(updatedUserDTO);
            }
        }
    }
}