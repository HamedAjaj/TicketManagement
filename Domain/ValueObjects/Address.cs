namespace TicketManagement.Domain.ValueObjects
{
    public class Address
    {
        public string Governorate { get; private set; }
        public string City { get; private set; }
        public string District { get; private set; }

        protected Address() { }

        public Address(string governorate, string city, string district)
        {
            if (string.IsNullOrWhiteSpace(governorate) || string.IsNullOrWhiteSpace(city) || string.IsNullOrWhiteSpace(district))
                throw new ArgumentException("Address fields cannot be empty.");

            Governorate = governorate;
            City = city;
            District = district;
        }
    }

}

