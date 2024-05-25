using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Repositories;

namespace BLL.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IMapper _mapper;
        private readonly IServiceProviderPricingRepository _installerPricingRepository;

        public ProductsService(IProductsRepository productsRepository, IMapper mapper, IServiceProviderPricingRepository installerPricingRepository)
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

        public async Task DeleteProductAsync(Guid id)
        {
            await _productsRepository.DeleteProductAsync(id);
        }

        public async Task<ProductVm> GetProductAsync(Guid id)
        {
            return _mapper.Map<ProductVm>(await _productsRepository.GetProductAsync(id));
        }

        public async Task<List<ProductVm>> GetProductsAsync()
        {
            return  _mapper.Map<List<ProductVm>>(await _productsRepository.GetProductsAsync());
        }

        public async Task<ProductVm> UpdateProductAsync(Guid id, ProductVm product)
        {
            var entity = _mapper.Map<Product>(product);
            return _mapper.Map<ProductVm>(await _productsRepository.UpdateProductAsync(id, entity));
        }

        public async Task<SingleProductVm> GetProductWithPrices(Guid id, Guid installerId, Guid productId)
        {
            var productVm = await this.GetProductAsync(id);
            var installerPricing = await _installerPricingRepository.GetServiceProviderPicing(installerId, productId);
            var installerPricingVm = _mapper.Map<ServiceProviderPricingVm>(installerPricing);

            return new SingleProductVm
            {
                Product = productVm,
                installerPricing = installerPricingVm,
            };
        }
    }
}
