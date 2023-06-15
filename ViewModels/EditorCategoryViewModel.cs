using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels
{
    public class EditorCategoryViewModel
    {
        [Required(ErrorMessage = "O nome é Obrigatório")]
        [StringLength(40,MinimumLength = 3, ErrorMessage ="Este Campo deve conter entre 3 a 40 caracterie")]
        public string Name { get; set; }
        [Required(ErrorMessage = "O slug é Obrigatório")]
        public string Slug { get; set; }
    }
}
