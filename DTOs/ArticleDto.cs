using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs
{
    public class ArticleDto
    {
        [Required]
        [StringLength(200)]
        public string Article_No { get; set; }

        [Required]
        [StringLength(200)]
        public string Tex_No { get; set; }

        [Required]
        [StringLength(200)]
        public string Length { get; set; }

        [Required]
        [StringLength(200)]
        public string Cone_Round_Tex { get; set; }

        [Required]
        [StringLength(200)]
        public string No_of_Cones_inside_the_Carton { get; set; }
    }

    public class UpdateArticleDto
    {
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
