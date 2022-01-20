using Mc2.CrudTest.Presentation.Front.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Front.Data.ViewServices
{
    public interface ICustomerManagementService
    {
        Task<CustomerListResultView> GetCustomersAsync();
        Task<CustomerResultView> GetCustomerById(Guid id);
        Task<ResultView> AddCustomer(CustomerCreateView customer);
        Task<ResultView> DeleteCustomer(Guid id);
    }

    public class CustomerManagementService : ICustomerManagementService
    {
        private readonly HttpClient _httpClient;
        public CustomerManagementService(HttpClient httpClient)
        {
            this._httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

        }

        public async Task<ResultView> AddCustomer(CustomerCreateView customer)
        {
            var result = new ResultView();
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"api/Customer", customer);
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    result.isSuccess = true;
                    result.message = new List<string> { "Customer Created" };
                }

            }
            catch (Exception ex)
            {
                result.isSuccess = false;
                result.message = new List<string> { ex.Message };
                return result;
            }

            return result;
        }

        public async Task<ResultView> DeleteCustomer(Guid id)
        {
            var result = new ResultView();
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Customer/{id.ToString()}");

                if (response.IsSuccessStatusCode)
                {
                    result.isSuccess = true;
                    result.message = new List<string> { "Customer Deleted" };
                }
            }
            catch (Exception ex)
            {
                result.isSuccess = false;
                result.message = new List<string> { ex.Message };
                return result;
            }
            return result;
        }

        public async Task<CustomerResultView> GetCustomerById(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<CustomerResultView>($"api/Customer/{id.ToString()}");
            return result;
        }

        public async Task<CustomerListResultView> GetCustomersAsync()
        {
            var result = new CustomerListResultView();
            try
            {
                //Https:/localhost:5001/api/Customer
                result = await _httpClient.GetFromJsonAsync<CustomerListResultView>("api/Customer");

            }
            catch (Exception ex)
            {
                result.isSuccess = false;
                result.message = new List<string> { ex.Message };
                return result;
            }

            return result;
        }
    }

    public class CustomerView
    {
        public Guid id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public object bankAccountNumber { get; set; }
    }

    public class CustomerListResultView
    {
        public List<CustomerView> data { get; set; }
        public List<string> message { get; set; }
        public bool isSuccess { get; set; }
    }

    public class CustomerResultView
    {
        public CustomerView data { get; set; }
        public List<string> message { get; set; }
        public bool isSuccess { get; set; }
    }

    public class CustomerCreateView
    {
        [Required(ErrorMessage = "Enter FirstName..!")]
        [MaxLength(50, ErrorMessage = "First name should not exceed 50 characters")]
        public string firstname { get; set; }
        [Required(ErrorMessage = "Enter LastName..!")]
        [MaxLength(50, ErrorMessage = "LastName should not exceed 50 characters")]
        public string lastname { get; set; }
        public DateTime dateOfBirth { get; set; }
        [Required(ErrorMessage = "Enter PhoneNumber..!")]
        public string phoneNumber { get; set; }
        [Required(ErrorMessage = "Enter Email..!")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Please Enter Correct Email")]
        public string email { get; set; }
        public string bankAccountNumber { get; set; }
    }

    public class ResultView
    {
        public List<string> message { get; set; }
        public bool isSuccess { get; set; }
    }


}
