using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Entities
{
    [Table("LabelInfo", Schema = "dbo")]
    public class LabelEntity
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(400)]
        public required string BAR_CODE { get; set; }
        [Required]
        [MaxLength(100)]
        public required string ORDER_QUANTITY { get; set; }

        [MaxLength(300)]
        public string BATCH_LOT_NO { get; set; }

        [MaxLength(200)]
        public string COLOR_CODE { get; set; }

        [MaxLength(200)]
        public string ARTICLE_NO { get; set; }

        public DateTime DATE { get; set; } = DateTime.Now;

        [MaxLength(100)]
        public string CARTON_INSIDE_QUANTITY { get; set; }

        [MaxLength(100)]
        public string TEX_NO { get; set; }

        [MaxLength(200)]
        public string LENGTH { get; set; }

        [MaxLength(200)]
        public string CONE_ROUND_TEX { get; set; }

        [MaxLength(100)]
        public string NO_OF_STICKER_WITH_FULL_BOX { get; set; }

        [MaxLength(100)]
        public string NO_OF_LOOSE_QUANTITY_IN_LAST_STICKER { get; set; }

        [MaxLength(100)]
        public string PRINT_QUANTITY_FOR_LOOSE_STICKER { get; set; }

        [MaxLength(100)]
        public string PRINT_QUANTITY_FOR_CONE_ROUND_STICKER { get; set; }

        [MaxLength(100)]
        public string AMANN_COLOR_CODE { get; set; }

        [MaxLength(100)]
        public string COMPETETOR_COLOR_CODE { get; set; }

        public DateTime CREATED_AT { get; set; } = DateTime.Now;
        public DateTime UPDATED_AT { get; set; } = DateTime.Now;

    }
}
