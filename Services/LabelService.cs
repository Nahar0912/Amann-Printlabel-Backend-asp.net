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

        public async Task<IEnumerable<LabelEntity>> GetAllAsync()
        {
            return await _context.Labels.ToListAsync();
        }

        public async Task<LabelEntity> GetByIdAsync(int id)
        {
            return await _context.Labels.FindAsync(id);
        }

        public async Task<LabelEntity> CreateAsync(LabelDto dto)
        {
            var label = new LabelEntity
            {
                BAR_CODE = dto.BAR_CODE,
                ORDER_QUANTITY = dto.ORDER_QUANTITY,
                BATCH_LOT_NO = dto.BATCH_LOT_NO,
                COLOR_CODE = dto.COLOR_CODE,
                ARTICLE_NO = dto.ARTICLE_NO,
                DATE = dto.DATE ?? DateTime.Now,
                CARTON_INSIDE_QUANTITY = dto.CARTON_INSIDE_QUANTITY,
                TEX_NO = dto.TEX_NO,
                LENGTH = dto.LENGTH,
                CONE_ROUND_TEX = dto.CONE_ROUND_TEX,
                NO_OF_STICKER_WITH_FULL_BOX = dto.NO_OF_STICKER_WITH_FULL_BOX,
                NO_OF_LOOSE_QUANTITY_IN_LAST_STICKER = dto.NO_OF_LOOSE_QUANTITY_IN_LAST_STICKER,
                PRINT_QUANTITY_FOR_LOOSE_STICKER = dto.PRINT_QUANTITY_FOR_LOOSE_STICKER,
                PRINT_QUANTITY_FOR_CONE_ROUND_STICKER = dto.PRINT_QUANTITY_FOR_CONE_ROUND_STICKER,
                AMANN_COLOR_CODE = dto.AMANN_COLOR_CODE,
                COMPETETOR_COLOR_CODE = dto.COMPETETOR_COLOR_CODE,
                CREATED_AT = DateTime.Now,
                UPDATED_AT = DateTime.Now,
            };

            _context.Labels.Add(label);
            await _context.SaveChangesAsync();
            return label;
        }

        public async Task<LabelEntity> UpdateAsync(int id, UpdateLabelDto dto)
        {
            var label = await _context.Labels.FindAsync(id);
            if (label == null) return null;

            foreach (var prop in dto.GetType().GetProperties())
            {
                var value = prop.GetValue(dto);
                if (value != null)
                {
                    typeof(LabelEntity).GetProperty(prop.Name)?.SetValue(label, value);
                }
            }

            label.UPDATED_AT = DateTime.Now;
            await _context.SaveChangesAsync();
            return label;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var label = await _context.Labels.FindAsync(id);
            if (label == null) return false;

            _context.Labels.Remove(label);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
