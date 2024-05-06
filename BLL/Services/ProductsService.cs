using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IMapper _mapper;
        private readonly IInstallerPricingRepository _installerPricingRepository;

        public ProductsService(IProductsRepository productsRepository, IMapper mapper, IInstallerPricingRepository installerPricingRepository)
        {
            _productsRepository = productsRepository;
            _mapper = mapper;
            _installerPricingRepository = installerPricingRepository;
        }

        public async Task<ProductVm> CreateProductAsync(ProductVm product)
        {
            var entity = _mapper.Map<Product>(product);
            var vm = _mapper.Map<ProductVm>(await _productsRepository.CreateProductAsync(entity));
            return vm;
        }

        public async Task DeleteProductAsync(int id)
        {
            await _productsRepository.DeleteProductAsync(id);
        }

        public async Task<ProductVm> GetProductAsync(int id)
        {
            return _mapper.Map<ProductVm>(await _productsRepository.GetProductAsync(id));
        }

        public async Task<List<ProductVm>> GetProductsAsync()
        {
            return  _mapper.Map<List<ProductVm>>(await _productsRepository.GetProductsAsync());
        }

        public async Task<ProductVm> UpdateProductAsync(int id, ProductVm product)
        {
            var entity = _mapper.Map<Product>(product);
            return _mapper.Map<ProductVm>(await _productsRepository.UpdateProductAsync(id, entity));
        }

        public async Task<SingleProductVm> GetProductWithPrices(int id, Guid installerId, int productId)
        {
            var productVm = await this.GetProductAsync(id);
            var installerPricing = await _installerPricingRepository.GetInstallerPicing(installerId, productId);
            var installerPricingVm = _mapper.Map<InstallerPricingVm>(installerPricing);

            return new SingleProductVm
            {
                Product = productVm,
                installerPricing = installerPricingVm,
            };
        }
    }
}
