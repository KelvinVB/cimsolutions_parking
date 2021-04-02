using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AccountService.Interfaces;
using AccountService.Managers;
using AccountService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        IAccountManager accountManager;

        public AccountController(IAccountManager accountManager)
        {
            this.accountManager = accountManager;
        }

        /// <summary>
        /// retrieves account from database with token
        /// </summary>
        /// <returns>account</returns>
        [HttpGet("get")]
        [Authorize(Roles = "user")]
        public IActionResult GetAccount()
        {
            string accountID = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Account account = accountManager.GetAccount(accountID);

            if (account == null)
                return BadRequest(new { message = "Can't receive account" });

            return Ok(account);
        }

        /// <summary>
        /// Creates a new account
        /// </summary>
        /// <param name="request"></param>
        /// <returns>account</returns>
        [HttpPost("create")]
        public IActionResult CreateAccount([FromBody] Account request)
        {
            Account account = accountManager.CreateAccount(request);

            if(account == null)
            {
                return BadRequest("Could not create a new account.");
            }

            return Ok(account);
        }
    }
}