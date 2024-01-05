
using Application.Handlers.Account;
using Domain.DTO;
using Domain.Models;
using Infrastructure.Services;
using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MovieApi.Controllers
{
    public class AccountController : BaseApiController
    {
        
        private readonly TokenService _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountController( TokenService tokenService)
        {
            
            _tokenService = tokenService;
            
        }

        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDetails)
        {
            
            return HandleResults(await Mediator.Send(new Login.LoginCommand { LoginDetails = loginDetails }));
        }
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDetails)
        {

        }


        
    }
}
