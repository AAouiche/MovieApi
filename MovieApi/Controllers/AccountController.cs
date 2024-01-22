
using Application.DTO;
using Application.Handlers.Account;
using Domain.DTO;
using Domain.Models;
using Infrastructure.Services;
using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace MovieApi.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly TokenService _tokenService;

        public AccountController( TokenService tokenService, ILogger<BaseApiController> logger) : base(logger)
        {
            _tokenService = tokenService;
           
            
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
        [HttpPost("addWatchedMovie")]
        public async Task<IActionResult> AddWatchedMovie(Movie movie)
        {
            var command = new AddWatchedMovie.AddWatchedMovieCommand { Movie = movie};
            var result = await Mediator.Send(command);

           

            return HandleResults(result);
        }
        [HttpGet("List")]
        public async Task<ActionResult<List<Movie>>> ListWatchedMovies()
        {
            var query = new ListWatchedMovie.ListWatchedMoviesQuery();
            return HandleResults(await Mediator.Send(query));
        }

        //Persistence
        [HttpPost("validateToken")]
        public IActionResult ValidateToken(TokenDTO token)
        {
            try
            {
                _tokenService.ValidateToken(token);
                return Ok();
            }
            catch (SecurityTokenException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
        [HttpGet("getUser")]
        public async Task<ActionResult<UserDTO>> GetUser()
        {
            return HandleResults(await Mediator.Send(new GetUser.Command()));
        }
    }
}
