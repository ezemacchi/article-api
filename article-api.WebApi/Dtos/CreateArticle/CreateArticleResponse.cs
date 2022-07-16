using System;

namespace article_api.WebApi.Dtos.CreateArticle
{
    public class CreateArticleResponse
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }
    }
}
