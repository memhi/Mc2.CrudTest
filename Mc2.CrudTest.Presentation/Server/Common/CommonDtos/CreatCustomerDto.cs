using System;
using System.ComponentModel.DataAnnotations;

namespace Mc2.CrudTest.Presentation.Server.CommonDtos
{
    public class CreatCustomerDto
    {
        [Required(ErrorMessage = "Enter FirstName..!")]
        [MaxLength(50, ErrorMessage = "First name should not exceed 50 characters")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Enter LastName..!")]
        [MaxLength(50, ErrorMessage = "LastName should not exceed 50 characters")]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "Enter DateOfBirth..!")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Enter PhoneNumber..!")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Enter Email..!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string? BankAccountNumber { get; set; }
    }
}
