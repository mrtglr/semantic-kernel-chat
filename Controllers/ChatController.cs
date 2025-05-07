using Microsoft.AspNetCore.Mvc;
using semantic_kernel_chat.Dtos;
using semantic_kernel_chat.Services;

namespace semantic_kernel_chat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatController : ControllerBase
    {
        IChatService chatService;

        public ChatController(IChatService chatService)
        {
            this.chatService = chatService;
        }

        [HttpPost]
        public async Task RealTimeChat([FromBody] ChatRequest request, CancellationToken ct)
        {
            await chatService.RealTimeChat(request.Message, ct);
        }


    }
}
