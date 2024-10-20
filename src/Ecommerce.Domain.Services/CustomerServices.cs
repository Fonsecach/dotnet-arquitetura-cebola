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
            customer.IsActive = true;
            customer.CreatedAt = DateTime.Now;
            customer.Address.IsActive = true;
            customer.Address.CreatedAt = DateTime.Now;
            _customerRepository.Insert(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            _customerRepository.Update(customer);
        }

        public void DeleteCustomer(string id)
        {
            _customerRepository.Delete(id);
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _customerRepository.Get();
        }

        public Customer GetCustomerById(string id)
        {
            return _customerRepository.Get(id);
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
