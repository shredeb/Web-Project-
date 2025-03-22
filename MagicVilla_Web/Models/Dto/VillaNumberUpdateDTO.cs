using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Web.Models.Dto
{
    public class VillaNumberUpdateDTO
    {
        [Required]
        public int villaNo { get; set; }
        [Required]
        public int villaID { get; set; }
        public string SpecialDetailes { get; set; }
    }
}
