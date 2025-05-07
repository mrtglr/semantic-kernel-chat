namespace semantic_kernel_chat.Services
{
    public interface IChatService
    {
        Task RealTimeChat(string message, CancellationToken cancellationToken);
    }
}
