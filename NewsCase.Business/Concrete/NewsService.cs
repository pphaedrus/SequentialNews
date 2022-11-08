

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Driver;
using NewsCase.Business.Abstract;
using NewsCase.Business.Dtos;
using NewsCase.Business.Settings;
using NewsCase.Core.Common;

namespace NewsCase.Business.Concrete
{
    public class NewsService: INewsService
    {
        private readonly IMongoCollection<News> _newsCollection;
        private readonly IMapper _mapper;

        public NewsService(IDatabaseSettings databaseSettings, IMapper mapper)
        {

            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _newsCollection = database.GetCollection<News>(databaseSettings.NewsCollectionName);
            _mapper = mapper;
        }

        public async Task<List<NewsDto>> GetAllAsync()
        {
            var news = await _newsCollection.Find(news => true).ToListAsync();
            return _mapper.Map<List<NewsDto>>(news);
        }

        public async Task<bool> CreateAsync(NewsCreateDto newsDto)
        {
            var allNews = await _newsCollection.Find(news => true).ToListAsync();
            var news = _mapper.Map<News>(newsDto);
            if (!allNews.Any())
            {
                news.QueueNumber = 1;
                news.ChangedDate = DateTime.UtcNow;
            }
            else
            {
                var queue = await _newsCollection.Aggregate().SortByDescending(p => p.QueueNumber).FirstOrDefaultAsync();
                news.QueueNumber = queue.QueueNumber + 1;
                news.ChangedDate = DateTime.UtcNow;
            }

            await _newsCollection.InsertOneAsync(news);
            return true;
        }

        public async Task<bool> CreateManyAsync(IEnumerable<NewsCreateDto> createDtos)
        {
            var news = _mapper.Map<List<News>>(createDtos);
            var allNews = await _newsCollection.Find(news => true).ToListAsync();
            int queueCount;
            if (!allNews.Any())
            {
                queueCount = 0;
            }
            else
            {
                var queue = await _newsCollection.Aggregate().SortByDescending(p => p.QueueNumber).FirstOrDefaultAsync();
                queueCount = queue.QueueNumber;
            }

            foreach (var i in news)
            {
                queueCount += 1;
                i.QueueNumber = queueCount;
                i.ChangedDate = DateTime.UtcNow;
            }
            await _newsCollection.InsertManyAsync(news);
            return true;
        }

        public async Task<NewsDto> GetByIdAsync(string id)
        {
            var news = await _newsCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
            if (news is null)
            {
                return null;
            }
            return _mapper.Map<NewsDto>(news);
        }

        public async Task<bool> UpdateAsync(NewsUpdateDto newsUpdateDto)
        {
            var entity = await _newsCollection.Find(p => p.Id == newsUpdateDto.Id).FirstOrDefaultAsync();
            if(entity is null)
            {
                return false;
            }
            if (_newsCollection.Find(p => p.QueueNumber == newsUpdateDto.QueueNumber && newsUpdateDto.QueueNumber!=entity.QueueNumber).Any())//sıra numaraları tekil olmalı
            {
                return false;
            }
            var result = await _newsCollection.FindOneAndReplaceAsync(p => p.Id == newsUpdateDto.Id, _mapper.Map<News>(newsUpdateDto));
            if (result is null)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _newsCollection.DeleteOneAsync(p => p.Id == id);
            if (entity.DeletedCount > 0)
            {
                return true;
            }
            return false;
        }

    }
}
