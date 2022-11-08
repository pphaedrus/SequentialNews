

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsCase.Business.Abstract;
using NewsCase.Business.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News.Api.Controllers
{
    [Route("api/v1/veri-seti")]
    [ApiController]
    public class SequentialNewsController : ControllerBase
    {
        private readonly ISequentialNewsService _sequentialNewsService;

        public SequentialNewsController(ISequentialNewsService sequentialNewsService)
        {
            _sequentialNewsService = sequentialNewsService;
        }

        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById(string id)
        {

            var response = await _sequentialNewsService.GetByIdAsync(id);

            if (response is null)
            {
                return BadRequest("Başlık bulunamadı");
            }
            return Ok(response);
        }
        
        [HttpGet("{title}")]
        public async Task<IActionResult> GetByTitle(string title)
        {
            var response = await _sequentialNewsService.GetByTitleAsync(title);
            if(response is null)
            {
                return BadRequest("Başlık bulunamadı");
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SequentialNewsCreateDto sequentialNews)
        {
            var response = await _sequentialNewsService.CreateAsync(sequentialNews);
            if (response)
            {
                return Ok(response);
            }
            return BadRequest("İşlem Başarısız");
        }

        [HttpPut]
        public async Task<IActionResult> Update(SequentialNewsUpdateDto sequentialNews)
        {
            var response = await _sequentialNewsService.UpdateAsync(sequentialNews);
            if (response)
            {
                return Ok(response);
            }
            return BadRequest("İşlem Başarısız");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _sequentialNewsService.DeleteAsync(id);
            if (response)
            {
                return Ok(response);
            }
            return BadRequest("İşlem Başarısız");
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _sequentialNewsService.GetAllAsync();
            return Ok(response);
        }
    }
}
