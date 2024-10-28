using Microsoft.AspNetCore.Mvc;
using Services;
using Shared.Products;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : Controller
	{
		private readonly IProductService _service;
		public ProductController(IProductService service) => _service = service;

		[HttpGet]
		public IActionResult GetProducts()
		{
			try
			{
				var products = _service.GetAllProducts();
				return Ok(products);
			}
			catch (Exception ex)
			{

				throw new Exception("Internal Server Error");
			}
		}

		[HttpGet("{productGuid:Guid}")]
		public IActionResult GetProductById(Guid productGuid)
		{
			var product =_service.GetProductById(productGuid);
			return Ok(product);
		}

		[HttpPut]
		public IActionResult UpdateProduct(ProductDto product)
		{
			var newProduct = _service.UpdateProduct(product);

			return Ok(newProduct);
		}

		[HttpPost]
		public IActionResult AddProduct(ProductDto product)
		{
			product.ProductGuid = Guid.NewGuid();
			var newProduct = _service.AddProduct(product);
			return Ok();
		}

		[HttpDelete("{productGuid:Guid}")]
		public IActionResult DeleteProduct(Guid productGuid)
		{
			_service.DeleteProduct(productGuid);

			return Ok();
		}

	}
}
