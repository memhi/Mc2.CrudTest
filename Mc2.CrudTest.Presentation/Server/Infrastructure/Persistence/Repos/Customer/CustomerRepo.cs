using Mc2.CrudTest.Presentation.Server.Core.Domain.Customers.Models;
using Mc2.CrudTest.Presentation.Server.Core.Domain.Customers.RepoInterfaces;
using Mc2.CrudTest.Presentation.Server.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Server.Persistence.Repos.Customer
{
    public class CustomerRepo : ICustomerRepo
    {
        private GenericRepo<CustomerInfo> _CustomerRepository;

        public CustomerRepo(ScopeDBContext ctx)
        {
            this._CustomerRepository = new GenericRepo<CustomerInfo>(ctx);
        }
        
        public async Task<IEnumerable<CustomerInfo>> GetCustomersAsync()
        {
            return await _CustomerRepository.GetAsync();
        }
        public async Task<CustomerInfo> GetCustomersByEmailAsync(string email)
        {
            var result = await _CustomerRepository.GetAsync(c => c.Email == email);
            return result.FirstOrDefault();

        }
        public async Task<CustomerInfo> GetCustomerByIdAsync(Guid customerId)
        {
            return await _CustomerRepository.GetByIdAsync(customerId);
        }

        public Task<bool> AddCustomer(CustomerInfo customer)
        {
            try
            {
                _CustomerRepository.Insert(customer);
            }
            catch
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }

        public Task<bool> EditCustomer(CustomerInfo customer)
        {
            try
            {
                _CustomerRepository.Update(customer);
            }
            catch
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }

        public async Task<bool> DeleteCustomer(Guid customerId)
        {
            var customer = await GetCustomerByIdAsync(customerId);
            if (customer == null)
                return false;
            try
            {

                customer.IsRemoved = true;
                customer.RemoveDate = DateTime.Now;
                _CustomerRepository.Update(customer);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task SaveAsync() =>
            await _CustomerRepository.SaveAsync();      

     
    }
}
