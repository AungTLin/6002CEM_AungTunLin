using Fungry.API.Services;
using fungry.lib.Dtos;
using System.Security.Claims;

namespace Fungry.API.Endpoints

{
    public static class Endpoints
    {

        private static Guid GetUserId(this ClaimsPrincipal principal) =>
           Guid.Parse(principal.FindFirstValue(ClaimTypes.NameIdentifier)!);
        public static IEndpointRouteBuilder MapEndpoints (this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/signup",
                async (SignupRequestDto dto, AuthService authservice) =>
                TypedResults.Ok(await authservice.SignupAsync(dto)));

            app.MapPost("/api/signin",
                async (SigninRequestDto dto, AuthService authservice) =>
                TypedResults.Ok(await authservice.SigninAsync(dto)));

            app.MapGet("/api/foods",
                async (FoodService foodsService) =>
                TypedResults.Ok(await foodsService.GetFoodsAsync()));

            var orderGroup = app.MapGroup("/api/orders").RequireAuthorization();


            orderGroup.MapPost("/place-order",
                async (OrderPlaceDto dto, ClaimsPrincipal principal, OrderService orderService) =>
                await orderService.PlaceOrderAsync(dto, principal.GetUserId()));

            orderGroup.MapGet("",
                async (ClaimsPrincipal principal, OrderService orderService) =>
                TypedResults.Ok(await orderService.GetUserOrderAsync(principal.GetUserId())));

            orderGroup.MapGet("/{orderId:long}/items",
                async (long orderId, ClaimsPrincipal principal, OrderService orderService) =>
                TypedResults.Ok(await orderService.GetUserOrderItemsAsync(orderId, principal.GetUserId())));
               

            return app;

            

        }
    }
}
