using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace article_api.BusinessLogic.Dtos
{
    public class UpdateArticleRequest
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The Title is required")]
        public string Title { get; set; }

        public string Text { get; set; }
    }
}
