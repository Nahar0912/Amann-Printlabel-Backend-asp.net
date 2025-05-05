using Backend.Data;
using Backend.DTOs;
using Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class LabelService(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        // Create label
        public async Task<LabelEntity> CreateLabelAsync(LabelDto dto)
        {
            // Check for duplicate
            bool exists = await _context.Labels.AnyAsync(l => l.BAR_CODE == dto.BAR_CODE && l.ORDER_QUANTITY == dto.ORDER_QUANTITY);

            if (exists)
                throw new InvalidOperationException("This barcode with the same quantity already exists.");

            // Split the barcode
            string[] parts = dto.BAR_CODE?.Split('-') ?? [];

            string articleNo = parts.Length > 0 ? parts[0] : "UNKNOWN";
            string colorCode = parts.Length > 1 ? parts[1] : "UNKNOWN";
            string competitorColorCode = parts.Length == 4 ? parts[2] : "UNKNOWN";
            string batchLotNo = parts.Length == 4 ? parts[3] : parts.Length == 3 ? parts[2] : "UNKNOWN";

            // Look up article info
            var article = await _context.Articles.FirstOrDefaultAsync(a => a.Article_No == articleNo);

            // Fallback values if article is null
            int conesPerCarton = Convert.ToInt32(article.No_of_Cones_inside_the_Carton);
            int quantity = Convert.ToInt32(dto.ORDER_QUANTITY); // Convert ORDER_QUANTITY to int

            // ---- Sticker Calculation Logic ----
            int fullStickers = quantity / conesPerCarton;
            int looseQuantity = quantity - (conesPerCarton * fullStickers);
            string printLooseSticker = looseQuantity > 0 ? "1" : "0";
            int coneRoundStickerQty = quantity + 9 + fullStickers;

            // ---- Create Label Entity ----
            var label = new LabelEntity
            {
                BAR_CODE = dto.BAR_CODE,
                ORDER_QUANTITY = quantity.ToString(),

                ARTICLE_NO = articleNo,
                COLOR_CODE = colorCode,
                AMANN_COLOR_CODE = colorCode,
                COMPETETOR_COLOR_CODE = competitorColorCode,
                BATCH_LOT_NO = batchLotNo,

                DATE = DateTime.Now,

                CARTON_INSIDE_QUANTITY = conesPerCarton.ToString(),
                TEX_NO = article?.Tex_No ?? dto.TEX_NO,
                LENGTH = article?.Length ?? dto.LENGTH,
                CONE_ROUND_TEX = article?.Cone_Round_Tex ?? dto.CONE_ROUND_TEX,

                // Automatically calculated fields:
                NO_OF_STICKER_WITH_FULL_BOX = fullStickers.ToString(),
                NO_OF_LOOSE_QUANTITY_IN_LAST_STICKER = looseQuantity.ToString(),
                PRINT_QUANTITY_FOR_LOOSE_STICKER = printLooseSticker.ToString(),
                PRINT_QUANTITY_FOR_CONE_ROUND_STICKER = coneRoundStickerQty.ToString(),
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
            label.UPDATED_AT = DateTime.Now; 

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
