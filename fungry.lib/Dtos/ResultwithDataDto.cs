using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fungry.lib.Dtos
{
    internal class ResultwithDataDto
    {
    }
}
public record ResultWithDataDto<TData>(bool IsSuccess, TData Data, string? ErrorMessage)
{
    public static ResultWithDataDto<TData> Success(TData data) => new(true, data, null);

    public static ResultWithDataDto<TData> Failure(string? errorMessage) => new(false, default, errorMessage);
}