using Application.Utility;
using Domain.DTO;
using Domain.Interfaces.IRepositories;
using Domain.Models;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.Account
{
    public class Login
    {

        public class LoginCommand : IRequest<Result<UserDTO>>
        {
            public LoginDTO LoginDetails { get; set; }
        }

        public class Handler:IRequestHandler<LoginCommand , Result<UserDTO>>
        {
            
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly TokenService _service;

            public Handler(UserManager<ApplicationUser> userManager,TokenService service)
            {
                
                _userManager = userManager;
                _service = service;
            }
            public async Task<Result<UserDTO>> Handle(LoginCommand command,CancellationToken cancellationToken)
            {
                
                    var user = await _userManager.FindByEmailAsync(command.LoginDetails.Email);
                    if (user == null)
                    {
                        return Result<UserDTO>.Failure("The user does not exist", ErrorCode.Unauthorized);

                    };
                    var checkedPass = await _userManager.CheckPasswordAsync(user, command.LoginDetails.Password);
                    if (!checkedPass)
                    {
                        return Result<UserDTO>.Failure("Incorrect Password", ErrorCode.Unauthorized);
                    }
                    var loggedInUser = new UserDTO
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        ImageUrl = user.ImgUrl,
                        Token = _service.Token(user)
                    };


                    return Result<UserDTO>.SuccessResult(loggedInUser);
                
               
                
               
            }
        }
    

    

    }
}
