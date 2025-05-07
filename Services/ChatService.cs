using Microsoft.SemanticKernel;

namespace semantic_kernel_chat.Services
{
    public class ChatService : IChatService
    {
        private readonly Kernel kernel;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ChatService(Kernel _kernel, IHttpContextAccessor httpContextAccessor)
        {
            kernel = _kernel;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task RealTimeChat(string message, CancellationToken cancellationToken)
        {
            var response = httpContextAccessor.HttpContext.Response;
            response.Headers["Content-Type"] = "text/event-stream";
            response.Headers["Cache-Control"] = "no-cache";
            response.Headers["Connection"] = "keep-alive";

            await foreach (var streaming in kernel.InvokePromptStreamingAsync(message, cancellationToken: cancellationToken))
            {
                var content = streaming?.ToString();
                if (!string.IsNullOrWhiteSpace(content))
                {
                    await response.WriteAsync($"data: {content}\n\n", cancellationToken);
                    await response.Body.FlushAsync(cancellationToken);
                }
            }

            await response.WriteAsync("data: [DONE]\n\n", cancellationToken);
            await response.Body.FlushAsync(cancellationToken);
        }
    }
}
