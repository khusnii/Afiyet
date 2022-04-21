using Afiyet.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Afiyet.Service.DTOs.Locations
{
    public class LocationForOrderDto
    {
        [Required]
        public ushort OrderNumber { get; set; }

        [Required]
        public PlaceType PlaceName { get; set; }
    }
}
