using System;
using System.Collections.Generic;
using System.Data;
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
        private readonly IAccountManager accountManager;

        public AccountController(IAccountManager accountManager)
        {
            this.accountManager = accountManager;
        }

        /// <summary>
        /// retrieves account from database with token
        /// </summary>
        /// <returns>account</returns>
        [HttpGet("get")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAccountAsync()
        {
            try
            {
                string accountID = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                Account account = await accountManager.GetAccount(accountID);

                if (account == null)
                    return NotFound(new { message = "Can't receive account" });

                return Ok(account);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Creates a new account
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Account</returns>
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateAccountAsync([FromBody] Account request)
        {
            try
            {
                Account account = await accountManager.CreateAccount(request);

                if (account == null)
                {
                    return BadRequest("Could not create a new account.");
                }

                return Ok(account);
            }
            catch (DuplicateNameException)
            {
                return new ConflictResult();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Update account with token
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Account</returns>
        [HttpPut("update")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateAccountAsync(Account request)
        {
            try
            {
                string accountID = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                request.accountID = accountID;
                Account account = await accountManager.UpdateAccount(request);

                if (account == null)
                {
                    return NotFound("Could not update account.");
                }

                return Ok(account);
            }
            catch (DuplicateNameException)
            {
                return Conflict();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Remove account with token
        /// </summary>
        /// <returns>Account</returns>
        [HttpDelete("delete")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAccountAsync()
        {
            try
            {
                string accountID = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                Account account = await accountManager.DeleteAccount(accountID);

                if (account == null)
                {
                    return NotFound("Could not delete the account.");
                }

                return Ok(account);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}