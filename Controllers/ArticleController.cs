﻿using Backend.DTOs;
using Backend.Entities;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("articles")]
    [ApiController]
    public class ArticleController(ArticleService articleService) : ControllerBase
    {
        private readonly ArticleService _articleService = articleService;

        // Create a new article
        [HttpPost("add")]
        public async Task<ActionResult<ArticleEntity>> Create([FromBody] ArticleDto articleDto)
        {
            var article = await _articleService.CreateAsync(articleDto);
            return CreatedAtAction(nameof(GetById), new { id = article.Id }, article);
        }

        // Get all articles
        [HttpGet]
        public Task<ActionResult> FindAll()
        {
            var articles = _articleService.FindAll();
            return Task.FromResult<ActionResult>(Ok(articles));
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
        [Authorize]
        [HttpPut("update/{id}")]
        public async Task<ActionResult<ArticleEntity>> Update(int id, [FromBody] UpdateArticleDto updateArticleDto)
        {
            var article = await _articleService.UpdateAsync(id, updateArticleDto);
            return Ok(article);
        }

        // Delete an article
        [Authorize]
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _articleService.DeleteAsync(id);
            return NoContent();
        }
    }
}
