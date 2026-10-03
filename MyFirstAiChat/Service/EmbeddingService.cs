using Mscc.GenerativeAI;

namespace MyFirstAiChat.Service
{
    public class EmbeddingService
    {
        private readonly string _apiKey;

        public EmbeddingService(IConfiguration config)
        {
            _apiKey = config["Gemini:ApiKey"]!;
        }

        public async Task<float[]> CreateEmbedding(string text)
        {
            var googleAI = new GoogleAI(apiKey: _apiKey);
            var model = googleAI.GenerativeModel(model: "gemini-embedding-001");

            var result = await model.EmbedContent(
                content: text,
                model: "gemini-embedding-001");

            return result.Embedding!.Values!
                         .Select(x => (float)x)
                         .ToArray();
        }
    }
}