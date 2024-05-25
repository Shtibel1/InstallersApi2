using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.Entities;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using BLL.Interfaces;
using DAL.Enums;
using BLL.Models;

namespace InstallersApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductsService _productsService;
        
        public ProductsController(ILogger<ProductsController> logger, IProductsService productsService)
        {
            _logger = logger;
            _productsService = productsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductVm>>> GetProducts()
        {
            return Ok(await _productsService.GetProductsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductVm>> GetProduct(Guid id)
        {
            return Ok(await _productsService.GetProductAsync(id));
        }


        [HttpPut("{id}")]
        [Authorize(Roles = nameof(Role.Employee))]
        public async Task<IActionResult> PutProduct(Guid id, ProductVm product)
        {

            return Ok(await _productsService.UpdateProductAsync(id, product));
        }

        
        [HttpPost]
        [Authorize(Roles = nameof(Role.Employee))]
        public async Task<ActionResult<ProductVm>> PostProduct(ProductVm product)
        {
            var newProd = await _productsService.CreateProductAsync(product);
            return CreatedAtAction(nameof(GetProduct),
                new { id = newProd.Id }, newProd);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(Role.Employee))]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await _productsService.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
