using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using DnsClient;
using Ecommerce.Domain.Core.Interfaces.Repositories;
using Ecommerce.Domain.Core.Services;
using Ecommerce.Domain.Model;

namespace Ecommerce.Domain.Services
{
    public class CustomerServices : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerServices(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public void SaveCustomer(Customer customer)
        {
            ValidateEmail(customer.Email);
            _customerRepository.Add(customer);
        }

        private bool IsEmailValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var emailAddress = new MailAddress(email.Trim()); 


                if (emailAddress.Address == email.Trim())
                {

                    string domain = emailAddress.Host;
                    return CheckDomain(domain);
                }
            }
            catch (FormatException)
            {
                return false;
            }

            return false;
        }

        private bool CheckDomain(string domain)
        {
            try
            {
                var lookup = new LookupClient();
                var result = lookup.Query(domain, QueryType.MX); 
                return result.Answers.MxRecords().Any();
            }
            catch (Exception)
            {

                return false;
            }
        }

        private void ValidateEmail(string email)
        {
            if (!IsEmailValid(email))
            {
                throw new ValidationException("Invalid email address.");
            }
        }
    }
}
