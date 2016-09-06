﻿using BikeTracker.Entities;

namespace BikeTracker.Repositories
{
    public class NewsRepository : EFRepository<News>, INewsRepository
    {
        public News GetById(long id)
        {
            return GetBy(x => x.NewsId == id);
        }

        public void Delete(long newsId)
        {
            var news = GetById(newsId);
            news.IsDeleted = true;
            Save(news);
        }
    }
}