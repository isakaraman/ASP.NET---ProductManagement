﻿
using LoggerService;
using Microsoft.Extensions.Logging;
using Repository;
using Repository.Models;

namespace Services
{
	public class ProductService : IProductService
	{
		private readonly IRepositoryManager _repositoryManager;
		private readonly ILoggerManager _logger;

        public ProductService(IRepositoryManager repositoryManager,ILoggerManager loggerManager)
        {
			_repositoryManager = repositoryManager;
            _logger = loggerManager;
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

		public IEnumerable<Product> GetAllProducts()
		{
			try
			{
				var products = _repositoryManager.Product.GetAllProducts();
				return products;
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error in {nameof(ProductService)} . {nameof(GetAllProducts)}: {ex}");
				throw;
			}

		}

		public Product GetProductById(Guid productId)
		{
			var product = _repositoryManager.Product.GetProductById(productId);
			return product;
		}

		public Product UpdateProduct(Product product)
		{
			try
			{
				var existingProduct = _repositoryManager.Product.GetProductById(product.ProductGuid);
				if (existingProduct == null)
				{
					throw new Exception($"Product Guid: {product.ProductGuid} is not existed.");
				}

				existingProduct.Title = product.Title;
				existingProduct.Description = product.Description;
				existingProduct.Price = product.Price;
				existingProduct.ActualCost = product.ActualCost;
				existingProduct.Quantity = product.Quantity;
				existingProduct.ProductType = product.ProductType;

				_repositoryManager.Product.UpdateProduct(existingProduct);
				_repositoryManager.Save();

				return existingProduct;
			}
			catch (Exception ex)
			{

				_logger.LogError($"Error in {nameof(ProductService)} . {nameof(UpdateProduct)}: {ex}");
				throw;
			}
		}
	}
}