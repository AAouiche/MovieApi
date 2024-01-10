namespace MovieApi.Middleware
{
    internal class AppException
    {
        private int statusCode;
        private string message;
        private string? v;

        public AppException(int statusCode, string message, string? v)
        {
            this.statusCode = statusCode;
            this.message = message;
            this.v = v;
        }
    }
}