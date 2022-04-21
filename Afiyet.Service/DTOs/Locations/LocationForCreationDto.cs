using Afiyet.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Afiyet.Service.DTOs.Locations
{
    public class LocationForCreationDto
    {
        [Required]
        public int PlaceDigit { get; set; }

        [Required]
        public PlaceType PlaceType { get; set; }

        public IFormFile Image { get; set; }
    }
}
