using FamilyManagement.Service.Dtos;
using System;
using System.Threading.Tasks;

namespace FamilyManagement.Service
{
    public interface IFamiliesService
    {
        Task<Guid> Create(CreateFamilyDto family);
        Task Edit(EditFamilyDto dto);
    }
}
