using Microsoft.AspNetCore.Mvc;
using Services;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : Controller
	{
		private readonly IProductService _service;
		public ProductController(IProductService service) => _service = service;

		[HttpGet("getall")]
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
	}
}
