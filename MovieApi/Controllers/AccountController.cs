
using Application.DTO;
using Application.Handlers.Account;
using Application.Utility;
using Domain.DTO;
using Domain.Models;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MovieApi.Controllers
{
    
    public class AccountController : BaseApiController
    {
        private readonly TokenService _tokenService;
        private readonly ILogger<BaseApiController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController( TokenService tokenService, ILogger<BaseApiController> logger, IHttpContextAccessor httpContextAccessor) : base(logger)
        {
            _tokenService = tokenService;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;

           
            
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("addWatchedMovie")]
        public async Task<IActionResult> AddWatchedMovie(Movie movie)
        {
            var command = new AddWatchedMovie.AddWatchedMovieCommand { Movie = movie};
            var result = await Mediator.Send(command);

           

            return HandleResults(result);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("List")]
        public async Task<ActionResult<List<MovieDTO>>> ListWatchedMovies()
        {
            var query = new ListWatchedMovie.ListWatchedMoviesQuery();
            return HandleResults(await Mediator.Send(query));
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<List<MovieDTO>>> DeleteWatchedMovie(String id)
        {
            var query = new DeleteWatchedMovie.DeleteWatchedMovieCommand { MovieId = id };
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("getUser")]
        public async Task<ActionResult<UserDTO>> GetUser()
        {
            
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                 if (token == null)
                  {
                     return Unauthorized(new { message = "Token is required." });
                  }
                _tokenService.ValidateToken(new TokenDTO { Token = token });

                
                
               
                
                var result = await Mediator.Send(new GetUser.Command());
                return HandleResults(result);
            
        }
    }
}
