using AccountService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.Interfaces
{
    interface IAccountManager
    {
        Task<IActionResult> Post([FromBody]Account model);
        Task<IActionResult> GetUserByToken();
    }
}
