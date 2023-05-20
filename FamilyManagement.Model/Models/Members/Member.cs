using System;

namespace FamilyManagement.Model.Members
{
    public abstract class Member
    {
        public Member(string firstName , string lastName,string gender,string dateOfBirth)
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            DateOfBirth = dateOfBirth;
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
        public Guid FamilyId { get; set; }
        public int? ContactInfoId { get; set; }
        public ContactInfo ContactInfo { get; set; }
    }
}
