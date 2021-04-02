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
using MongoDB.Driver;

namespace AccountService.Managers
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly AppSettings settings;
        private readonly JwtTokenConfig jwtTokenConfig;
        private readonly byte[] secret;
        private readonly IMongoCollection<Account> accounts;
        private readonly IMongoCollection<Authentication> accountCredentials;

        public AuthenticationManager(IAccountDatabaseSettings settings, JwtTokenConfig jwtTokenConfig)
        {
            this.jwtTokenConfig = jwtTokenConfig;
            this.secret = Encoding.ASCII.GetBytes(jwtTokenConfig.Secret);
            MongoClient client = new MongoClient(settings.ConnectionString);
            IMongoDatabase database = client.GetDatabase(settings.DatabaseName);
            accounts = database.GetCollection<Account>(settings.AccountsCollectionName);
            accountCredentials = database.GetCollection<Authentication>(settings.AuthenticationCollectionName);

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
            Authentication auth = accountCredentials.Find(x => x.username == request.username && x.password == request.password).SingleOrDefault();
            Account user = accounts.Find(x => x.accountID == auth.accountID).SingleOrDefault();

            // return null if user not found
            if (user == null) return null;

            //add accountId and role claim
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier,user.accountID),
            new Claim(ClaimTypes.Role, user.role)
            };

            // authentication successful so generate jwt token
            var token = generateJwtToken(auth, claims, DateTime.Now);

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
            Account user = accounts.Find(x => x.accountID.Equals(accountID)).SingleOrDefault();
            return user;
        }
    }
}
