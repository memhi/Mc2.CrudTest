using Mc2.CrudTest.Presentation.Server.Core.Domain.Customers.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Server.Core.Services.CustomerServices
{
    public interface ICustomerService
    {
        Task<ResultSet<List<CustomerInfo>>> GetCustomersAsync();
        Task<ResultSet<CustomerInfo>> GetCustomerByIdAsync(Guid customerId);
        Task<ResultSet<CustomerInfo>> AddCustomerAsync(CustomerInfo customer);
        Task<ResultSet> EditCustomerAsync(CustomerInfo customer);
        Task<ResultSet> DeleteCustomerAsync(Guid customerId);

    }
}
