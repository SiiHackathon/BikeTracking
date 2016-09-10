﻿using BikeTracker.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BikeTracker.Repositories
{
    public class NewsRepository : EFRepository<News>, INewsRepository
    {
        public News GetById(long id)
        {
            return GetBy(x => x.NewsId == id);
        }

        public void DeleteById(long newsId)
        {
            DeleteBy(x => x.NewsId == newsId);
        }

        public IEnumerable<News> GetLast(int numberOfRecords)
        {
            return _dbContext.Set<News>().OrderByDescending(n => n.AddedOn).Take(numberOfRecords).ToList();
        }
    }
}