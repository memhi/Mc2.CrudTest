using AutoMapper;
using Mc2.CrudTest.Presentation.Server.CommonDtos;
using Mc2.CrudTest.Presentation.Server.Core.Domain.Customers.Models;
using Mc2.CrudTest.Presentation.Server.Core.Services.CustomerServices;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mc2.CrudTest.Presentation.Server.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerService _customerService;
        private readonly IMapper _mapper;
        public CustomerController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }
        // GET: api/<CustomerController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var serviceResult = await _customerService.GetCustomersAsync();
            var result = new BaseDto<List<CustomerInfoDto>>(IsSuccess: serviceResult.IsSucceed, Message: new List<string> { serviceResult.Message }
                , Data: serviceResult.Data.Select(p=>new CustomerInfoDto
                {
                    Id = p.Id,
                    Firstname = p.Firstname,
                    Lastname = p.Lastname,
                    Email = p.Email,
                    PhoneNumber = p.PhoneNumber
                }).ToList());
                       
            
            return Ok(result);
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var serviceResult = await _customerService.GetCustomerByIdAsync(id);

            if (!serviceResult.IsSucceed)
                return NotFound(new BaseDto(
                    IsSuccess : serviceResult.IsSucceed,
                    Message : new List<string> { serviceResult.Message}                 
                ));

            var result = new BaseDto<CustomerInfoDto>(serviceResult.IsSucceed, new List<string> { serviceResult.Message }
                ,Data: new CustomerInfoDto
                {
                    Id= serviceResult.Data.Id,
                    Firstname= serviceResult.Data.Firstname,
                    Lastname= serviceResult.Data.Lastname,
                    Email= serviceResult.Data.Email,
                    PhoneNumber= serviceResult.Data.PhoneNumber,
                    DateOfBirth= serviceResult.Data.DateOfBirth,
                    BankAccountNumber= serviceResult.Data.BankAccountNumber
                });
            return Ok(result);
          
        }

        // POST api/<CustomerController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreatCustomerDto request)
        {
            if (!ModelState.IsValid)
            {
                List<string> message = new List<string>();
                foreach (var er in ModelState.Values.SelectMany(modelstate => modelstate.Errors))
                    message.Add(er.ErrorMessage + " \n");

                return BadRequest(new BaseDto(
                    IsSuccess : false,
                    Message : message));
            }

            var customer = _mapper.Map<CustomerInfo>(request);
            var serviceResult = await _customerService.AddCustomerAsync(customer);
            if (serviceResult == null)
                return BadRequest(new BaseDto(false, new List<string> { "Request Failed" }));
            if (!serviceResult.IsSucceed)
                return BadRequest(new BaseDto(false, new List<string> { "Request Failed" }));
            var result = new BaseDto(serviceResult.IsSucceed, new List<string> { "Customer Created SuccesFully" });
            string url = Url.Action(nameof(Get), "Customer", new {id= serviceResult.Data.Id},Request.Scheme);

            return Created(url, result);
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                List<string> message = new List<string>();
                foreach (var er in ModelState.Values.SelectMany(modelstate => modelstate.Errors))
                    message.Add(er.ErrorMessage + " \n");

                return BadRequest(new BaseDto(
                    IsSuccess: false,
                    Message: message));
            }

            if ( !Guid.TryParse(id.ToString(),out id))
                return BadRequest(new BaseDto(false, new List<string> { "Input Parameter Not Valid...!" }));

            var request = await _customerService.DeleteCustomerAsync(id);

            if (!request.IsSucceed)
                return NotFound(new BaseDto(
                  IsSuccess: request.IsSucceed,
                  Message: new List<string> { request.Message }));

            return Ok(new BaseDto(
                  IsSuccess: request.IsSucceed,
                  Message: new List<string> { request.Message }));

        }
    }
}
