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
        public IActionResult Authenticate([FromBody] Authentication request)
        {
            var response = authenticationManager.Authenticate(request);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpGet("token")]
        [Authorize(Roles ="user")]
        public IActionResult GetAccount()
        {
            var accountID = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if(accountID == null)
            {
                return BadRequest();
            }
            var user = authenticationManager.GetAccount(accountID);
            return Ok(user);
        }
    }
}