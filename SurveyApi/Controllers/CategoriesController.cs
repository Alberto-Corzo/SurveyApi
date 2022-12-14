using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveyApi.Data;
using SurveyApi.Dtos.Category;
using SurveyApi.Models;
using SurveyApi.Services.CategoryService;

namespace SurveyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/Categories
        [HttpGet, Authorize(Roles = "Admin, Normal")]
        public async Task<ActionResult<ServiceResponse<List<GetCategoryDto>>>> GetCategory()
        {
            return Ok(await _categoryService.GetAllCategories());
        }

        // GET: api/Categories/5
        [HttpGet("{id}"), Authorize(Roles = "Admin, Normal")]
        public async Task<ActionResult<ServiceResponse<GetCategoryDto>>> GetCategoryById(Guid id)
        {
            var response = await _categoryService.GetCategoryById(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<GetCategoryDto>>> PutCategory(Guid id, UpdateCategoryDto category)
        {
            var response = await _categoryService.UpdateCategory(category, id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<GetCategoryDto>>>> PostCategory(AddCategoryDto category)
        {
            return Ok(await _categoryService.AddCategory(category));
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<GetCategoryDto>>>> DeleteCategory(Guid id)
        {
            var response = await _categoryService.DeleteCategory(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
