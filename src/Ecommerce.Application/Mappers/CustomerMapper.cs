namespace Ecommerce.Application.Mappers;

public class CustomerMapper : IMapper<Customer, CustomerDto>, IMapper<CustomerDto, Customer>
{
    public Customer Map(CustomerDto source)
    {
        return new Customer
        {
            Name = source.Name,
            LastName = source.LastName,
            Email = source.Email,
            BirthDate = source.BirthDate,
            Address = new Address 
            {
                Street = source.AddressDto.Street,
                Number = source.AddressDto.Number,
                Complement = source.AddressDto.Complement,
                Neighborhood = source.AddressDto.Neighborhood,
                City = source.AddressDto.City,
                State = source.AddressDto.State,
                Country = source.AddressDto.Country,
                PostalCode = source.AddressDto.PostalCode
            },
            Cpf = source.Cpf
        };
    }

    public CustomerDto Map(Customer source)
    {
        return new CustomerDto
        {
            Name = source.Name,
            LastName = source.LastName,
            Email = source.Email,
            BirthDate = source.BirthDate,
            AddressDto = new AddressDto
            {
                Street = source.Address.Street,
                Number = source.Address.Number,
                Complement = source.Address.Complement,
                Neighborhood = source.Address.Neighborhood,
                City = source.Address.City,
                State = source.Address.State,
                Country = source.Address.Country,
                PostalCode = source.Address.PostalCode
            },
            Cpf = source.Cpf
        };
    }

    public IEnumerable<CustomerDto> Map(IEnumerable<Customer> source)
    {
        foreach (var item in source)
        {
            yield return Map(item);
        }
    }

    public IEnumerable<Customer> Map(IEnumerable<CustomerDto> source)
    {
        foreach (var item in source)
        {
            yield return Map(item);
        }
    }
}
