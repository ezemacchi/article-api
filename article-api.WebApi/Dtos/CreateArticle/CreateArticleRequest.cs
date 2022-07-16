using System.ComponentModel.DataAnnotations;

namespace article_api.WebApi.Dtos.CreateArticle
{
    public class CreateArticleRequest
    {
        [Required(ErrorMessage = "The Title is required")]
        public string Title { get; set; }

        public string Text { get; set; }
    }
}
