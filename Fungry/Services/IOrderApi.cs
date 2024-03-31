using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fungry.lib.Dtos;
using Refit;

namespace Fungry.Services

{
    [Headers("Authorization: Bearer")]
    public interface IOrderApi
    {
        [Post("/api/orders/place-order")]
        Task<ResultDto> PlaceOrderAsync(OrderPlaceDto dto);

        [Get("/api/orders")]
        Task<OrderDto[]> GetMyOrdersAsync();

        [Get("/api/orders/{orderId}/items")]
        Task<OrderItemDto[]> GetOrderItemsAsync(long orderId);
    }
}
