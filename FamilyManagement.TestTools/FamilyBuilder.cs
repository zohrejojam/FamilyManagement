using FamilyManagement.Model;
using FamilyManagement.Model.Members;
using System;
using System.Collections.Generic;

namespace FamilyManagement.TestTools
{
    public class FamilyBuilder
    {
        private Family family = new Family(Guid.NewGuid(),47, "something.gmail.com", "23");

        public FamilyBuilder WithInsurance(Insurance insurance)
        {
            family.Insurance = insurance;
            return this;
        }

        public FamilyBuilder WithContactInfo(ContactInfo contactInfo)
        {
            family.ContactInfo = contactInfo;
            return this;
        }

        public FamilyBuilder WithStudents(List<Student> students)
        {
            family.Students = students;
            return this;
        }

        public FamilyBuilder WithParents(List<Parent> parents)
        {
            family.Parents = parents;
            return this;
        }

        public Family Build(EFDataContext context)
        {
            context.Families.Add(family);
            context.SaveChanges();
            return family;
        }
    }
}
