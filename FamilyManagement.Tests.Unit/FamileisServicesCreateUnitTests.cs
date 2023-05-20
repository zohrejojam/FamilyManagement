using FamilyManagement.Model;
using FamilyManagement.Model.Members;
using FamilyManagement.Service;
using FamilyManagement.Service.Dtos;
using FamilyManagement.TestTools;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FamilyManagement.Tests.Unit
{
    public class FamileisServicesCreateUnitTests
    {
        private EFDataContext _context;
        private EFDataContext _readContext;
        private IFamiliesService sut;

        public FamileisServicesCreateUnitTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            sut = FamilyFactory.CreateService(_context);
        }
        [Fact]
        public async void Create_Family_Successfully()
        {
            var dto = new CreateFamilyDto { BusinessId = 2, Email = "fake-email", ExternalId = "23" };

            var familyId = await sut.Create(dto);

            var expected = _readContext.Families.First(_ => _.Id == familyId);
            expected.BusinessId.Should().Be(dto.BusinessId);
            expected.Email.Should().Be(dto.Email);
            expected.ExternalId.Should().Be(dto.ExternalId);

        }

        [Fact]
        public async void Create_Family_With_Insurance()
        {
            var insurance = new Insurance("1", "12", "test", "54", "123456");
            var dto = new CreateFamilyDto
            {
                BusinessId = 2,
                Email = "fake-email",
                ExternalId = "23"
            ,
                Insurance = insurance
            };

            var familyId = await sut.Create(dto);

            var expected = _readContext.Families.Include(_ => _.Insurance).First(_ => _.Id == familyId);
            expected.Should().NotBeNull();
            expected.BusinessId.Should().Be(dto.BusinessId);
            expected.Email.Should().Be(dto.Email);
            expected.ExternalId.Should().Be(dto.ExternalId);
            expected.Insurance.Should().BeEquivalentTo(insurance);
        }

        [Fact]
        public async void Create_Family_With_ContactInfo()
        {
            var contactInfo = new ContactInfoDto { AlternatePhone = "452178", PrimaryPhone = "123456" };
            contactInfo.Address = new AddressDto {
               Lat = 12, Lng = 34, 
                Address1 = "test1", Address2 = "test2", 
                City = "city", State = "state", 
                Country = "country", Zip= "1234" 
            };
            var dto = new CreateFamilyDto
            {
                BusinessId = 2,
                Email = "fake-email",
                ExternalId = "23",
                ContactInfo = contactInfo
            };

            var familyId = await sut.Create(dto);

            var expected = _readContext.Families.Include(_ => _.ContactInfo).ThenInclude(_=>_.Address).First(_ => _.Id == familyId);
            expected.Should().NotBeNull();
            expected.BusinessId.Should().Be(dto.BusinessId);
            expected.Email.Should().Be(dto.Email);
            expected.ExternalId.Should().Be(dto.ExternalId);
            expected.ContactInfo.Should().NotBeNull();
            expected.ContactInfo.Should().BeEquivalentTo(contactInfo);
            expected.ContactInfo.Address.Should().NotBeNull();
            expected.ContactInfo.Address.Should().BeEquivalentTo(contactInfo.Address);
        }

        [Fact]
        public async void Create_Family_With_Student()
        {
            var student = new StudentDto
            {
                FirstName = "zohre",
                LastName = "jojam",
                Gender = "woman",
                DateOfBirth = "1370/05/10",
                GradeLevel = "A"
            };
            student.ContactInfo = new ContactInfoDto { AlternatePhone = "452178", PrimaryPhone = "123456" };
            student.ContactInfo.Address = new AddressDto
            {
                Lat = 12,
                Lng = 34,
                Address1 = "test1",
                Address2 = "test2",
                City = "city",
                State = "state",
                Country = "country",
                Zip = "1234"
            };
            var dto = new CreateFamilyDto
            {
                BusinessId = 2,
                Email = "fake-email",
                ExternalId = "23",
                Students = new List<StudentDto>
                {
                    student
                }
            };

            var familyId = await sut.Create(dto);

            var expected = _readContext.Families.Include(_ => _.Students).ThenInclude(_=>_.ContactInfo.Address).First(_ => _.Id == familyId);
            expected.BusinessId.Should().Be(dto.BusinessId);
            expected.Email.Should().Be(dto.Email);
            expected.ExternalId.Should().Be(dto.ExternalId);
            expected.Students.Should().NotBeNull();
            expected.Students.Should().ContainEquivalentOf(student);
            expected.Students.Select(_=>_.ContactInfo).Should().ContainEquivalentOf(student.ContactInfo);
        }

        [Fact]
        public async void Create_Family_With_Parent()
        {
            var parent = new ParentDto
            {
                FirstName = "zohre",
                LastName = "jojam",
                Gender = "woman",
                DateOfBirth = "1370/05/10"
            };
            var dto = new CreateFamilyDto
            {
                BusinessId = 2,
                Email = "fake-email",
                ExternalId = "23",
                Parents = new List<ParentDto>
                {
                    parent
                }
            };

            var familyId = await sut.Create(dto);

            var expected = _readContext.Families.Include(_ => _.Parents).First(_ => _.Id == familyId);
            expected.BusinessId.Should().Be(dto.BusinessId);
            expected.Email.Should().Be(dto.Email);
            expected.ExternalId.Should().Be(dto.ExternalId);
            expected.Parents.Should().NotBeNull();
            expected.Parents.Should().ContainEquivalentOf(parent);
        }
    }


}
