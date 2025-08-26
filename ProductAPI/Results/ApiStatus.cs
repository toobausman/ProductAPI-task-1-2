using ProductAPI.Results;

namespace ProductAPI.Results
{
    // ✅ ENUM (value type) — provides Ok/Created/... members
    public enum ApiStatus
    {
        Ok = 0,
        Created = 1,
        NoContent = 2,
        NotFound = 3,
        Conflict = 4,
        Unauthorized = 5,
        BadRequest = 6
    }
}
