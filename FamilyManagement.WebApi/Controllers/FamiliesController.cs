using FamilyManagement.Service;
using FamilyManagement.Service.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FamilyManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamiliesController : ControllerBase
    {
        private readonly IFamiliesService _service;

        public FamiliesController(IFamiliesService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<Guid> Create(CreateFamilyDto dto)
        {
            return await _service.Create(dto);
        }

        [HttpPut]
        public async Task Edit(EditFamilyDto dto)
        {
            await _service.Edit(dto);
        }
    }
}
