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
            _customerRepository.Insert(customer);
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
                throw new DuplicateEmailException(email);
            }
        }
    }
}
