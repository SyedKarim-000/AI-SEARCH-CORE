namespace MyFirstAiChat.ChatModel
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string EmbeddingJson { get; set; } = string.Empty;
    }
}