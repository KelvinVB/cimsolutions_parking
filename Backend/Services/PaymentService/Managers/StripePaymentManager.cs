using PaymentService.Interfaces;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.Managers
{
    public class StripePaymentManager : IStripePaymentManager
    {
        public string CreateToken(string cardNumber, string cardExpMonth, string cardExpYear, string cardCVC)
        {
            throw new NotImplementedException();
        }

        public async Task<dynamic> PayByCreditCard(string cardnumber, int month, int year, string cvc, int value)
        {
            try
            {
                StripeConfiguration.ApiKey = "sk_test_51Iyd0JCW5oBVi3aeyirlYffw09mn2TFbGyt10imL1VdyHYJq46wYgBs4fF6xMLhZBhGqkAwfQrJ9PpQ6qxT8XBZT000gUFLyzy";

                TokenCreateOptions optionstoken = new TokenCreateOptions
                {
                    Card = new TokenCardOptions
                    {
                        Number = cardnumber,
                        ExpMonth = month,
                        ExpYear = year,
                        Cvc = cvc
                    }
                };

                TokenService tokenService = new TokenService();
                Token token = await tokenService.CreateAsync(optionstoken);

                ChargeCreateOptions options = new ChargeCreateOptions
                {
                    Amount = value,
                    Currency = "eur",
                    Description = "testing stripe payment",
                    Source = token.Id
                };

                ChargeService service = new ChargeService();
                Charge charge = await service.CreateAsync(options);

                return charge.Paid;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
