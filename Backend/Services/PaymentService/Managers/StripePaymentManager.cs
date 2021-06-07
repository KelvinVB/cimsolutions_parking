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

        public async Task<dynamic> PayByCreditCard(string id, string email, string firstName, string lastName, string cardnumber, int month, int year, string cvc, int value)
        {
            try
            {
                StripeConfiguration.ApiKey = "sk_test_51Iyd0JCW5oBVi3aeyirlYffw09mn2TFbGyt10imL1VdyHYJq46wYgBs4fF6xMLhZBhGqkAwfQrJ9PpQ6qxT8XBZT000gUFLyzy";

                Customer customer = await GetCustomer(id);

                if (customer == null)
                {
                    customer = await PostCustomer(id, email, firstName, lastName);
                }

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
                    Source = token.Id,
                    Customer
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

        public async Task<dynamic> GetCustomer(string id)
        {
            try
            {
                var service = new CustomerService();
                Dictionary<string, string> user = new Dictionary<string, string>
                {
                    { "UserId", id },
                };

                StripeList<Customer> customers = service.List( user );

                if (customers.size() > 0)
                {
                    Customer customer = customers.get(0);
                    Customer customer = await service.GetAsync(customer.Metadata({ "UserId", id});
                }
                return customer;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<dynamic> PostCustomer(string id, string email, string firstName, string lastName)
        {
            try
            {
                StripeConfiguration.ApiKey = "sk_test_51Iyd0JCW5oBVi3aeyirlYffw09mn2TFbGyt10imL1VdyHYJq46wYgBs4fF6xMLhZBhGqkAwfQrJ9PpQ6qxT8XBZT000gUFLyzy";

                var options = new CustomerCreateOptions
                {
                    Email = email,
                    Name = firstName + " " + lastName,
                    PaymentMethod = "pm_1FWS6ZClCIKljWvsVCvkdyWg",
                    Metadata = new Dictionary<string, string>
                    {
                        { "UserId", id },
                    }
                };
                var service = new CustomerService();
                Customer customer = await service.CreateAsync(options);

                return customer;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
