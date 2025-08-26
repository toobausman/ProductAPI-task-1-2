namespace ProductAPI.Results
{
    public class ApiResult<T>
    {
        public ApiStatus Status { get; init; }
        public T? Data { get; init; }
        public string? RouteName { get; init; }
        public object? RouteValues { get; init; }

        public static ApiResult<T> Ok(T data) => new() { Status = ApiStatus.Ok, Data = data };

        // NEW: allow 201 without Location header
        public static ApiResult<T> Created(T data) => new() { Status = ApiStatus.Created, Data = data };

        public static ApiResult<T> Created(T data, string routeName, object routeValues)
            => new() { Status = ApiStatus.Created, Data = data, RouteName = routeName, RouteValues = routeValues };

        public static ApiResult<T> NoContent() => new() { Status = ApiStatus.NoContent };
        public static ApiResult<T> NotFound() => new() { Status = ApiStatus.NotFound };
        public static ApiResult<T> Conflict() => new() { Status = ApiStatus.Conflict };
        public static ApiResult<T> Unauthorized() => new() { Status = ApiStatus.Unauthorized };
        public static ApiResult<T> BadRequest() => new() { Status = ApiStatus.BadRequest };
    }
}
