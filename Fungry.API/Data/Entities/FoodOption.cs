using System.ComponentModel.DataAnnotations;

namespace Fungry.API.Data.Entities
{
    public class FoodOption

    {
        public int FoodId { get; set; }

        [Required, MaxLength(50)]

        public string Taste { get; set; }

        [Required, MaxLength(50)]

        public string Extra {  get; set; }

        public virtual Food Food { get; set; }

    }

}
