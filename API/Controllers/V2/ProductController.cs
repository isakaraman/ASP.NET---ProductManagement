using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Services;
using Shared.Paging;
using Shared.Products;

namespace API.Controllers.V2
{
	[ApiVersion("2.0")]
	[Route("api/v{v:apiversion}/[controller]")]
	[ApiController]
	public class ProductController : Controller
	{
		private readonly IProductService _service;
		public ProductController(IProductService service) => _service = service;



		[HttpGet]
		public async Task<IActionResult> GetProductsAsync([FromQuery] RequestParameter requestParameter)
		{
			try
			{
				var products = await _service.GetAllProductsAsync(requestParameter);
				return Ok(products);
			}
			catch (Exception ex)
			{

				throw new Exception("Internal Server Error");
			}
		}

		[HttpGet("{productGuid:Guid}")]
		public async Task<IActionResult> GetProductByIdAsync(Guid productGuid)
		{
			int versionNumber = 2;
			var product = await _service.GetProductByIdAsync(productGuid, versionNumber);
			return Ok(product);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateProductAsync(ProductDto product)
		{
			var newProduct =await _service.UpdateProductAsync(product);

			return Ok(newProduct);
		}

		[HttpPost]
		public async Task<IActionResult> AddProduct(ProductDto product)
		{
			product.ProductGuid = Guid.NewGuid();
			var newProduct = await _service.AddProductAsync(product);
			return Ok();
		}

		[HttpDelete("{productGuid:Guid}")]
		public async Task<IActionResult> DeleteProductAsync(Guid productGuid)
		{
			await _service.DeleteProductAsync(productGuid);

			return Ok();
		}

	}
}
