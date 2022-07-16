using System;

namespace article_api.BusinessLogic.Dtos.CreateArticle
{
    public class CreateArticleResponse
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }
    }
}
