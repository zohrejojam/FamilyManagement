namespace FamilyManagement.Model
{
    public class ContactInfo
    {
        public ContactInfo()
        {

        }
        public ContactInfo(string primaryPhone,string alternatePhone)
        {
            PrimaryPhone = primaryPhone;
            AlternatePhone = alternatePhone;
        }
        public int Id { get; set; }
        public string PrimaryPhone { get; set; }
        public string AlternatePhone { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
    }
}
