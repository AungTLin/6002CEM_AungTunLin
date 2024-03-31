using System.Net;
using Fungry.API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fungry.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext (DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet <User> Users{ get; set; }

        public DbSet <Food> Foods { get; set; }

        public DbSet <FoodOption> FoodOptions { get; set; }

        public DbSet <Order> Orders { get; set; }

        public DbSet <OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FoodOption>()
             .HasKey(io => new { io.FoodId, io.Taste, io.Extra });

            AddSeedData(modelBuilder);
        }


        private static void AddSeedData(ModelBuilder modelBuilder)
        {
            Food[] foods = [
                new Food { Id =1, Name ="Chicken Pizza", Image ="https://serenetrail.com/wp-content/uploads/2022/11/A-slice-of-bbq-pizza.jpg",Price = 15.2},
                new Food { Id =2, Name ="Chicken Pizza", Image ="https://serenetrail.com/wp-content/uploads/2022/11/A-slice-of-bbq-pizza.jpg",Price = 15.2},
                new Food { Id =3, Name ="Chicken Pizza", Image ="https://serenetrail.com/wp-content/uploads/2022/11/A-slice-of-bbq-pizza.jpg",Price = 15.2},
                new Food { Id =4, Name ="Chicken Pizza", Image ="https://serenetrail.com/wp-content/uploads/2022/11/A-slice-of-bbq-pizza.jpg",Price = 15.2},
                new Food { Id =5, Name ="Chicken Pizza", Image ="https://serenetrail.com/wp-content/uploads/2022/11/A-slice-of-bbq-pizza.jpg",Price = 15.2},
                new Food { Id =6, Name ="Chicken Pizza", Image ="https://serenetrail.com/wp-content/uploads/2022/11/A-slice-of-bbq-pizza.jpg",Price = 15.2},
                new Food { Id =7, Name ="Chicken Pizza", Image ="https://serenetrail.com/wp-content/uploads/2022/11/A-slice-of-bbq-pizza.jpg",Price = 15.2},
                new Food { Id =8, Name ="Chicken Pizza", Image ="https://serenetrail.com/wp-content/uploads/2022/11/A-slice-of-bbq-pizza.jpg",Price = 15.2},
                new Food { Id =9, Name ="Chicken Pizza", Image ="https://serenetrail.com/wp-content/uploads/2022/11/A-slice-of-bbq-pizza.jpg",Price = 15.2},
                new Food { Id =10, Name ="Chicken Pizza", Image ="https://serenetrail.com/wp-content/uploads/2022/11/A-slice-of-bbq-pizza.jpg",Price = 15.2},
                new Food { Id =11, Name ="Chicken Pizza", Image ="https://serenetrail.com/wp-content/uploads/2022/11/A-slice-of-bbq-pizza.jpg",Price = 15.2},
                new Food { Id =12, Name ="Chicken Pizza", Image ="https://serenetrail.com/wp-content/uploads/2022/11/A-slice-of-bbq-pizza.jpg",Price = 15.2},
                new Food { Id =13, Name ="Chicken Pizza", Image ="https://serenetrail.com/wp-content/uploads/2022/11/A-slice-of-bbq-pizza.jpg",Price = 15.2},
                new Food { Id =14, Name ="Chicken Pizza", Image ="https://serenetrail.com/wp-content/uploads/2022/11/A-slice-of-bbq-pizza.jpg",Price = 15.2},
                new Food { Id =15, Name ="Chicken Pizza", Image ="https://serenetrail.com/wp-content/uploads/2022/11/A-slice-of-bbq-pizza.jpg",Price = 15.2},
                ];
            FoodOption[] foodOptions = [
                new FoodOption {FoodId =1, Taste= "Default", Extra="Cheese"},
                new FoodOption {FoodId =2, Taste="Spicy", Extra="Default"},
                new FoodOption {FoodId =3, Taste="Spicy", Extra="Default"},
                new FoodOption {FoodId =4, Taste="Spicy", Extra="Default"},
                new FoodOption {FoodId =5, Taste="Spicy", Extra="Default"},
                new FoodOption {FoodId =6, Taste="Spicy", Extra="Default"},
                new FoodOption {FoodId =7, Taste="Spicy", Extra="Default"},
                new FoodOption {FoodId =8, Taste="Spicy", Extra="Default"},
                new FoodOption {FoodId =9, Taste="Spicy", Extra="Default"},
                new FoodOption {FoodId =10, Taste="Spicy", Extra="Default"},
                new FoodOption {FoodId =11, Taste="Spicy", Extra="Default"},
                new FoodOption {FoodId =12, Taste="Spicy", Extra="Default"},
                new FoodOption {FoodId =13, Taste="Spicy", Extra="Default"},
                new FoodOption {FoodId =14, Taste="Spicy", Extra="Default"},
                new FoodOption {FoodId =15, Taste="Spicy", Extra="Default"},

                ];

            modelBuilder.Entity<Food>()
                .HasData(foods);

            modelBuilder.Entity<FoodOption>()
                .HasData(foodOptions);
        }

    }


}



