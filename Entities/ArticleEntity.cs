using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Entities
{
    [Table("ArticleInfo", Schema = "dbo")]
    public class ArticleEntity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(200)]
        public string Article_No { get; set; }

        [StringLength(200)]
        public string Tex_No { get; set; }

        [StringLength(200)]
        public string Length { get; set; }

        [StringLength(200)]
        public string Cone_Round_Tex { get; set; }

        [StringLength(200)]
        public string No_of_Cones_inside_the_Carton { get; set; }
    }
}
