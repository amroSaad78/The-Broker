using System.Collections.Generic;

namespace WebMVC.Services
{
    public interface IPagesNames
    {
        Dictionary<string, string> GetProperty();
        IEnumerable<string> GetPropertyVal();
        Dictionary<string, string> GetCustomers();
        IEnumerable<string> GetCustomersVal();
        Dictionary<string, string> GetPayments();
        IEnumerable<string> GetPaymentsVal();
    }
}