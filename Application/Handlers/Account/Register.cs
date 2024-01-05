using Application.Utility;
using Domain.DTO;
using Domain.Models;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Handlers.Account.Login;

namespace Application.Handlers.Account
{
    public class Register
    {
        public class RegisterCommand:IRequest<Result<UserDTO>>
        {
            public RegisterDTO RegisterDetails {  get; set; }
        }

        public class RegisterHandler:IRequestHandler<RegisterCommand, Result<UserDTO>>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly ILogger<RegisterCommand> _logger;
            private readonly TokenService _tokenService;
          public RegisterHandler(UserManager<ApplicationUser> userManager,ILogger<RegisterCommand> logger,TokenService tokenService)
           {
                _userManager = userManager;
                _logger = logger;
                _tokenService = tokenService;
           }
         public async Task<Result<UserDTO>> Handle(RegisterCommand command,CancellationToken cancellationToken)
          {
                var newUser = new ApplicationUser
                {
                    Email = command.RegisterDetails.Email,
                    UserName = command.RegisterDetails.UserName,
                    ImgUrl = null
                };

               var created = await _userManager.CreateAsync(newUser,command.RegisterDetails.Password);

               if (created != null) 
               {
                    _logger.LogWarning("Error creating user: {Errors}", created.Errors);
                    return Result<UserDTO>.Failure("Error Creating New User", ErrorCode.GeneralError);
               }

                return Result<UserDTO>.SuccessResult(new UserDTO
                {
                    Email = newUser.Email,
                    UserName = newUser.UserName,
                    Token = _tokenService.Token(newUser),
                    ImageUrl = null

                }); 
           }
         }
    
     

    }
    
}
