
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsCase.Business.Abstract;
using NewsCase.Business.Dtos;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News.Api.Controllers
{
    [Route("api/v1/haberler")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _newsService.GetAllAsync();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _newsService.GetByIdAsync(id);
            if (response is null)
            {
                return BadRequest("Başlık bulunamadı");
            }
            return Ok(response);
        }

        //çoklu haber girişi yapılabilir
        [HttpPost]
        public async Task<IActionResult> CreateMany(IEnumerable<NewsCreateDto> createDtos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Lütfen başlık giriniz");
            }
            var response = await _newsService.CreateManyAsync(createDtos);

            return Ok(response);
        }


        [HttpPut]
        public async Task<IActionResult> Update(NewsUpdateDto updateDto)
        {
            var response = await _newsService.UpdateAsync(updateDto);
            if (response)
            {
                return Ok(response);
            }
            return BadRequest("İşlem Başarısız");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _newsService.DeleteAsync(id);

            if (response)
            {
                return Ok(response);
            }
            return BadRequest("İşlem Başarısız");
        }
    }
}
