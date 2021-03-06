using AccountService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.Interfaces
{
    public interface IAuthenticationManager
    {
        Task<AuthenticateResponse> Authenticate(Authentication authenticationModel);
    }
}
