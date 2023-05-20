using FamilyManagement.Data;
using FamilyManagement.Model;
using FamilyManagement.Service;

namespace FamilyManagement.TestTools
{
    public static class FamilyFactory
    {
        public static IFamiliesService CreateService(EFDataContext context)
        {
            var repository = new FamiliesRepository(context);
            return new FamiliesService(repository);
        }
    }
}
