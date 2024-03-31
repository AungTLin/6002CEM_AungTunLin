using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fungry.Models;
using SQLite;


namespace Fungry.SQL
{
    public class CartItemEntity
    {
        [PrimaryKey, AutoIncrement]

        public int Id { get; set; }

        public int FoodId { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public string TasteName { get; set; }

        public string ExtraName { get; set; }

        public int Quant {  get; set; }

        public CartItemEntity (Storecart storecartmodel)
        {
            FoodId = storecartmodel.FoodId;
            Name = storecartmodel.Name;
            Price = storecartmodel.Price;
            TasteName = storecartmodel.TasteName;
            ExtraName = storecartmodel.ExtraName;
            Quant = storecartmodel.Quant;
        }

        public CartItemEntity()
        {

        }

        public Storecart ToStorecartModel() =>
            new ()
            {
                Id = Id,
                Name = Name,
                Price = Price,
                TasteName = TasteName,
                ExtraName = ExtraName,
                FoodId = FoodId,
                Quant = Quant,


            };
    }

}
