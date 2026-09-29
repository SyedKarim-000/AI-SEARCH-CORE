using Google.GenAI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mscc.GenerativeAI;
using Mscc.GenerativeAI.Types;
using MyFirstAiChat.ChatModel;

namespace MyFirstAiChat.Controllers
{
    [Route("api/chat")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IConfiguration _config;

        public ChatController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> Send(ChatRequest request)
        {
            string apiKey = _config["Gemini:ApiKey"]!;

            var googleAI =
                new GoogleAI(apiKey);

            var model =
                googleAI.GenerativeModel(
                    model: Model.Gemini3Flash);

            ChatMemory.Messages.Add(
                new MyFirstAiChat.ChatModel.ChatMessage
                {
                    Role = "user",
                    Content = request.Message
                });

            var prompt =
                string.Join("\n",
                    ChatMemory.Messages.Select(x =>
                        $"{x.Role}: {x.Content}"));

            var response =
                await model.GenerateContent(prompt);

            ChatMemory.Messages.Add(
                new MyFirstAiChat.ChatModel.ChatMessage
                {
                    Role = "assistant",
                    Content = response.Text!
                });

            return Ok(response.Text);
        }



        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? prompt)
        {
            string apiKey = "";

            string textInput = string.IsNullOrWhiteSpace(prompt)
                        ? "Hello! Introduce yourself in one sentence."
                        : prompt;

            var _prompt =
$"""
Explain the following as if I am a 10 year old child.
Question:
{prompt}
""";

            var googleAi = new GoogleAI(apiKey: apiKey);
            var model = googleAi.GenerativeModel(Model.Gemini3Flash);

            var response = await model.GenerateContent(_prompt);
            return Ok(response.Text);
        }   
    }
}
