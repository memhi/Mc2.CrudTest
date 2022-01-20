using Mc2.CrudTest.Presentation.Server.Core.Domain.Customers.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Server.Core.Domain.Customers.RepoInterfaces
{
    public interface ICustomerRepo
    {
        Task<IEnumerable<CustomerInfo>> GetCustomersAsync();
        Task<CustomerInfo> GetCustomersByEmailAsync(string email);
        Task<CustomerInfo> GetCustomerByIdAsync(Guid customerId);
        Task<bool> AddCustomer(CustomerInfo customer);
        Task<bool> EditCustomer(CustomerInfo customer);
        Task<bool> DeleteCustomer(Guid customerId);
               
        Task SaveAsync();
    }
}
