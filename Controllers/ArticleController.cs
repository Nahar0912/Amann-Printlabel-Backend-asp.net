using Backend.DTOs;
using Backend.Entities;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("articles")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly ArticleService _articleService;

        public ArticleController(ArticleService articleService)
        {
            _articleService = articleService;
        }

        // Health check endpoint
        [HttpGet("index")]
        public string GetIndex()
        {
            return _articleService.GetIndex();
        }

        // Create a new article
        [HttpPost("add")]
        public async Task<ActionResult<ArticleEntity>> Create([FromBody] ArticleDto articleDto)
        {
            var article = await _articleService.CreateAsync(articleDto);
            return CreatedAtAction(nameof(GetById), new { id = article.Id }, article);
        }

        // Get all articles
        [HttpGet]
        public async Task<ActionResult> FindAll()
        {
            var articles = await _articleService.FindAllAsync();
            return Ok(articles);
        }

        // Get article by ARTICLE_NO
        [HttpGet("get/{articleNo}")]
        public async Task<ActionResult<ArticleEntity>> GetByArticleNo(string articleNo)
        {
            var article = await _articleService.FindOneByArticleNoAsync(articleNo);
            return Ok(article);
        }

        // Get article by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleEntity>> GetById(int id)
        {
            var article = await _articleService.FindOneByIdAsync(id);
            return Ok(article);
        }

        // Update an article
        [HttpPut("update/{id}")]
        public async Task<ActionResult<ArticleEntity>> Update(int id, [FromBody] UpdateArticleDto updateArticleDto)
        {
            var article = await _articleService.UpdateAsync(id, updateArticleDto);
            return Ok(article);
        }

        // Delete an article
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _articleService.DeleteAsync(id);
            return NoContent();
        }
    }
}
