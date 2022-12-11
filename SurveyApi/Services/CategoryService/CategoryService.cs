using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SurveyApi.Data;
using SurveyApi.Dtos.AuthRepo;
using SurveyApi.Dtos.Category;
using SurveyApi.Models;

namespace SurveyApi.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CategoryService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetCategoryDto>>> AddCategory(AddCategoryDto newCategory)
        {
            var response = new ServiceResponse<List<GetCategoryDto>>();

            Category cat = _mapper.Map<Category>(newCategory);
            _context.Category.Add(cat);

            await _context.SaveChangesAsync();

            response.Data = await _context.Category.Select(c => _mapper.Map<GetCategoryDto>(c)).ToListAsync();

            return response;
        }

        public async Task<ServiceResponse<List<GetCategoryDto>>> DeleteCategory(Guid id)
        {
            ServiceResponse<List<GetCategoryDto>> response = new ServiceResponse<List<GetCategoryDto>>();
            try
            {
                Category cat = await _context.Category
                    .FirstOrDefaultAsync(u => u.IdCategory.ToString().ToUpper() == id.ToString().ToUpper());

                if (cat != null)
                {
                    _context.Category.Remove(cat);
                    await _context.SaveChangesAsync();

                    response.Data = _context.Category.Select(c => _mapper.Map<GetCategoryDto>(c)).ToList();
                }
                else
                {
                    response.Success = false;
                    response.Message = "Category Not Found";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetCategoryDto>>> GetAllCategories()
        {
            var response = new ServiceResponse<List<GetCategoryDto>>();

            var categories = await _context.Category.ToListAsync();

            response.Data = categories.Select(c => _mapper.Map<GetCategoryDto>(c)).ToList();

            return response;
        }

        public async Task<ServiceResponse<GetCategoryDto>> GetCategoryById(Guid id)
        {
            var response = new ServiceResponse<GetCategoryDto>();
            var cat = await _context.Category
                .FirstOrDefaultAsync(c => c.IdCategory.ToString().ToUpper() == id.ToString().ToUpper());

            if (cat != null)
            {
                response.Data = _mapper.Map<GetCategoryDto>(cat);
            }
            else
            {
                response.Success = false;
                response.Message = "Category Not Found";
            }

            return response;
        }

        public async Task<ServiceResponse<GetCategoryDto>> UpdateCategory(UpdateCategoryDto updatedCategory, Guid id)
        {
            ServiceResponse<GetCategoryDto> response = new ServiceResponse<GetCategoryDto>();

            try
            {
                var cat = await _context.Category
                    .FirstOrDefaultAsync(c => c.IdCategory.ToString().ToUpper() == id.ToString().ToUpper());

                if (cat != null)
                {
                    _mapper.Map(updatedCategory, cat);

                    await _context.SaveChangesAsync();

                    response.Data = _mapper.Map<GetCategoryDto>(cat);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Category Not Found";
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
