using Application.Utility;
using Domain.Return.DTO;
using Infrastructure.AppDbContext;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.Account
{
    public class GetUser
    {
        public class Command : IRequest<Result<UserDTO>>
        {
        }
        public class Handler : IRequestHandler<GetUser.Command, Result<UserDTO>>
        {
            private readonly MovieContext _context;
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly ILogger<GetUser> _logger;

            public Handler(MovieContext context, IHttpContextAccessor httpContextAccessor, ILogger<GetUser> logger)
            {
                _context = context;
                _httpContextAccessor = httpContextAccessor;
                _logger = logger;
            }
            
            public async Task<Result<UserDTO>> Handle(GetUser.Command request, CancellationToken cancellationToken)
            {
               
                    
                    if (_httpContextAccessor.HttpContext == null || _httpContextAccessor.HttpContext.User == null)
                    {
                        _logger.LogError("HttpContext or User is null");
                        return Result<UserDTO>.Failure("HttpContext or User is null", ErrorCode.Unauthorized);
                    }

                    
                    

                    
                    var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                    if (string.IsNullOrEmpty(userId))
                    {
                        _logger.LogError("User ID not found in claims");
                        return Result<UserDTO>.Failure("User ID not found in claims", ErrorCode.Unauthorized);
                    }

                   
                    var user = await _context.Users
                    .Include(u => u.Image)
                    .SingleOrDefaultAsync(u => u.Id == userId);
                    if (user == null)
                    {
                        _logger.LogError($"User not found in database for ID: {userId}");
                        return Result<UserDTO>.Failure("User not found in database", ErrorCode.Unauthorized);
                    }

                    var userDto = new UserDTO
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        WatchedMovies = user.WatchedMovies,
                        Reviews = user.Reviews,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        ImageUrl = user.Image?.Url
                    };

                    return Result<UserDTO>.SuccessResult(userDto);
                
                
            }
        }
    }
}
