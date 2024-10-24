﻿
using AutoMapper;
using LoggerService;
using Microsoft.Extensions.Logging;
using Repository;
using Repository.Models;
using Shared.Products;
using System.Reflection.Metadata.Ecma335;

namespace Services
{
	public class ProductService : IProductService
	{
		private readonly IRepositoryManager _repositoryManager;
		private readonly ILoggerManager _logger;
		private readonly IMapper _mapper;

        public ProductService(IRepositoryManager repositoryManager,ILoggerManager loggerManager, IMapper mapper)
        {
			_repositoryManager = repositoryManager;
            _logger = loggerManager;
			_mapper = mapper;
        }

		public Product AddProduct(Product product)
		{
			try
			{
				var newProduct = _repositoryManager.Product.AddProduct(product);
				_repositoryManager.Save();
				return newProduct;
			}
			catch(Exception ex) 
			{
				_logger.LogError($"Error in {nameof(ProductService)} . {nameof(AddProduct)}: {ex}");
				throw;
			}

		}

		public void Delete(Guid productId)
		{
			try
			{
				var productToDelete = _repositoryManager.Product.GetProductById(productId);
				if(productToDelete == null)
				{
					throw new Exception($"Product Guid: {productId} is not existed");

				}
				_repositoryManager.Product.DeleteProduct(productToDelete);
				_repositoryManager.Save();
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error in {nameof(ProductService)} . {nameof(Delete)}: {ex}");
				throw;
			}
		}

		public IEnumerable<ProductDto> GetAllProducts()
		{
			try
			{
				var products = _repositoryManager.Product.GetAllProducts();

				if (products == null)
				{
					return null;
				}

				var productsDto =  _mapper.Map<IEnumerable<ProductDto>>(products);

				return productsDto;
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error in {nameof(ProductService)} . {nameof(GetAllProducts)}: {ex}");
				throw;
			}

		}

		public ProductDto GetProductById(Guid productId)
		{
			var product = _repositoryManager.Product.GetProductById(productId);
			var productDto = _mapper.Map<ProductDto>(product);
			return productDto;
		}

		public ProductDto UpdateProduct(ProductDto product)
		{
			try
			{
				var existingProduct = _repositoryManager.Product.GetProductById(product.ProductGuid);

				if (existingProduct is null)
				{
					throw new Exception($"Product Guid: {product.ProductGuid} is not existed.");
				}

				existingProduct.Title = product.Title;
				existingProduct.Description = product.Description;
				existingProduct.Price = product.Price;
				existingProduct.Quantity = product.Quantity;
				existingProduct.ProductTypeId = product.ProductTypeId;

				_repositoryManager.Product.UpdateProduct(existingProduct);
				_repositoryManager.Save();

				product = GetProductById(product.ProductGuid);
				return product;
			}
			catch (Exception ex)
			{

				_logger.LogError($"Error in {nameof(ProductService)} . {nameof(UpdateProduct)}: {ex}");
				throw;
			}
		}
	}
}
