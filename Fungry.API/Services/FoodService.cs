using System.Runtime.CompilerServices;
using fungry.lib.Dtos;

using Fungry.API.Data;
using Fungry.API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fungry.API.Services
{
    public class FoodService(DataContext context)
    {

       private readonly DataContext _context = context;

        public async Task<FoodDto[]> GetFoodsAsync() =>
            await _context.Foods.AsNoTracking()
            .Select(i => new FoodDto(i.Id, i.Name, i.Image, i.Price, i.Options.Select(o => new FoodOptionDto(o.Taste, o.Extra)).ToArray()))
            .ToArrayAsync();
    }





}
