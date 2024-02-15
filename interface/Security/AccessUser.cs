using Domain.Interfaces.Security;
using Domain.Models;
using Infrastructure.AppDbContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Security
{
    public class AccessUser : IAccessUser
    {
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private readonly MovieContext _context;
        public AccessUser(IHttpContextAccessor HttpContextAccessor, MovieContext context)
        {
            _HttpContextAccessor = HttpContextAccessor;
            _context = context;
        }
        public string GetUserId()
        {

            var userIdString = _HttpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdString))
            {

                throw new InvalidOperationException("User is not authenticated");
            }

            return userIdString;
        }
        public string GetUsername()
        {
            var username = _HttpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            if (username != null)
                return username;


            throw new InvalidOperationException("Username not found in claims");
        }
        
        public async Task<ApplicationUser> GetUser()
        {
            var userIdString = _HttpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdString))
            {

                throw new InvalidOperationException("User is not authenticated");
            }
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == userIdString);
        }
    }
}
