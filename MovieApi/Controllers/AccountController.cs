
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
        
        
        public AccountController( TokenService tokenService, ILogger<BaseApiController> logger) : base(logger)
        {
            
           
            
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDetails)
        {
            try
            {
                return HandleResults(await Mediator.Send(new Login.LoginCommand { LoginDetails = loginDetails }));
            }
            catch (Exception ex) { throw new Exception("controller", ex); }
            
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDetails)
        {
            return HandleResults(await Mediator.Send(new Register.RegisterCommand { RegisterDetails = registerDetails }));
        }


        
    }
}
