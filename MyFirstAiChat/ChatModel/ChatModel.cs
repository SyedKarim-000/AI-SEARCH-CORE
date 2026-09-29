namespace MyFirstAiChat.ChatModel
{
    public class ChatRequest
    {
        public string Message { get; set; } = "";
    }
    public class ChatMessage
    {
        public string Role { get; set; } = "";
        public string Content { get; set; } = "";
    }
    public static class ChatMemory
    {
        public static List<ChatMessage> Messages
            = new();
    }   
}
