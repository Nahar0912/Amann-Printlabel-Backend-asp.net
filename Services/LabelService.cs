using Backend.Data;
using Backend.DTOs;
using Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class LabelService
    {
        private readonly AppDbContext _context;

        public LabelService(AppDbContext context)
        {
            _context = context;
        }

        // Create label
        public async Task<LabelEntity> CreateLabelAsync(LabelDto dto)
        {
            // Check for existing article
            var article = await _context.Articles.FirstOrDefaultAsync(a => a.Article_No == dto.ARTICLE_NO);

            if (article == null)
            {
                // If the article doesn't exist, create a new one with default values
                article = new ArticleEntity
                {
                    Article_No = dto.ARTICLE_NO ?? "ART123", // Default article number
                    Tex_No = dto.TEX_NO ?? "TEX123", // Default Tex No
                    Length = dto.LENGTH ?? "100", // Default length
                    Cone_Round_Tex = dto.CONE_ROUND_TEX ?? "CONE123", // Default cone round tex
                    No_of_Cones_inside_the_Carton = dto.CARTON_INSIDE_QUANTITY ?? "20" // Default carton inside quantity
                };
                _context.Articles.Add(article);
                await _context.SaveChangesAsync();
            }

            var label = new LabelEntity
            {
                BAR_CODE = dto.BAR_CODE,
                ORDER_QUANTITY = dto.ORDER_QUANTITY,

                // Set default values for other required fields
                BATCH_LOT_NO = "LT323",  // Default value
                COLOR_CODE = "BLUE",  // Default value
                ARTICLE_NO = dto.ARTICLE_NO ?? "ART123",  // Default value if not provided
                DATE = DateTime.Now,  // Current date
                CARTON_INSIDE_QUANTITY = dto.CARTON_INSIDE_QUANTITY ?? "20",  // Default value
                TEX_NO = dto.TEX_NO ?? "TEX123",  // Default value
                LENGTH = dto.LENGTH ?? "100",  // Default value
                CONE_ROUND_TEX = dto.CONE_ROUND_TEX ?? "CONE123",  // Default value
                NO_OF_STICKER_WITH_FULL_BOX = dto.NO_OF_STICKER_WITH_FULL_BOX ?? "5",  // Default value
                NO_OF_LOOSE_QUANTITY_IN_LAST_STICKER = dto.NO_OF_LOOSE_QUANTITY_IN_LAST_STICKER ?? "10",  // Default value
                PRINT_QUANTITY_FOR_LOOSE_STICKER = dto.PRINT_QUANTITY_FOR_LOOSE_STICKER ?? "20",  // Default value
                PRINT_QUANTITY_FOR_CONE_ROUND_STICKER = dto.PRINT_QUANTITY_FOR_CONE_ROUND_STICKER ?? "15",  // Default value
                AMANN_COLOR_CODE = dto.AMANN_COLOR_CODE ?? "AM123",  // Default value
                COMPETETOR_COLOR_CODE = dto.COMPETETOR_COLOR_CODE ?? "COMP456",  // Default value
            };

            _context.Labels.Add(label);
            await _context.SaveChangesAsync();

            return label;
        }

        // Get all labels
        public async Task<IEnumerable<LabelEntity>> GetAllLabelsAsync()
        {
            return await _context.Labels.ToListAsync();
        }

        // Get label by ID
        public async Task<LabelEntity?> GetLabelByIdAsync(int id)
        {
            return await _context.Labels.FirstOrDefaultAsync(l => l.ID == id);
        }

        // Update label
        public async Task<LabelEntity?> UpdateLabelAsync(int id, UpdateLabelDto dto)
        {
            var label = await _context.Labels.FirstOrDefaultAsync(l => l.ID == id);
            if (label == null)
                return null;

            // Update label fields
            label.BAR_CODE = dto.BAR_CODE ?? label.BAR_CODE;
            label.ORDER_QUANTITY = dto.ORDER_QUANTITY ?? label.ORDER_QUANTITY;
            label.BATCH_LOT_NO = dto.BATCH_LOT_NO ?? label.BATCH_LOT_NO;
            label.COLOR_CODE = dto.COLOR_CODE ?? label.COLOR_CODE;
            label.ARTICLE_NO = dto.ARTICLE_NO ?? label.ARTICLE_NO;
            label.DATE = dto.DATE ?? label.DATE;
            label.CARTON_INSIDE_QUANTITY = dto.CARTON_INSIDE_QUANTITY ?? label.CARTON_INSIDE_QUANTITY;
            label.TEX_NO = dto.TEX_NO ?? label.TEX_NO;
            label.LENGTH = dto.LENGTH ?? label.LENGTH;
            label.CONE_ROUND_TEX = dto.CONE_ROUND_TEX ?? label.CONE_ROUND_TEX;
            label.NO_OF_STICKER_WITH_FULL_BOX = dto.NO_OF_STICKER_WITH_FULL_BOX ?? label.NO_OF_STICKER_WITH_FULL_BOX;
            label.NO_OF_LOOSE_QUANTITY_IN_LAST_STICKER = dto.NO_OF_LOOSE_QUANTITY_IN_LAST_STICKER ?? label.NO_OF_LOOSE_QUANTITY_IN_LAST_STICKER;
            label.PRINT_QUANTITY_FOR_LOOSE_STICKER = dto.PRINT_QUANTITY_FOR_LOOSE_STICKER ?? label.PRINT_QUANTITY_FOR_LOOSE_STICKER;
            label.PRINT_QUANTITY_FOR_CONE_ROUND_STICKER = dto.PRINT_QUANTITY_FOR_CONE_ROUND_STICKER ?? label.PRINT_QUANTITY_FOR_CONE_ROUND_STICKER;
            label.AMANN_COLOR_CODE = dto.AMANN_COLOR_CODE ?? label.AMANN_COLOR_CODE;
            label.COMPETETOR_COLOR_CODE = dto.COMPETETOR_COLOR_CODE ?? label.COMPETETOR_COLOR_CODE;

            // Sync article
            var article = await _context.Articles.FirstOrDefaultAsync(a => a.Article_No == dto.ARTICLE_NO);
            if (article == null)
            {
                article = new ArticleEntity
                {
                    Article_No = dto.ARTICLE_NO,
                    Tex_No = dto.TEX_NO,
                    Length = dto.LENGTH,
                    Cone_Round_Tex = dto.CONE_ROUND_TEX,
                    No_of_Cones_inside_the_Carton = dto.CARTON_INSIDE_QUANTITY
                };
                _context.Articles.Add(article);
            }
            else
            {
                article.Tex_No = dto.TEX_NO ?? article.Tex_No;
                article.Length = dto.LENGTH ?? article.Length;
                article.Cone_Round_Tex = dto.CONE_ROUND_TEX ?? article.Cone_Round_Tex;
                article.No_of_Cones_inside_the_Carton = dto.CARTON_INSIDE_QUANTITY ?? article.No_of_Cones_inside_the_Carton;
            }

            await _context.SaveChangesAsync();
            return label;
        }

        // Delete label
        public async Task<bool> DeleteLabelAsync(int id)
        {
            var label = await _context.Labels.FirstOrDefaultAsync(l => l.ID == id);
            if (label == null)
                return false;

            _context.Labels.Remove(label);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
