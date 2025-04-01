using System.Text;

namespace Assignment.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _logFilePath = "log.txt";
        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var request = context.Request;

            string requestBody = request.Method != HttpMethod.Get.ToString() 
                ? await ReadRequestBodyAsync(request) 
                : "[No Body]";

            StringBuilder logMessage = new StringBuilder();
            logMessage.AppendLine("New Request:");
            logMessage.AppendLine($"Schema: {request.Scheme}");
            logMessage.AppendLine($"Host: {request.Host}");
            logMessage.AppendLine($"Path: {request.Path}");
            logMessage.AppendLine($"QueryString {request.QueryString}");
            logMessage.AppendLine($"Request Body: {Environment.NewLine} {requestBody}");
            logMessage.AppendLine($"------------------------------------------------------");
            
            try
            {
                await File.AppendAllTextAsync(_logFilePath, logMessage.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error writing to log file: {e.Message}");
            }

            await _next(context);
        }

        public async Task<string> ReadRequestBodyAsync(HttpRequest request)
        {
            request.EnableBuffering();
            var bodyContent = "";
            if (request.Method != HttpMethod.Get.ToString())
            {
                using (var reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
                {
                    bodyContent = await reader.ReadToEndAsync();
                }
            }
            return bodyContent;
        }

    }
}
