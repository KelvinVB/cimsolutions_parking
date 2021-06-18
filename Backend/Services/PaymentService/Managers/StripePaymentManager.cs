using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PaymentService.Context;
using PaymentService.Helpers;
using PaymentService.Interfaces;
using PaymentService.Models;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.Managers
{
    public class StripePaymentManager : IStripePaymentManager
    {
        private PaymentContext context;
        private string key = "sk_test_51Iyd0JCW5oBVi3aeyirlYffw09mn2TFbGyt10imL1VdyHYJq46wYgBs4fF6xMLhZBhGqkAwfQrJ9PpQ6qxT8XBZT000gUFLyzy";

        public void SetContext(PaymentContext context, IOptions<AppSettings> config)
        {
            this.context = context;
            key = config.Value.key;
        }

        /// <summary>
        /// Creates a payment. Creates new customer or adds payment to existing customer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="email"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="cardnumber"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <param name="cvc"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<dynamic> PayByCreditCard(string id, string email, string firstName, string lastName, string cardnumber, int month, int year, string cvc, int value)
        {
            try
            {
                StripeConfiguration.ApiKey = key;
                Models.Customer customer = await context.customers.Where(c => c.accountId == id).FirstOrDefaultAsync();
                Stripe.Customer paymentCustomer = new Stripe.Customer();

                //create new customer if it doesn't exists
                if (customer == null)
                {
                    paymentCustomer = await PostCustomer(id, email, firstName, lastName);
                    customer = new Models.Customer();
                    customer.accountId = id;
                    customer.customerId = paymentCustomer.Id;
                    await context.customers.AddAsync(customer);
                    context.SaveChanges();
                }

                //setup token options
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

                //setup payment options
                ChargeCreateOptions options = new ChargeCreateOptions
                {
                    Amount = value,
                    Currency = "eur",
                    Description = "Parking spot reservation. Date: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                    Source = token.Id,
                    Customer = customer.customerId
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

        /// <summary>
        /// Gets a customer with account id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<dynamic> GetCustomer(string id)
        {
            try
            {
                Models.Customer customer = await context.customers.Where(c => c.accountId == id).FirstOrDefaultAsync();

                if (customer == null)
                {
                    return null;
                }

                var service = new CustomerService();

                Stripe.Customer stripeCustomer = await service.GetAsync(customer.customerId);

                return stripeCustomer;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Adds a new customer with account id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="email"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public async Task<dynamic> PostCustomer(string id, string email, string firstName, string lastName)
        {
            try
            {
                StripeConfiguration.ApiKey = key;

                var options = new CustomerCreateOptions
                {
                    Email = email,
                    Name = firstName + " " + lastName,
                    Metadata = new Dictionary<string, string>
                    {
                        { "UserId", id },
                    }
                };
                var service = new CustomerService();
                Stripe.Customer customer = await service.CreateAsync(options);

                return customer;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// IDeal payment
        /// </summary>
        /// <param name="id"></param>
        /// <param name="email"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<dynamic> PayByIDeal(string id, string email, string firstName, string lastName, int value)
        {
            try
            {
                StripeConfiguration.ApiKey = key;

                Models.Customer customer = await context.customers.Where(c => c.accountId == id).FirstOrDefaultAsync();
                Stripe.Customer paymentCustomer = new Stripe.Customer();

                //create new customer if it doesn't exists
                if (customer == null)
                {
                    paymentCustomer = await PostCustomer(id, email, firstName, lastName);
                    customer = new Models.Customer();
                    customer.accountId = id;
                    customer.customerId = paymentCustomer.Id;
                    await context.customers.AddAsync(customer);
                    context.SaveChanges();
                }

                //setup payment options
                var options = new PaymentIntentCreateOptions
                {
                    Amount = value,
                    Currency = "eur",
                    Customer = customer.customerId,
                    Description = "Parking spot reservation. Date: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                    PaymentMethodTypes = new List<string>
                    {
                        "ideal",
                    },
                };

                var service = new PaymentIntentService();
                var intent = service.Create(options);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<dynamic> GetPayments(string id)
        {
            try
            {
                Models.Customer customer = await context.customers.Where(c => c.accountId == id).FirstOrDefaultAsync();
                if (customer == null)
                {
                    return null;
                }

                StripeConfiguration.ApiKey = key;
                var options = new PaymentIntentListOptions
                {
                    Limit = 50,
                    Customer = customer.customerId
                };
                var service = new PaymentIntentService();
                StripeList<PaymentIntent> payments = await service.ListAsync(
                  options
                );

                List<PaymentIntentInformation> pi = new List<PaymentIntentInformation>();
                foreach (var p in payments)
                {
                    pi.Add(new PaymentIntentInformation { id = p.Id, description = p.Description, value = (double)p.Amount/100 });
                }

                return pi;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
