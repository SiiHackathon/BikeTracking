using System.Collections.Generic;
using BikeTracker.Entities;

namespace BikeTracker.Repositories
{
    public interface INewsRepository
    {
        News GetById(long id);
        IEnumerable<News> GetAll();
        void Save(News news);
        void Delete(long newsId);
        IEnumerable<News> GetLast(int numberOfRecords);
    }
}