using FamilyManagement.Model.Members;
using System;
using System.Collections.Generic;

namespace FamilyManagement.Model
{
    public class Family
    {
        public Family(Guid id,int businessId, string email,string externalId)
        {
            Id = id;
            BusinessId = businessId;
            Email = email;
            ExternalId = externalId;
        }
        public Guid Id { get; set; }
        public int BusinessId { get; set; }
        public string Email { get; set; }
        public string ExternalId { get; set; }
        public string? InsuranceId { get; set; }
        public Insurance? Insurance { get; set; }
        public int? ContactInfoId { get; set; }
        public ContactInfo? ContactInfo { get; set; }
        public List<Parent> Parents { get; set; } = new List<Parent>();
        public List<Student> Students { get; set; } = new List<Student>();
    }
}
