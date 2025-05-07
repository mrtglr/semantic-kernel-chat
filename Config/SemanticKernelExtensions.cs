using Microsoft.SemanticKernel;

namespace semantic_kernel_chat.Config
{
    public static class SemanticKernelExtensions
    {
        public static void AddSemanticKernel(this IServiceCollection services, IConfiguration config)
        {
            services.AddKernel();
            services.AddOpenAIChatCompletion("gpt-4o", config["AI:OpenAi:ApiKey"]);
        }
    }
}
