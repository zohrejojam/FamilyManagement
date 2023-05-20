using FamilyManagement.Model;
using FamilyManagement.Model.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyManagement.Service.Dtos
{
    public class EditFamilyDto
    {
        public Guid Id { get; set; }
        public int BusinessId { get; set; }
        public string Email { get; set; }
        public string ExternalId { get; set; }
        public Insurance Insurance { get; set; } 
        public ContactInfoDto ContactInfo { get; set; } 
        public List<StudentDto> Students { get; set; } = new List<StudentDto>();
        public List<ParentDto> Parents { get; set; } = new List<ParentDto>();
    }
}
