using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Security
{
    public interface IAccessUser
    {
        public string GetUserId();
        string GetUsername();
        Task<ApplicationUser> GetUser();
    }
}
