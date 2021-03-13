using ProjectManagement.Core.UseCases.Customers.Dto;
using System.Collections.Generic;

namespace ProjectManagement.Core.UseCases.Customers.ViewModels
{
    public class CustomerVm
    {
        public IList<CustomerDto> Customers { get; set; }
        public int Count { get; set; }
    }
}
