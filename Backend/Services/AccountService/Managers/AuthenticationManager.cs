using AccountService.Helpers;
using AccountService.Interfaces;
using AccountService.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AccountService.Managers
{
    public class AuthenticationManager: IAuthenticationManager
    {
        private readonly AppSettings appSettings;

        private List<Authentication> accounts = new List<Authentication>
        {
            new Authentication { accountID = "test", username = "test", password = "test" }
        };

        private List<Account> users = new List<Account>
        {
            new Account { accountID = "test", firstName = "test", lastName = "test", email = "test" }
        };

        public AuthenticationManager(IOptions<AppSettings> appSettings)
        {
            this.appSettings = appSettings.Value;
        }

        public IEnumerable<Account> GetAll()
        {
            throw new NotImplementedException();
        }

        public Account GetById(int id)
        {
            throw new NotImplementedException();
        }

        AuthenticateResponse IAuthenticationManager.Authenticate(Authentication authenticationModel)
        {
            var account = accounts.SingleOrDefault(x => x.username == authenticationModel.username && x.password == authenticationModel.password);
            var user = users.SingleOrDefault(x => x.accountID == account.accountID);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(account);

            return new AuthenticateResponse(user, token);
        }
        private string generateJwtToken(Authentication user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.accountID) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public Account GetUserByToken(string accountID)
        {
            var user = users.Find(x=> x.accountID.Equals(accountID));
            return user;
        }
    }
}
