using Application.Utility;
using Domain.DTO;
using Infrastructure.AppDbContext;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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

            public Handler(MovieContext context, IHttpContextAccessor httpContextAccessor)
            {
                _context = context;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<Result<UserDTO>> Handle(GetUser.Command request, CancellationToken cancellationToken)
            {
                var email = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);
                if (string.IsNullOrEmpty(email))
                {
                    return Result<UserDTO>.Failure("User not found",ErrorCode.GeneralError);
                }

                var user = await _context.Users
                     .SingleOrDefaultAsync(u => u.Email == email);

               

                var userDto = new UserDTO
                {
                    
                    UserName = user.UserName,
                    Email = user.Email,
                    WatchedMovies = user.WatchedMovies,
                    Reviews = user.Reviews,
                    
                };

                return Result<UserDTO>.SuccessResult(userDto);
            }
        }
    }
}
