using FamilyManagement.Model;
using FamilyManagement.Model.Members;
using FamilyManagement.Service;
using FamilyManagement.Service.Dtos;
using FamilyManagement.Service.Exceptions;
using FamilyManagement.TestTools;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FamilyManagement.Tests.Unit
{
    public class FamilyServicesEditUnitTests
    {
        private EFDataContext _context;
        private EFDataContext _readContext;
        private IFamiliesService sut;

        public FamilyServicesEditUnitTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            sut = FamilyFactory.CreateService(_context);
        }
        [Fact]
        public async void Edit_Family_Successfully()
        {
            var family = new FamilyBuilder().Build(_context);
            var dto = new EditFamilyDto { Id = family.Id, BusinessId = 7, Email = "fake-email", ExternalId = "43" };

            await sut.Edit(dto);

            var expected = _readContext.Families.First(_ => _.Id == family.Id);
            expected.BusinessId.Should().Be(dto.BusinessId);
            expected.Email.Should().Be(dto.Email);
            expected.ExternalId.Should().Be(dto.ExternalId);
        }

        [Fact]
        public void Edit_Fammily_NotFoundException()
        {
            var dto = new EditFamilyDto { Id = Guid.NewGuid(), BusinessId = 7, Email = "fake-email", ExternalId = "43" };

            Func<Task> expected = async () => await sut.Edit(dto);

            expected.Should().ThrowExactlyAsync<FamilyNotFoundException>();
        }

        [Fact]
        public async void Edit_Family_With_Insurance()
        {
            var oldInsurance = new Insurance("1", "12", "first", "54", "123456");
            var newInsurance = new Insurance("1", "455", "second", "467", "569874");
            var family = new FamilyBuilder().WithInsurance(oldInsurance).Build(_context);
            var dto = new EditFamilyDto
            {
                Id = family.Id,
                BusinessId = 7,
                Email = "fake-email",
                ExternalId = "43",
                Insurance = newInsurance
            };

            await sut.Edit(dto);

            var expected = _readContext.Families.Include(_ => _.Insurance).First(_ => _.Id == family.Id);
            expected.Should().NotBeNull();
            expected.BusinessId.Should().Be(dto.BusinessId);
            expected.Email.Should().Be(dto.Email);
            expected.ExternalId.Should().Be(dto.ExternalId);
            expected.Insurance.Should().NotBeNull();
            expected.Insurance.Should().BeEquivalentTo(newInsurance);
        }

        [Fact]
        public async void Edit_Family_With_ContactInfo()
        {

            var oldContactInfo = new ContactInfo { AlternatePhone = "452178", PrimaryPhone = "123456" };
            oldContactInfo.Address = new Address
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
            var newContactInfo = new ContactInfoDto { AlternatePhone = "789456", PrimaryPhone = "541263" };
            newContactInfo.Address = new AddressDto
            {
                Lat = 32,
                Lng = 67,
                Address1 = "test2",
                Address2 = "test2",
                City = "city2",
                State = "state2",
                Country = "country2",
                Zip = "12342"
            };
            var family = new FamilyBuilder().WithContactInfo(oldContactInfo).Build(_context);
            var dto = new EditFamilyDto
            {
                Id = family.Id,
                BusinessId = 7,
                Email = "fake-email",
                ExternalId = "43",
                ContactInfo = newContactInfo
            };

            await sut.Edit(dto);

            var expected = _readContext.Families.Include(_ => _.ContactInfo).ThenInclude(_=>_.Address).First(_ => _.Id == family.Id);
            expected.Should().NotBeNull();
            expected.BusinessId.Should().Be(dto.BusinessId);
            expected.Email.Should().Be(dto.Email);
            expected.ExternalId.Should().Be(dto.ExternalId);
            expected.ContactInfo.Should().NotBeNull();
            expected.ContactInfo.Should().BeEquivalentTo(newContactInfo);
            expected.ContactInfo.Address.Should().NotBeNull();
            expected.ContactInfo.Address.Should().BeEquivalentTo(newContactInfo.Address);
        }

        [Fact]
        public async void Edit_Family_With_Student()
        {
            var oldStudent = new Student("zohre", "jojam", "woman", "1370/05/10", "A");
            var newStudent = new StudentDto
            {
                FirstName = "zohre",
                LastName = "jojam",
                Gender = "woman",
                DateOfBirth = "1370/05/10",
                GradeLevel = "A"
            };
            newStudent.ContactInfo = new ContactInfoDto { AlternatePhone = "452178", PrimaryPhone = "123456" };
            newStudent.ContactInfo.Address = new AddressDto
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
            var family = new FamilyBuilder().WithStudents(new List<Student> { oldStudent}).Build(_context);
            var dto = new EditFamilyDto
            {
                Id = family.Id,
                BusinessId = 7,
                Email = "fake-email",
                ExternalId = "43",
                Students = new List<StudentDto> { newStudent}
            };

            await sut.Edit(dto);

            var expected = _readContext.Families.Include(_ => _.Students).ThenInclude(_=>_.ContactInfo.Address).First(_ => _.Id == family.Id);
            expected.BusinessId.Should().Be(dto.BusinessId);
            expected.Email.Should().Be(dto.Email);
            expected.ExternalId.Should().Be(dto.ExternalId);
            expected.Students.Should().NotBeNull();
            expected.Students.Should().ContainEquivalentOf(newStudent);
            expected.Students.Select(_ => _.ContactInfo).Should().ContainEquivalentOf(newStudent.ContactInfo);
        }

        [Fact]
        public async void Edit_Family_With_Parent()
        {
            var oldParent = new Parent("zohre", "jojam", "woman", "1370/05/10");
            var newParent = new ParentDto
            {
                FirstName = "vahid",
                LastName = "shirvan",
                Gender = "man",
                DateOfBirth = "1360/05/10"
            };
            var family = new FamilyBuilder().WithParents(new List<Parent> { oldParent }).Build(_context);
            var dto = new EditFamilyDto
            {
                Id = family.Id,
                BusinessId = 7,
                Email = "fake-email",
                ExternalId = "43",
                Parents = new List<ParentDto> { newParent }
            };

            await sut.Edit(dto);

            var expected = _readContext.Families.Include(_ => _.Parents).First(_ => _.Id == family.Id);
            expected.BusinessId.Should().Be(dto.BusinessId);
            expected.Email.Should().Be(dto.Email);
            expected.ExternalId.Should().Be(dto.ExternalId);
            expected.Parents.Should().NotBeNull();
            expected.Parents.Should().ContainEquivalentOf(newParent);
        }
    }
}
