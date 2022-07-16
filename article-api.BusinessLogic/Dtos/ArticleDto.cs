using System;

namespace article_api.BusinessLogic.Dtos
{
    public class ArticleDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }
    }
}
