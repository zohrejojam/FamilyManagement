using FamilyManagement.Model;
using FamilyManagement.Model.Members;
using FamilyManagement.Service.Dtos;
using FamilyManagement.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyManagement.Service
{
    public class FamiliesService : IFamiliesService
    {
        private readonly IFamiliesRepository _repository;

        public FamiliesService(IFamiliesRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Create(CreateFamilyDto dto)
        {
            var family = new Family(Guid.NewGuid(), dto.BusinessId, dto.Email, dto.ExternalId);
            family.Insurance = dto.Insurance;
            family.ContactInfo = GenerateContactInfo(dto.ContactInfo);
            family.Students = GenerateStudent(dto.Students);
            family.Parents = GenerateParents(dto.Parents);
            await _repository.Add(family);
            return family.Id;
        }

        public async Task Edit(EditFamilyDto dto)
        {
            var family = await _repository.FindById(dto.Id);
            if (family == null)
                throw new FamilyNotFoundException();
            try
            {
                family.Email = dto.Email;
                family.ExternalId = dto.ExternalId;
                family.BusinessId = dto.BusinessId;

                if (dto.Insurance != null)
                {
                    family.Insurance.PolicyNumber = dto.Insurance.PolicyNumber;
                    family.Insurance.CompanyName = dto.Insurance.CompanyName;
                    family.Insurance.CompanyPhone = dto.Insurance.CompanyPhone;
                    family.Insurance.ExternalId = dto.Insurance.ExternalId;
                }

                EditContactInfo(dto.ContactInfo, family.ContactInfo);
                family.Students.RemoveAll(_ => _.FamilyId == family.Id);
                family.Students = GenerateStudent(dto.Students);
                
                family.Parents.RemoveAll(_ => _.FamilyId == family.Id);
                family.Parents = GenerateParents(dto.Parents);
                await _repository.Edit(family);
            }
            catch (Exception e)
            {

                throw;
            }

        }

        private static void EditContactInfo(ContactInfoDto dto, ContactInfo existContactInfo)
        {
            if (dto != null)
            {
                existContactInfo.PrimaryPhone = dto.PrimaryPhone;
                existContactInfo.AlternatePhone = dto.AlternatePhone;
                if (dto.Address != null)
                {
                    existContactInfo.Address.Address1 = dto.Address.Address1;
                    existContactInfo.Address.Address2 = dto.Address.Address2;
                    existContactInfo.Address.Zip = dto.Address.Zip;
                    existContactInfo.Address.Country = dto.Address.Country;
                    existContactInfo.Address.State = dto.Address.State;
                    existContactInfo.Address.City = dto.Address.City;
                    existContactInfo.Address.Lat = dto.Address.Lat;
                    existContactInfo.Address.Lng = dto.Address.Lng;

                }
            }
        }
        private static ContactInfo GenerateContactInfo(ContactInfoDto dto)
        {
            var result = new ContactInfo();
            if (dto != null && dto.Address != null)
            {
                result = new ContactInfo(dto.PrimaryPhone, dto.AlternatePhone);
                result.Address = new Address(dto.Address.Lat, dto.Address.Lng,
                    dto.Address.Address1, dto.Address.Address2, dto.Address.City,
                    dto.Address.State, dto.Address.Country, dto.Address.Zip);
            }
            return result;
        }

        private static List<Student> GenerateStudent(List<StudentDto> students)
        {
            var result = new List<Student>();
            foreach (var item in students)
            {
                var student =
                     new Student(

                     firstName: item.FirstName,
                     lastName: item.LastName,
                     dateOfBirth: item.DateOfBirth,
                     gradeLevel: item.GradeLevel,
                     gender: item.Gender
                 );
                    student.ContactInfo = GenerateContactInfo(item.ContactInfo);

                result.Add(student);
            }
            return result;
        }


        private static List<Parent> GenerateParents(List<ParentDto> parents)
        {
            var result = new List<Parent>();
            foreach (var item in parents)
            {
                var parent = new Parent(

                    firstName: item.FirstName,
                    lastName: item.LastName,
                    dateOfBirth: item.DateOfBirth,
                    gender: item.Gender
                );
                parent.ContactInfo = GenerateContactInfo(item.ContactInfo);
                result.Add(parent);
            }
            return result;
        }


    }
}
