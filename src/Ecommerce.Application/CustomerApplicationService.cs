

using Ecommerce.Application.Interfaces;

namespace Ecommerce.Application
{
    public class CustomerApplicationService : ICustomerApplicationService
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper<CustomerDto, Customer> _mapperEntity;
        private readonly IMapper<Customer, CustomerDto> _mapperDto;

        public CustomerApplicationService(ICustomerService customerService,
                                          IMapper<CustomerDto, Customer> mapperEntity,
                                          IMapper<Customer, CustomerDto> mapperDto)
        {
            _customerService = customerService;
            _mapperEntity = mapperEntity;
            _mapperDto = mapperDto;
        }

        public void DeleteCustomer(string id)
        {
            _customerService.DeleteCustomer(id);
        }

        public CustomerDto GetCustomerById(string id)
        {
            var customer = _customerService.GetCustomerById(id);
            var customerDto = _mapperDto.Map(customer);
            return customerDto;
        }

        public IEnumerable<CustomerDto> GetCustomers()
        {
            var customers = _customerService.GetCustomers();
            var customersDto = _mapperDto.Map(customers);
            return customersDto;
        }

        public void SaveCustomer(CustomerDto customerDto)
        {
            var customer = _mapperEntity.Map(customerDto);
            _customerService.SaveCustomer(customer);
        }

        public void UpdateCustomer(CustomerDto customerDto)
        {
            var customer = _mapperEntity.Map(customerDto);
            _customerService.UpdateCustomer(customer);
        }
    }
}