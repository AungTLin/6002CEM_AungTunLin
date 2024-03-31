using System.ComponentModel.DataAnnotations;



namespace Fungry.API.Data.Entities
    {
        public class User

        {
            [Key]

            public Guid Id { get; set; }

            [Required, MaxLength(30)]
            public string Name { get; set; }

            [Required, MaxLength(80)]
            public string Email { get; set; }

            [Required, MaxLength(120)]

            public string Address { get; set; }

            [Required, MaxLength(30)]

            public string Salt { get; set; }

            [Required, MaxLength(100)]
            public string Hash { get; set; }
            
            
        }



}

