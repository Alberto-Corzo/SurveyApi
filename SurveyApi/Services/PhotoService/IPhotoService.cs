using SurveyApi.Dtos.Photo;
using SurveyApi.Models;

namespace SurveyApi.Services.PhotoService
{
    public interface IPhotoService
    {
        Task<ServiceResponse<List<GetPhotoDto>>> GetAllPhotos();

        Task<ServiceResponse<GetPhotoDto>> GetPhotoById(Guid id);

        Task<ServiceResponse<List<GetPhotoDto>>> AddPhoto(AddPhotoDto newPhoto);

        Task<ServiceResponse<GetPhotoDto>> UpdatePhoto(UpdatePhotoDto updatedPhoto, Guid id);

        Task<ServiceResponse<List<GetPhotoDto>>> DeletePhoto(Guid id);
    }
}
