namespace Ground.Endpoints.WebApi.Middlewares.ApiExceptionHandler
{
    /// <summary>
    /// Represents an error that can be returned by the API.
    /// </summary>
    public class ApiError
    {
        public string Id { get; set; }
        public short Status { get; set; }
        public string Code { get; set; }
        public string Links { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
    }
}
