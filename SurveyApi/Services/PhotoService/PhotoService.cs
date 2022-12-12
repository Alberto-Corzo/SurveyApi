using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SurveyApi.Data;
using SurveyApi.Dtos.Photo;
using SurveyApi.Models;

namespace SurveyApi.Services.PhotoService
{
    public class PhotoService : IPhotoService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public PhotoService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetPhotoDto>>> AddPhoto(AddPhotoDto newPhoto)
        {
            var response = new ServiceResponse<List<GetPhotoDto>>();

            Photo photo = _mapper.Map<Photo>(newPhoto);
            _context.Photo.Add(photo);

            await _context.SaveChangesAsync();

            response.Data = await _context.Photo.Select(p => _mapper.Map<GetPhotoDto>(p)).ToListAsync();

            return response;
        }

        public async Task<ServiceResponse<List<GetPhotoDto>>> DeletePhoto(Guid id)
        {
            ServiceResponse<List<GetPhotoDto>> response = new ServiceResponse<List<GetPhotoDto>>();
            try
            {
                Photo photo = await _context.Photo
                    .FirstOrDefaultAsync(p => p.IdPhoto.ToString().ToUpper() == id.ToString().ToUpper());

                if (photo != null)
                {
                    _context.Photo.Remove(photo);
                    await _context.SaveChangesAsync();

                    response.Data = _context.Photo.Select(p => _mapper.Map<GetPhotoDto>(p)).ToList();
                }
                else
                {
                    response.Success = false;
                    response.Message = "Photo Not Found";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetPhotoDto>>> GetAllPhotos()
        {
            var response = new ServiceResponse<List<GetPhotoDto>>();

            var photos = await _context.Photo.ToListAsync();

            response.Data = photos.Select(c => _mapper.Map<GetPhotoDto>(c)).ToList();

            return response;
        }

        public async Task<ServiceResponse<GetPhotoDto>> GetPhotoById(Guid id)
        {
            var response = new ServiceResponse<GetPhotoDto>();
            var photo = await _context.Photo
                .FirstOrDefaultAsync(p => p.IdPhoto.ToString().ToUpper() == id.ToString().ToUpper());

            if (photo != null)
            {
                response.Data = _mapper.Map<GetPhotoDto>(photo);
            }
            else
            {
                response.Success = false;
                response.Message = "Photo Not Found";
            }

            return response;
        }

        public async Task<ServiceResponse<GetPhotoDto>> UpdatePhoto(UpdatePhotoDto updatedPhoto, Guid id)
        {
            ServiceResponse<GetPhotoDto> response = new ServiceResponse<GetPhotoDto>();

            try
            {
                var photo = await _context.Photo
                    .FirstOrDefaultAsync(c => c.IdPhoto.ToString().ToUpper() == id.ToString().ToUpper());

                if (photo != null)
                {
                    _mapper.Map(updatedPhoto, photo);

                    await _context.SaveChangesAsync();

                    response.Data = _mapper.Map<GetPhotoDto>(photo);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Photo Not Found";
                }
            }
            catch (DbUpdateException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
