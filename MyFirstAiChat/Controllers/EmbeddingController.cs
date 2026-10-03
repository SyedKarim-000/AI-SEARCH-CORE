using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFirstAiChat.ChatModel;
using MyFirstAiChat.Data;
using MyFirstAiChat.Helper;
using MyFirstAiChat.Service;
using Newtonsoft.Json;

namespace MyFirstAiChat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmbeddingController : ControllerBase
    {
        private readonly EmbeddingService _embeddingService;
        private readonly AppDbContext _db;

        public EmbeddingController(EmbeddingService embeddingService, AppDbContext db)
        {
            _embeddingService = embeddingService;
            _db = db;
        }

        [HttpPost]
        public async Task Save(string description)
        {
            //string description = "Apple iPhone 16 Pro smartphone";

            var embedding = await _embeddingService.CreateEmbedding(description);

            var product = new Product
            {
                Name = "iPhone 16 Pro",
                Description = description,
                EmbeddingJson = JsonConvert.SerializeObject(embedding)
            };

            _db.Products.Add(product);
            await _db.SaveChangesAsync();
        }

        [HttpGet]
        public async Task<IActionResult> Search(string query)
        {
            var queryEmbedding = await _embeddingService.CreateEmbedding(query);

            var products = await _db.Products.ToListAsync();

            var result = products
                .Select(p => new
                {
                    Product = p,
                    Score = SimilarityHelper.CosineSimilarity(
                        queryEmbedding,
                        JsonConvert.DeserializeObject<float[]>(p.EmbeddingJson)!)
                })
                .OrderByDescending(x => x.Score)
                .Take(5)
                .ToList();

            return Ok(result);
        }
    }
}