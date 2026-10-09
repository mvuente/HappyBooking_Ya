namespace HappyBooking_Ya.Middleware
{
    /// <summary>
    /// Класс глобальный обработчик исключений
    /// </summary>
    public class GlobalExceptionsHandlingMiddleware
    {
        /// <summary>
        /// делегат вызова конвейера
        /// </summary>
        private readonly RequestDelegate _next;
        /// <summary>
        /// Экземпляр логгера
        /// </summary>
        private readonly ILogger<GlobalExceptionsHandlingMiddleware> _logger;

        /// <summary>
        /// Контсруктор класса
        /// </summary>
        /// <param name="next"> делегат вызова конвейера </param>
        /// <param name="logger"> Экземпляр логгера </param>
        public GlobalExceptionsHandlingMiddleware(
            RequestDelegate next,
            ILogger<GlobalExceptionsHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Точка входа конкретного запроса
        /// </summary>
        /// <param name="httpContext"> контекст запроса </param>
        /// <returns> Сигнал завершения асинхронной операции </returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleException(httpContext, ex);
            }
        }

        /// <summary>
        /// Метод обработки исключений
        /// </summary>
        /// <param name="httpContext"> контекст запроса </param>
        /// <param name="ex"> экземпляр исключения </param>
        /// <returns> Сигнал завершения асинхронной операции </returns>
        private async Task HandleException(HttpContext httpContext, Exception ex)
        {
            _logger.LogError(
                ex,
                "Unhandled exception. Method={Method}, Path={Path}, RequestId={RequestId}",
                httpContext.Request.Method,
                httpContext.Request.Path,
                httpContext.Request.Headers["x-request-id"]);

            if (httpContext.Response.HasStarted)
            {
                return;
            }

            var statusCode = MapStatusCode(ex);

            httpContext.Response.StatusCode = statusCode;
            httpContext.Response.ContentType = "application/json";

            var error = new ProblemDetails
            {
                Status = statusCode,
                Detail = ex.Message
            };

            await httpContext.Response.WriteAsJsonAsync(error);
        }

        /// <summary>
        /// Метод маппинга кода ошибок
        /// </summary>
        /// <param name="ex"> экземпляр исключения </param>
        /// <returns> код ошибки </returns>
        private static int MapStatusCode(Exception ex)
            => ex switch
            {
                FluentValidation.ValidationException ve => StatusCodes.Status400BadRequest,
                NotFoundException nfe => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };
    }
}