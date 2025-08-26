using Microsoft.AspNetCore.Mvc;

namespace ProductAPI.Results
{
    public static class ApiResultExtensions
    {
        public static IActionResult ToActionResult<T>(this ApiResult<T> result, ControllerBase c)
        {
            return result.Status switch
            {
                ApiStatus.Ok => c.Ok(result.Data),
                ApiStatus.Created =>
                    (!string.IsNullOrWhiteSpace(result.RouteName) && result.RouteValues is not null)
                        ? c.CreatedAtRoute(result.RouteName!, result.RouteValues!, result.Data)
                        : c.StatusCode(201, result.Data),
                ApiStatus.NoContent => c.NoContent(),
                ApiStatus.NotFound => c.NotFound(),
                ApiStatus.Conflict => c.Conflict(),
                ApiStatus.Unauthorized => c.Unauthorized(),
                ApiStatus.BadRequest => c.BadRequest(),
                _ => c.StatusCode(500)
            };
        }
    }
}
