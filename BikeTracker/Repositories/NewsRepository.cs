using BikeTracker.Entities;
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

        public IEnumerable<News> GetLast(int? numberOfRecords = null)
        {
            var news = _dbContext.Set<News>().OrderByDescending(n => n.AddedOn);

            if (numberOfRecords.HasValue)
            {
                news = (IOrderedQueryable<News>)news.Take(numberOfRecords.Value);
            }
            return news.ToList();
        }
    }
}