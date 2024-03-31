using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fungry.lib.Dtos
{

    public record struct FoodOptionDto(string Taste, string Extra);
    public record FoodDto(int Id,string Name,string Image, double Price, FoodOptionDto[] Options );

  
    
}
