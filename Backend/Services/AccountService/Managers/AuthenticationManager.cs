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
        private readonly JwtTokenConfig jwtTokenConfig;
        private readonly byte[] secret;
        private readonly IMongoCollection<Account> accounts;
        private readonly IMongoCollection<Authentication> accountCredentials;

        public AuthenticationManager(IAccountDatabaseSettings settings, JwtTokenConfig jwtTokenConfig)
        {
            //token configuration
            this.jwtTokenConfig = jwtTokenConfig;
            this.secret = Encoding.ASCII.GetBytes(jwtTokenConfig.Secret);

            //mongodb configuration
            MongoClient client = new MongoClient(settings.ConnectionString);
            IMongoDatabase database = client.GetDatabase(settings.DatabaseName);
            accounts = database.GetCollection<Account>(settings.AccountsCollectionName);
            accountCredentials = database.GetCollection<Authentication>(settings.AuthenticationCollectionName);

        }

        /// <summary>
        /// Authenticates using credentials
        /// </summary>
        /// <param name="authentication"></param>
        /// <returns>AuthenticateResponse</returns>
        public async Task<AuthenticateResponse> Authenticate(Authentication authentication)
        {
            try
            {
                Authentication auth = await accountCredentials.Find(x => x.username.Equals(authentication.username)).SingleOrDefaultAsync();

                if (auth == null)
                {
                    return null;
                }

                bool validPassword = BCrypt.Net.BCrypt.Verify(authentication.password, auth.password);
                if (!validPassword)
                {
                    return null;
                }

                Account user = await accounts.Find(x => x.accountID.Equals(auth.accountID)).SingleOrDefaultAsync();

                // return null if user not found
                if (user == null)
                {
                    return null;
                }
                
                //add accountId and role claim
                Claim[] claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier,user.accountID),
                    new Claim(ClaimTypes.Role, user.role)
                };

                // authentication successful so generate jwt token
                string token = generateJwtToken(claims, DateTime.Now);

                return new AuthenticateResponse(user, token);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Generates JWT with role and expiration date
        /// </summary>
        /// <param name="user"></param>
        /// <param name="claims"></param>
        /// <param name="currentDateTime"></param>
        /// <returns>string token</returns>
        private string generateJwtToken(Claim[] claims, DateTime currentDateTime)
        {
            JwtSecurityToken jwtToken = new JwtSecurityToken(
                jwtTokenConfig.Issuer,
                jwtTokenConfig.Audience,
                claims,
                expires: currentDateTime.AddDays(jwtTokenConfig.AccessTokenExpiration),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            );

            string accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return accessToken;
        }
    }
}
