using Mc2.CrudTest.Presentation.Server.Core.Domain.Customers.Models;
using Mc2.CrudTest.Presentation.Server.Core.Domain.Customers.RepoInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Server.Core.Services.CustomerServices
{
    public class CustomerService : ICustomerService
    {
        private ICustomerRepo _CustomerRepository;

        public CustomerService(ICustomerRepo customerRepo)
        {
            this._CustomerRepository = customerRepo;
        }
        public async Task<ResultSet<CustomerInfo>> AddCustomerAsync(CustomerInfo customer)
        {
            var customers = await _CustomerRepository.GetCustomersByEmailAsync(customer.Email);

            if (customers != null)
            {
                return new ResultSet<CustomerInfo>()
                {
                    IsSucceed = false,
                    Message = "Customer With Email Is Exist..!",
                    Data = null
                };

            }

            _CustomerRepository.AddCustomer(customer);

            try { await _CustomerRepository.SaveAsync(); }

            catch (Exception e) { return new ResultSet<CustomerInfo>() { IsSucceed = false, Message = e.Message }; }

            return new ResultSet<CustomerInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = customer
            };
        }

        public async Task<ResultSet> DeleteCustomerAsync(Guid customerId)
        {
            if(!GetCustomerByIdAsync(customerId).Result.IsSucceed)
                return new ResultSet() { IsSucceed = false, Message = "Customer Not Found" };
            if (! await _CustomerRepository.DeleteCustomer(customerId))
                return new ResultSet() { IsSucceed = false, Message = "Customer Not Deleted" };

            try
            {
                await _CustomerRepository.SaveAsync();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "Customer Not Deleted" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet> EditCustomerAsync(CustomerInfo customer)
        {
            var result = await _CustomerRepository.GetCustomersByEmailAsync(customer.Email);

            if (result != null && result.Id != customer.Id)
            {

                return new ResultSet<CustomerInfo>()
                {
                    IsSucceed = false,
                    Message = "Email Is In Used With Anouther Customer...!",
                    Data = null
                };

            }


            if (! await _CustomerRepository.EditCustomer(customer))
                return new ResultSet() { IsSucceed = false, Message = "Customer Not Edited" };

            try
            {
                await _CustomerRepository.SaveAsync();
            }
            catch (Exception e)
            {
                return new ResultSet() { IsSucceed = false, Message = e.Message };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet<CustomerInfo>> GetCustomerByIdAsync(Guid customerId)
        {
            CustomerInfo Customer = await _CustomerRepository.GetCustomerByIdAsync(customerId);

            if (Customer == null)
                return new ResultSet<CustomerInfo>()
                {
                    IsSucceed = false,
                    Message = "Customer Not Found",
                    Data = null
                };

            return new ResultSet<CustomerInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = Customer
            };
        }
    

        public async Task<ResultSet<List<CustomerInfo>>> GetCustomersAsync()
        {
            var result = await _CustomerRepository.GetCustomersAsync();
            return new ResultSet<List<CustomerInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = result.ToList()
            };
        }
    }
}
