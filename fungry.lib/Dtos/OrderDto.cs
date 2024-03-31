using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fungry.lib.Dtos
{
    public record OrderItemDto(long Id, int FoodId, string Name, int Quant, double Price, string Taste, string Extra)
    {
        public double TotalPrice => Quant * Price;
    }

    public record OrderDto(long Id, DateTime OrderAt, double TotalPrice, int ItemsCount = 0)
    {
        public string ItemCountDisplay => ItemsCount + (ItemsCount > 1 ? "Items" : "Item");
    }
    public record OrderPlaceDto(OrderDto Order, OrderItemDto[] Items);

    
}
