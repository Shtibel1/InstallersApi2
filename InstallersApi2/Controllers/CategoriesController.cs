using BLL.Interfaces;
using BLL.Models;
using DAL.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InstallersApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {

        private readonly ICategoriesService _categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpPost]
        [Authorize(Roles = nameof(Role.Employee))]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryVm categoryVm)
        {
            if (categoryVm == null)
            {
                return BadRequest();
            }

            var createdCategory = await _categoriesService.CreateCategoryAsync(categoryVm);
            return CreatedAtAction(nameof(GetCategory), new { id = createdCategory.Id }, createdCategory);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(Role.Employee))]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = await _categoriesService.GetCategoryAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            await _categoriesService.DeleteCategoryAsync(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(Guid id)
        {
            var category = await _categoriesService.GetCategoryAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoriesService.GetCategoriesAsync();
            return Ok(categories);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = nameof(Role.Employee))]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] CategoryVm categoryVm)
        {
            if (categoryVm == null || categoryVm.Id != id)
            {
                return BadRequest();
            }

            var existingCategory = await _categoriesService.GetCategoryAsync(id);
            if (existingCategory == null)
            {
                return NotFound();
            }

            await _categoriesService.UpdateCategoryAsync(id, categoryVm);
            return NoContent();
        }

    }
}
