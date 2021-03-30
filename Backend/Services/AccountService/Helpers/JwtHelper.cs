using AccountService.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Helpers
{
    public class JwtHelper
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtHelper(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, IAuthenticationManager authenticationManager)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                attachUserToContext(context, authenticationManager, token);

            await _next(context);
        }

        private void attachUserToContext(HttpContext context, IAuthenticationManager authenticationManager, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var accountID = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                //jwt validation succeeded
                context.Items["Account"] = authenticationManager.GetById(accountID);
            }
            catch
            {
                //jwt validation fails
            }
        }
    }
}