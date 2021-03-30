using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountService.Interfaces;
using AccountService.Managers;
using AccountService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        IAccountManager accountManager;

        public IActionResult GetAccountByToken()
        {
            Account account = accountManager.GetAccountByToken();

            if (account == null)
                return BadRequest(new { message = "Can't receive account" });

            return Ok(account);
        }

        public Task<IActionResult> Post([FromBody] Account acount)
        {
            return accountManager.Post(acount);
        }
    }
}