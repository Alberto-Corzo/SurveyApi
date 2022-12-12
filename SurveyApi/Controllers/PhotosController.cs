using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveyApi.Dtos.Photo;
using SurveyApi.Models;
using SurveyApi.Services.PhotoService;

namespace SurveyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IPhotoService _photoService;

        public PhotosController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        // GET: api/Photos
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetPhotoDto>>>> GetPhoto()
        {
            return Ok(await _photoService.GetAllPhotos());
        }

        // GET: api/Photos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetPhotoDto>>> GetPhotoById(Guid id)
        {
            var response = await _photoService.GetPhotoById(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        // PUT: api/Photos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<GetPhotoDto>>> PutPhoto(Guid id, UpdatePhotoDto photo)
        {
            var response = await _photoService.UpdatePhoto(photo, id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        // POST: api/Photos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetPhotoDto>>>> PostPhoto(AddPhotoDto photo)
        {
            return Ok(await _photoService.AddPhoto(photo));
        }

        // DELETE: api/Photos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetPhotoDto>>>> DeletePhoto(Guid id)
        {
            var response = await _photoService.DeletePhoto(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
