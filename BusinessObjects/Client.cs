
using System;
namespace BusinessObjects
{
    public class Client
    {
        public Client()
        {

        }

        public Client(int clientId, string clientName)
        {
            this.ClientId = clientId;
            this.ClientName = clientName;
        }

        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string ContactName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string TIN { get; set; }
        public string PrivateClientDetails { get; set; }
        public string OtherClientDetails { get; set; }
        public string BillingAddress { get; set; }
        public string City { get; set; }
        public string StateCode { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public bool ShipToDifferentAddress { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingStateCode { get; set; }
        public string ShippingZip { get; set; }
        public string ShippingCountry { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
