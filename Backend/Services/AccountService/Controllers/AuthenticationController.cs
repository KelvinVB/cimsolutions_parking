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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Authenticate([FromBody] Authentication request)
        {
            try
            {
                AuthenticateResponse response = await authenticationManager.Authenticate(request);

                if (response == null)
                {
                    return NotFound(new { message = "Username or password is incorrect" });
                }
                
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
    }
}