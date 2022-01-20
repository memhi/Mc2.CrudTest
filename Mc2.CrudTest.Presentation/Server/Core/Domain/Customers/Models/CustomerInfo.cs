using Mc2.CrudTest.Presentation.Server.Core.Domain.CommonModels;
using System;

namespace Mc2.CrudTest.Presentation.Server.Core.Domain.Customers.Models
{
    public class CustomerInfo : BaseModel<Guid>
    { 
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string? BankAccountNumber { get; set; }

    }
}
