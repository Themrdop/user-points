using Microsoft.AspNetCore.Http;
using System;

namespace UserPoints.DataAccess
{
    public static class ExtetionMethods
    {
        public static int? GetOkObjectResultStatusCode(this IResult result)
        {
            return (int?)Type.GetType("Microsoft.AspNetCore.Http.Result.OkObjectResult, Microsoft.AspNetCore.Http.Results")?.GetProperty("StatusCode", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public).GetValue(result);
        }

        public static object? GetOkObjectResultValue(this IResult result)
        {
            return Type.GetType("Microsoft.AspNetCore.Http.Result.OkObjectResult, Microsoft.AspNetCore.Http.Results")?.GetProperty("Value", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public).GetValue(result);
        }
    }
}
