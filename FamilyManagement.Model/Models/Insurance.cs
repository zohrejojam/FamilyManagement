namespace FamilyManagement.Model
{
    public class Insurance
    {
        public Insurance()
        {

        }
        public Insurance(string id ,string externalId,string companyName,string policyNumber,string companyPhone)
        {
            Id = id;
            ExternalId = externalId;
            CompanyName = companyName;
            PolicyNumber = policyNumber;
            CompanyPhone = companyPhone;
        }
        public string Id { get; set; }
        public string ExternalId { get; set; }
        public string CompanyName { get; set; }
        public string PolicyNumber { get; set; }
        public string CompanyPhone { get; set; }
    }
}
