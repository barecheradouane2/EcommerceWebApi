namespace EcommerceWeb.Api.Middlewars
{
    public class ExceptionHandlerMiddlewar
    {

        private readonly ILogger logger;
        private readonly RequestDelegate next;
        public ExceptionHandlerMiddlewar(ILogger<ExceptionHandlerMiddlewar> logger1,RequestDelegate next)
        {
             this.logger = logger1;
            this.next= next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");
            }
        }

    }
}
