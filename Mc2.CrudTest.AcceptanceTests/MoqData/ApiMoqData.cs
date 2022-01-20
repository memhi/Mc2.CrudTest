using Mc2.CrudTest.Presentation.Server.CommonDtos;
using Mc2.CrudTest.Presentation.Server.Core.Domain.Customers.Models;
using Mc2.CrudTest.Presentation.Server.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.AcceptanceTests.MoqData
{
    public class ApiMoqData
    {
        public ResultSet<List<CustomerInfo>> GetAll()
        {
           
            var result = new ResultSet<List<CustomerInfo>>
            { 
              Message = "",
              IsSucceed = true,
              Data = new List<CustomerInfo>()
              {
                 new CustomerInfo { Id = Guid.NewGuid() ,  Firstname ="f1" , Lastname="l1" , Email = "fl1@gmail.com",PhoneNumber="123",BankAccountNumber="1"},
                 new CustomerInfo { Id = Guid.Parse("{6FEF43A1-3AA2-450D-930D-9B491FFABDF7}") ,  Firstname ="f2" , Lastname="l2" , Email = "fl2@gmail.com",PhoneNumber="123",BankAccountNumber="2"},
                 new CustomerInfo { Id = Guid.NewGuid() ,  Firstname ="f3" , Lastname="l3" , Email = "fl3@gmail.com",PhoneNumber="123",BankAccountNumber="3"},
                 new CustomerInfo { Id = Guid.Parse("{37405A8E-B38A-410D-87A6-952ACB22EE39}") ,  Firstname ="f4" , Lastname="l4" , Email = "fl4@gmail.com",PhoneNumber="123",BankAccountNumber="4"},
              }
            };
            
            return result;
        }

        public ResultSet<CustomerInfo> GetById(Guid id)
        {
            var allCustomers =GetAll();
            var customer = allCustomers.Data.FirstOrDefault(p=>p.Id==id);



            var result = new ResultSet<CustomerInfo>
            {
                Message = "",
                IsSucceed = true,
                Data = customer
            };

            return result;
        }

        public ResultSet<CustomerInfo> GetCreatedResult(CustomerInfo customer)
        {
            var result = new ResultSet<CustomerInfo>
            {
                Message = "",
                IsSucceed = true,
                Data = customer
            };

            return result;
        }

        public ResultSet PublicResult()
        {
            var result = new ResultSet
            {
                Message = "",
                IsSucceed = true,

            };

            return result;
        }

        public ResultSet NotFoundResult()
        {
            var result = new ResultSet
            {
                Message = "Not Found..!",
                IsSucceed = false,

            };

            return result;
        }




    }
}
