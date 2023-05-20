using FamilyManagement.Model;
using System;
using System.Threading.Tasks;

namespace FamilyManagement.Service
{
    public interface IFamiliesRepository
    {
        Task Add(Family family);
        Task<Family> FindById(Guid id);
        Task Edit(Family family);
    }
}
