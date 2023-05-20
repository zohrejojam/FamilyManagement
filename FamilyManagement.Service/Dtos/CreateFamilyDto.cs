using FamilyManagement.Model;
using FamilyManagement.Model.Members;
using System.Collections.Generic;

namespace FamilyManagement.Service.Dtos
{
    public class CreateFamilyDto
    {
        public int BusinessId { get; set; }
        public string Email { get; set; }
        public string ExternalId { get; set; }
        public Insurance Insurance { get; set; } 
        public ContactInfoDto ContactInfo { get; set; } 
        public List<StudentDto> Students { get; set; } = new List<StudentDto>();
        public List<ParentDto> Parents { get; set; } = new List<ParentDto>();
    }

    public class ContactInfoDto
    {
        public string PrimaryPhone { get; set; }
        public string AlternatePhone { get; set; }
        public AddressDto Address { get; set; }
    }

    public class AddressDto
    {
        public int Lat { get; set; }
        public int Lng { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
    }

    public class StudentDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
        public string GradeLevel { get; set; }
        public ContactInfoDto ContactInfo { get; set; }
    }
    public class ParentDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
        public ContactInfoDto ContactInfo { get; set; }
    }
}
