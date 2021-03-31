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
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly AppSettings appSettings;
        private readonly JwtTokenConfig jwtTokenConfig;
        private readonly byte[] secret;

        private List<Authentication> accounts = new List<Authentication>
        {
            new Authentication { accountID = "test", username = "test", password = "test" }
        };

        private List<Account> users = new List<Account>
        {
            new Account { accountID = "test", firstName = "test", lastName = "test", email = "test" }
        };

        public AuthenticationManager(IOptions<AppSettings> appSettings, JwtTokenConfig jwtTokenConfig)
        {
            this.appSettings = appSettings.Value;
            this.jwtTokenConfig = jwtTokenConfig;
            this.secret = Encoding.ASCII.GetBytes(jwtTokenConfig.Secret);

        }

        public IEnumerable<Account> GetAll()
        {
            throw new NotImplementedException();
        }

        public Account GetById(int id)
        {
            throw new NotImplementedException();
        }

        AuthenticateResponse IAuthenticationManager.Authenticate(Authentication request)
        {
            var account = accounts.SingleOrDefault(x => x.username == request.username && x.password == request.password);
            var user = users.SingleOrDefault(x => x.accountID == account.accountID);

            // return null if user not found
            if (user == null) return null;

            //add accountId and role claim
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier,user.accountID),
            new Claim(ClaimTypes.Role, user.role)
            };

            // authentication successful so generate jwt token
            var token = generateJwtToken(account, claims, DateTime.Now);

            return new AuthenticateResponse(user, token);
        }
        private string generateJwtToken(Authentication user, Claim[] claims, DateTime currentDateTime)
        {
            var jwtToken = new JwtSecurityToken(
            jwtTokenConfig.Issuer,
            jwtTokenConfig.Audience,
            claims,
            expires: currentDateTime.AddDays(jwtTokenConfig.AccessTokenExpiration),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return accessToken;
        }

        public Account GetAccount(string accountID)
        {
            var user = users.Find(x => x.accountID.Equals(accountID));
            return user;
        }
    }
}
