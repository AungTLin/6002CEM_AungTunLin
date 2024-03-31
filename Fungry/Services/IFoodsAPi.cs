using fungry.lib.Dtos;
using Refit;

namespace Fungry.Services
{
    public interface IFoodsAPi
    {
        [Get("/api/foods")]

        Task<FoodDto[]> GetFoodsAsync();
    }
}
