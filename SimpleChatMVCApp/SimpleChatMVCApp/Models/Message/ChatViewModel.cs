namespace SimpleChatMVCApp.Models.Message
{
    public class ChatViewModel
    {
        public MessageViewModel CurrentMessage { get; set; } = null!;
        public List<MessageViewModel> Message { get; set; } = null!;
    }
}
