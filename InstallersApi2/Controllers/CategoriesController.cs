using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using BLL.Interfaces;
using DAL.Enums;

namespace InstallersApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return Ok(await _categoriesService.GetCategoriesAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(Guid id)
        {
            return Ok(await _categoriesService.GetCategoryAsync(id));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = nameof(Role.Employee))]
        public async Task<IActionResult> PutCategory(Guid id, Category category)
        {
            return Ok(await _categoriesService.UpdateCategoryAsync(id, category));
        }

        [HttpPost]
        [Authorize(Roles = nameof(Role.Employee))]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {

            var newCat = await _categoriesService.CreateCategoryAsync(category);
            return CreatedAtAction(nameof(_categoriesService.GetCategoryAsync),
                new { Id = newCat.Id }, newCat);

        }

        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(Role.Employee))]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            await _categoriesService.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}
