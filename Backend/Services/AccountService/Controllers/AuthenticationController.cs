using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AccountService.Interfaces;
using AccountService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticationManager authenticationManager;

        public AuthenticationController(IAuthenticationManager authenticationManager)
        {
            this.authenticationManager = authenticationManager;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(Authentication authenticationModel)
        {
            var response = authenticationManager.Authenticate(authenticationModel);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpGet("token")]
        public IActionResult GetUserByToken()
        {
            var userName = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if(userName == null)
            {
                return BadRequest();
            }
            var user = authenticationManager.GetUserByToken(userName);
            return Ok(user);
        }
    }
}