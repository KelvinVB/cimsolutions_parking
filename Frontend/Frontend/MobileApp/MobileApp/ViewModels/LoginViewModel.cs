using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MobileApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private Authentication auth { get; set; }
        public LoginViewModel()
        {
            auth = new Authentication();
        }

        /// <summary>
        /// User login
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns>bool</returns>
        public async Task<bool> Login(Authentication credentials)
        {
            try
            {
                Account account = await authenticationService.Login(credentials);

                //set user and token in sercure storage
                if (account.username != null)
                {
                    await SecureStorage.SetAsync("username", account.username);
                }
                await SecureStorage.SetAsync("token", account.token);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
