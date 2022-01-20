using AutoMapper;
using Mc2.CrudTest.AcceptanceTests.MoqData;
using Mc2.CrudTest.Presentation.Server.Common.Mappers;
using Mc2.CrudTest.Presentation.Server.CommonDtos;
using Mc2.CrudTest.Presentation.Server.Controllers;
using Mc2.CrudTest.Presentation.Server.Core.Domain.Customers.Models;
using Mc2.CrudTest.Presentation.Server.Core.Services.CustomerServices;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Mc2.CrudTest.AcceptanceTests.Api
{
    public class CustomerApiControllerTest
    {
        ApiMoqData moqData;
        public CustomerApiControllerTest()
        {
            moqData = new ApiMoqData();
        }

        [Fact]
        public void GetTest()
        {
            //ApiMoqData moqData = new ApiMoqData();

            //Arrange
            var moq = new Mock<ICustomerService>(MockBehavior.Strict);
            moq.Setup(p => p.GetCustomersAsync().Result).Returns(moqData.GetAll());
            //moq.SetupAllProperties();

            CustomerController customer = new CustomerController(moq.Object,null);

            //Act
            var result = customer.Get().Result;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var list = result as OkObjectResult;
            Assert.IsType<BaseDto<List<CustomerInfoDto>>>(list.Value);
        }

        [Theory]
        [InlineData("{37405A8E-B38A-410D-87A6-952ACB22EE39}")]
        [InlineData("{6FEF43A1-3AA2-450D-930D-9B491FFABDF7}")]
        public void GetByIdTest(Guid id)
        {

            //Arrange
            var moq = new Mock<ICustomerService>(MockBehavior.Strict);
            moq.Setup(p=> p.GetCustomerByIdAsync(id).Result).Returns(moqData.GetById(id));

            CustomerController customer = new CustomerController(moq.Object,null);            

            //Act
            var result = customer.Get(id).Result;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var list = result as OkObjectResult;
            Assert.IsType<BaseDto<CustomerInfoDto>>(list.Value);
        }

        [Fact]
        public void PostTest()
        {
            //ApiMoqData moqData = new ApiMoqData();

            //Arrange
            var moq = new Mock<ICustomerService>();
            var moqmp = new Mock<IMapper>();

            var cmoqCustomer = moqData.GetAll().Data.FirstOrDefault(p => p.Id == Guid.Parse("{37405A8E-B38A-410D-87A6-952ACB22EE39}"));
            CreatCustomerDto customer = new CreatCustomerDto()
            {
                Firstname = cmoqCustomer.Firstname,
                Lastname = cmoqCustomer.Lastname,
                Email = cmoqCustomer.Email,
                DateOfBirth = cmoqCustomer.DateOfBirth,
                PhoneNumber = cmoqCustomer.PhoneNumber,
                BankAccountNumber = cmoqCustomer.BankAccountNumber
            };
   

            //moqmp.Setup(m => m.Map<CustomerInfo, CreatCustomerDto>(It.IsAny<CustomerInfo>())).Returns(customer);

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new CustomerProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
   

            CustomerController customerApi = new CustomerController(moq.Object, mapper);
                      

            //Act
            var result = customerApi.Post(customer).Result;

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);

            //Arrange
          /* CustomerInfo customerInf = new CustomerInfo()
            {
                Firstname = cmoqCustomer.Firstname,
                Lastname = cmoqCustomer.Lastname,
                Email = cmoqCustomer.Email,
                DateOfBirth = cmoqCustomer.DateOfBirth,
                PhoneNumber = cmoqCustomer.PhoneNumber,
                BankAccountNumber = cmoqCustomer.BankAccountNumber
            };

            var createdReturnData = moqData.GetCreatedResult(customerInf);
            moq.Setup(p => p.AddCustomerAsync(customerInf).Result).Returns(createdReturnData);
            customerApi = new CustomerController(moq.Object, mapper);

            //Act
            var createdResult = customerApi.Post(customer).Result;

            //Assert
            Assert.IsType<CreatedAtActionResult>(createdResult);*/

        }

        [Theory]
        [InlineData("{37405A8E-B38A-410D-87A6-952ACB22EE39}", "{CFEEBEE1-6D9B-4002-BE91-52D4EF9F7444}")]
        public void Delete_Test(Guid ValidId, Guid inValidId)
        {
            
            //Arrange
            var moq = new Mock<ICustomerService>();

            moq.Setup(p => p.DeleteCustomerAsync(ValidId).Result).Returns(moqData.PublicResult());
            moq.Setup(p => p.DeleteCustomerAsync(inValidId).Result).Returns(moqData.NotFoundResult());


            CustomerController customerApi = new CustomerController(moq.Object, null);

            //Act
            var result = customerApi.Delete(ValidId).Result;
            var invalidResult = customerApi.Delete(inValidId).Result;
            
            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<NotFoundObjectResult>(invalidResult);
            
        }

    }
}
