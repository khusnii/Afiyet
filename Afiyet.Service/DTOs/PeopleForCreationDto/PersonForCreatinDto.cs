using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Afiyet.Service.DTOs.PeopleForCreationDto
{
    public class PersonForCreatinDto
    {
        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public string Phone { get; set; }

        public IFormFile Image { get; set; }

    }
}
