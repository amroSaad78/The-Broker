using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Services
{
    public class PagesNames : IPagesNames
    {
        private readonly Dictionary<string, string> Property;
        private readonly Dictionary<string, string> Customers;
        private readonly Dictionary<string, string> Payments;
        
        public static string PaymentsAlert { get; } = "Payments Alert";
        public static string PropertyAlert { get; } = "Property Alert";
        public static string Maintenance { get; } = "Maintenance";
        public static string Electricity { get; } = "Electricity";
        public static string Water { get; } = "Water";
        public static string Sanitation { get; } = "Sanitation";
        public static string ExecutiveExpenses { get; } = "Executive Expenses";
        public static string Transportation { get; } = "Transportation";
        public static string Other { get; } = "Other";
        public static string SoldedProperty { get; } = "Solded Property";
        public static string RentedProperty { get; } = "Rented Property";
        public static string EmptyProperty { get; } = "Empty Property";
        public static string ReceivedFunds { get; } = "Received Funds";
        public static string RefusedCheques { get; } = "Refused Cheques";

        public PagesNames()
        {
            Property = new Dictionary<string, string>();
            Customers = new Dictionary<string, string>();
            Payments = new Dictionary<string, string>();
            AddProperty();
            AddCustomers();
            AddPayment();
        }

        private void AddCustomers()
        {
            Customers.Add("NewCustomer", "New Customer");
            Customers.Add("AllCustomers", "All Customers");
            Customers.Add("CustomerProperty", "Customer Property");
            Customers.Add("CustomerPayments", "Customer Payments");
            Customers.Add("CustomerStatement", "Customer Statement");
        }
        private void AddProperty()
        {
            Property.Add("Apartment", "Apartment");
            Property.Add("Villa", "Villa");
            Property.Add("Townhouse", "Townhouse");
            Property.Add("Penthouse", "Penthouse");
            Property.Add("Chalet", "Chalet");
            Property.Add("TwinHouse", "Twin House");
            Property.Add("Duplex", "Duplex");
            Property.Add("FullFloor", "Full Floor");
            Property.Add("HalfFloor", "Half Floor");
            Property.Add("WholeBuilding", "Whole Building");
            Property.Add("Land", "Land");
            Property.Add("BulkSale", "Bulk Sale");
            Property.Add("Bungalows", "Bungalows");
            Property.Add("HotelApartments", "Hotel Apartments");
            Property.Add("IVilla", "I Villa");
        }
        private void AddPayment()
        {
            Payments.Add("Cash", "Cash");
            Payments.Add("Cheque", "Cheque");
        }   
        public Dictionary<string, string> GetProperty() => Property;
        public IEnumerable<string> GetPropertyVal() => GetProperty().Select(val => val.Value);
        public Dictionary<string, string> GetCustomers() => Customers;
        public IEnumerable<string> GetCustomersVal() => GetCustomers().Select(val => val.Value);
        public Dictionary<string, string> GetPayments() => Payments;
        public IEnumerable<string> GetPaymentsVal() => GetPayments().Select(val => val.Value);
    }
}
