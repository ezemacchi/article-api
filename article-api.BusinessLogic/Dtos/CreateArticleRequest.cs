using System.ComponentModel.DataAnnotations;

namespace article_api.BusinessLogic.Dtos
{
    public class CreateArticleRequest
    {
        [Required(ErrorMessage = "The Title is required")]
        public string Title { get; set; }

        public string Text { get; set; }
    }
}
