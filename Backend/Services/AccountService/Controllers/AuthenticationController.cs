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

        /// <summary>
        /// authenticates the user
        /// </summary>
        /// <param name="request"></param>
        /// <returns>AuthenticateResponse with token</returns>
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] Authentication request)
        {
            try
            {
                AuthenticateResponse response = authenticationManager.Authenticate(request);

                if (response == null)
                    return BadRequest(new { message = "Username or password is incorrect" });

                return Ok(response);
            }
            catch (NullReferenceException)
            {
                return Unauthorized();
            }
            catch(Exception)
            {
                return BadRequest();
            }

        }
    }
}