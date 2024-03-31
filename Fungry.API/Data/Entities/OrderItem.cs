using System.ComponentModel.DataAnnotations;

namespace Fungry.API.Data.Entities
{
    public class OrderItem
    {

        [Key]

        public long Id { get; set; }

        public long OrderId { get; set; }

        public int FoodId { get; set; }

        [Required,MaxLength(50)]

        public string Name { get; set; }

        [Range(0.1, double.MaxValue)]

        public double Price { get; set; }

        [Range(1, 100)]
        public int Quant { get; set; }

       

        [Required, MaxLength(50)]

        public string Taste { get; set; }

        [Required, MaxLength(50)]

        public string Extra { get; set; }

        [Range(0.1,double.MaxValue)]

        public double TotalPrice { get; set; }

        public virtual Order Order { get; set; }
    }

}
