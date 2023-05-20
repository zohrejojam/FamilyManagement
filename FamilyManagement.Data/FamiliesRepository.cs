using FamilyManagement.Model;
using FamilyManagement.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace FamilyManagement.Data
{
    public class FamiliesRepository : IFamiliesRepository
    {
        private readonly EFDataContext _context;

        public FamiliesRepository(EFDataContext context)
        {
            _context = context;
        }

        public async Task Add(Family family)
        {
            _context.Families.Add(family);
          await  _context.SaveChangesAsync();
        }

        public async Task Edit(Family family)
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Family> FindById(Guid id)
        {
            return await _context.Families.Include(_=>_.Insurance)
                .Include(_=>_.Students)
                .Include(_=>_.Parents)
                .Include(_=>_.ContactInfo)
                .ThenInclude(_=>_.Address)
                .FirstOrDefaultAsync(_=>_.Id==id)
                ;
        }
    }
}
