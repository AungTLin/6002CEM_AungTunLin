using System.Linq.Expressions;
using fungry.lib.Dtos;
using Fungry.API.Data;
using Fungry.API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fungry.API.Services
{
    public class OrderService(DataContext context)
    {
        private readonly DataContext _context =context;

        public async Task<ResultDto> PlaceOrderAsync(OrderPlaceDto dto, Guid cutomerId)
        {
            var customer =await _context.Users.FirstOrDefaultAsync(u=> u.Id == cutomerId);
            if (customer is null)
                return ResultDto.Failure("Customer does not exists");
            
            var orderItems  = dto.Items.Select(i =>
            new OrderItem
            {
                Taste=i.Taste,
                FoodId=i.FoodId,
                Name=i.Name,
                Price=i.Price,
                Quant =i.Quant,
                Extra=i.Extra,
                TotalPrice=i.TotalPrice,
                
            }
            
            );

            var order = new Order
            {
                CustomerId = customer.Id,
                CustomerAddress = customer.Address,
                CustomerEmail = customer.Email,
                CustomerName = customer.Name,
                OrderAt = DateTime.Now,
                TotalPrice = orderItems.Sum(o => o.TotalPrice),
                Items = orderItems.ToArray()
            };
            try
            {
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
                return ResultDto.Success();
            }
            catch (Exception ex)
            {
                return ResultDto.Failure(ex.Message);
            }
           
        }

        public async Task<OrderDto[]> GetUserOrderAsync (Guid userId) =>
            await _context.Orders
                                 .Where(o => o.CustomerId == userId)
                                 .Select(o => new OrderDto(o.Id, o.OrderAt, o.TotalPrice,o.Items.Count))
                                 .ToArrayAsync();





        public async Task<OrderItemDto[]> GetUserOrderItemsAsync(long orderId, Guid userId) =>
               await _context.OrderItems
                             .Where(i=> i.OrderId == orderId && i.Order.CustomerId == userId)
                             .Select(i=> new OrderItemDto(i.Id, i.FoodId,i.Name,i.Quant,i.Price,i.Extra,i.Taste))
            .ToArrayAsync();
        
    }
}
