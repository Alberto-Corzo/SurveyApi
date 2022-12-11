using SurveyApi.Dtos.Category;
using SurveyApi.Models;

namespace SurveyApi.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<ServiceResponse<List<GetCategoryDto>>> GetAllCategories();

        Task<ServiceResponse<GetCategoryDto>> GetCategoryById(Guid id);

        Task<ServiceResponse<List<GetCategoryDto>>> AddCategory(AddCategoryDto newCategory);

        Task<ServiceResponse<GetCategoryDto>> UpdateCategory(UpdateCategoryDto updatedCategory, Guid id);

        Task<ServiceResponse<List<GetCategoryDto>>> DeleteCategory(Guid id);
    }
}
