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
        public async Task<IActionResult> GetAccountAsync()
        {
            string accountID = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Account account = await accountManager.GetAccount(accountID);

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
        public async Task<IActionResult> CreateAccountAsync([FromBody] Account request)
        {
            Account account = await accountManager.CreateAccount(request);

            if(account == null)
            {
                return BadRequest("Could not create a new account.");
            }

            return Ok(account);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateAccountAsync(Account request)
        {
            string accountID = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            request.accountID = accountID;
            Account account = await accountManager.UpdateAccount(request);

            if (account == null)
            {
                return BadRequest("Could not create a new account.");
            }

            return Ok(account);
        }
    }
}