using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Application.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        // Constructor nhận vào delegate cho middleware tiếp theo
        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // Phương thức Invoke là nơi xử lý logic của middleware
        public async Task Invoke(HttpContext context)
        {
            // Ghi log thông tin yêu cầu trước khi gọi middleware tiếp theo
            Console.WriteLine($"Request Path: {context.Request.Path}");

            // Gọi middleware tiếp theo trong pipeline
            await _next(context);

            // Sau khi middleware sau đã thực thi, ghi log thông tin phản hồi
            Console.WriteLine("Response sent");
        }

    }
}