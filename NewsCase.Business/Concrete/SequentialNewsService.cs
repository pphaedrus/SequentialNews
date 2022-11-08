using AutoMapper;
using MongoDB.Driver;
using NewsCase.Business.Abstract;
using NewsCase.Business.Dtos;
using NewsCase.Business.Settings;
using NewsCase.Core.Common;
using NewsCase.DataAccess.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsCase.Business.Concrete
{
    public class SequentialNewsService: ISequentialNewsService
    {
        private readonly IMongoCollection<News> _newsCollection;
        private readonly IMongoCollection<SequentialNews> _sequentialNewsCollection;
        private readonly IMapper _mapper;
        public SequentialNewsService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _newsCollection = database.GetCollection<News>(databaseSettings.NewsCollectionName);
            _sequentialNewsCollection = database.GetCollection<SequentialNews>(databaseSettings.SequentialNewsCollectionName);
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(SequentialNewsCreateDto sequentialNewsCreateDto)
        {

            if (_sequentialNewsCollection.Find(p => p.Title.ToLower() == sequentialNewsCreateDto.Title.ToLower()).Any()) //title tekrarlanmamalı
            {
                return false;
            }

            await _sequentialNewsCollection.InsertOneAsync(_mapper.Map<SequentialNews>(sequentialNewsCreateDto));
            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _sequentialNewsCollection.DeleteOneAsync(p => p.Id == id);
            if (entity.DeletedCount > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<SequentialNewsDto> GetByIdAsync(string id)
        {
            var sequentialNews = await _sequentialNewsCollection.Find(p => p.Id == id).FirstOrDefaultAsync();

            if (sequentialNews is null)
            {
                return null;
            }
            var sequential = _mapper.Map<SequentialNewsDto>(sequentialNews);
            List<NewsDto> newsInstance = new List<NewsDto>();
            sequential.News = newsInstance;
            foreach (var i in sequential.Rows)
            {
                var news = await _newsCollection.Find(p => p.QueueNumber == i).FirstOrDefaultAsync();
                sequential.News.Add(_mapper.Map<NewsDto>(news));
            }
            return sequential;
        }

        //case'deki domain.com/api/v1/veri-seti/anasayfa-manset-kutusu karşılığı
        public async Task<List<NewsDto>> GetByTitleAsync(string title)
        {
            var sequentialNews = await _sequentialNewsCollection.Find(p => p.Title.ToLower() == title.ToLower()).FirstOrDefaultAsync();
            if (sequentialNews is null)
            {
                return null;
            }

            List<NewsDto> newsInstance = new List<NewsDto>();
            foreach (var i in sequentialNews.Rows)
            {
                var news = await _newsCollection.Find(p => p.QueueNumber == i).FirstOrDefaultAsync();
                if (news != null)
                {
                    if (sequentialNews.NewsIds.Find(p => p == news.Id) != null)
                    {
                        newsInstance.Add(_mapper.Map<NewsDto>(news));
                    }
                }

            }
            return newsInstance;
        }

        public async Task<bool> UpdateAsync(SequentialNewsUpdateDto sequentialNewsUpdateDto)
        {
            var entity = await _sequentialNewsCollection.Find(p => p.Id == sequentialNewsUpdateDto.Id).FirstOrDefaultAsync();

            if (_sequentialNewsCollection.Find(p => p.Title.ToLower() == sequentialNewsUpdateDto.Title.ToLower() && sequentialNewsUpdateDto.Title.ToLower() != entity.Title.ToLower()).Any()) //title tekrarlanmamalı
            {
                return false;
            }
            var result = await _sequentialNewsCollection.FindOneAndReplaceAsync(p => p.Id == sequentialNewsUpdateDto.Id, _mapper.Map<SequentialNews>(sequentialNewsUpdateDto));
            if (result is null)
            {
                return false;
            }

            return true;
        }

        public async Task<List<SequentialNewsDto>> GetAllAsync()
        {
            var sequentialNews = await _sequentialNewsCollection.Find(news => true).ToListAsync();
            var sequentialNewsList = _mapper.Map<List<SequentialNewsDto>>(sequentialNews);
            foreach (var sequential in sequentialNewsList)
            {
                List<NewsDto> newsInstance = new List<NewsDto>();
                sequential.News = newsInstance;
                for (int k = 0; k < sequential.Rows.Length; k++)
                {
                    var news = await _newsCollection.Find(p => p.QueueNumber == sequential.Rows[k]).FirstOrDefaultAsync();

                    sequential.News.Add(_mapper.Map<NewsDto>(news));
                }
            }
            return sequentialNewsList;
        }
    }
}
