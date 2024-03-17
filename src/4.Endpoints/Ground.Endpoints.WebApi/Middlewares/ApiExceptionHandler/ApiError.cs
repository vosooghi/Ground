namespace Ground.Endpoints.WebApi.Middlewares.ApiExceptionHandler
{
    /// <summary>
    /// Error class to show to the user
    /// if we have a web application, we cand send html output
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
