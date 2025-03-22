using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Web.Models.Dto
{
    public class VillaCreateDTO
    {
        
        
        public string Name { get; set; }
        public string Description { get; set; }
        public double Rate { get; set; }
        public int sqft { get; set; }
        public int Occupancy { get; set; }
        public string ImageUrl { get; set; }
        public string Amenity { get; set; }
    }
}
