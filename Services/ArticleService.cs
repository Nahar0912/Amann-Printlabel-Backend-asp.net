using Backend.Data;
using Backend.DTOs;
using Microsoft.EntityFrameworkCore;
using Backend.Entities;

namespace Backend.Services
{
    public class ArticleService
    {
        private readonly AppDbContext _context;

        public ArticleService(AppDbContext context)
        {
            _context = context;
        }

        // Health check method
        public string GetIndex()
        {
            return "Article Service is running";
        }

        // Create a new article
        public async Task<ArticleEntity> CreateAsync(ArticleDto articleDto)
        {
            var article = new ArticleEntity
            {
                Article_No = articleDto.Article_No,
                Tex_No = articleDto.Tex_No,
                Length = articleDto.Length,
                Cone_Round_Tex = articleDto.Cone_Round_Tex,
                No_of_Cones_inside_the_Carton = articleDto.No_of_Cones_inside_the_Carton
            };

            _context.Articles.Add(article);
            await _context.SaveChangesAsync();
            return article;
        }

        // Get all articles
        public async Task<IQueryable<ArticleEntity>> FindAllAsync()
        {
            return _context.Articles;
        }

        // Get article by ARTICLE_NO
        public async Task<ArticleEntity> FindOneByArticleNoAsync(string articleNo)
        {
            var article = await _context.Articles
                .FirstOrDefaultAsync(a => a.Article_No == articleNo);

            if (article == null)
            {
                throw new KeyNotFoundException($"Article with ARTICLE_NO {articleNo} not found");
            }

            return article;
        }

        // Get article by ID
        public async Task<ArticleEntity> FindOneByIdAsync(int id)
        {
            var article = await _context.Articles.FirstOrDefaultAsync(a => a.Id == id);

            if (article == null)
            {
                throw new KeyNotFoundException($"Article with ID {id} not found");
            }

            return article;
        }

        // Update an article
        public async Task<ArticleEntity> UpdateAsync(int id, UpdateArticleDto updateArticleDto)
        {
            var article = await FindOneByIdAsync(id);

            if (updateArticleDto.Article_No != null)
                article.Article_No = updateArticleDto.Article_No;

            if (updateArticleDto.Tex_No != null)
                article.Tex_No = updateArticleDto.Tex_No;

            if (updateArticleDto.Length != null)
                article.Length = updateArticleDto.Length;

            if (updateArticleDto.Cone_Round_Tex != null)
                article.Cone_Round_Tex = updateArticleDto.Cone_Round_Tex;

            if (updateArticleDto.No_of_Cones_inside_the_Carton != null)
                article.No_of_Cones_inside_the_Carton = updateArticleDto.No_of_Cones_inside_the_Carton;

            await _context.SaveChangesAsync();

            return article;
        }

        // Delete an article
        public async Task DeleteAsync(int id)
        {
            var article = await FindOneByIdAsync(id);
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
        }
    }
}
