using FakePlatzi.Models;
using FakePlatzi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FakePlatzi.Controllers
{
	public class ProductController : Controller
	{

		private readonly IProductService _productService;
		private readonly IWebHostEnvironment _webHostEnvironment;

		public ProductController(IProductService productService, IWebHostEnvironment webHostEnvironment)
		{
			_productService = productService;
			_webHostEnvironment = webHostEnvironment;
		}


		//VISTA INDEX
		[HttpGet]
		public async Task<IActionResult> Index(string searchQuery)
		{
			var products = await _productService.GetProductsAsync();

			if (!string.IsNullOrEmpty(searchQuery))
			{
				products = products
					.Where(p => p.Title.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
					.ToList();
			}

			return View(products);
		}



		//VISTA CREATE
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(ProductModel newProduct)
		{
			if (ModelState.IsValid)
			{
				var createdProduct = await _productService.CreateProductAsync(newProduct);
				if (createdProduct != null)
				{
					// Pasamos el producto creado a la vista
					TempData["CreatedProductId"] = createdProduct.Id;
					TempData["CreatedProductName"] = createdProduct.Title;
					return RedirectToAction("Index");
				}
				ModelState.AddModelError(string.Empty, "Error al crear el producto.");
			}

			return View(newProduct);
		}



		//VISTA EDIT
		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			// Obtén el producto utilizando el servicio
			var product = await _productService.GetProductByIdAsync(id);

			if (product == null)
			{
				return NotFound(); // Retorna 404 si el producto no existe
			}
			return View(product);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(ProductModel updatedProduct)
		{
			if (ModelState.IsValid)
			{
				// Llama al servicio para actualizar el producto
				var success = await _productService.UpdateProductAsync(updatedProduct);

				if (success)
				{
					return RedirectToAction("Index");
				}
				ModelState.AddModelError(string.Empty, "Error al actualizar el producto.");
			}
			return View(updatedProduct);
		}



		//VISTA DELETE
		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			var product = await _productService.GetProductByIdAsync(id);
			if (product == null)
			{
				return NotFound();
			}
			return View(product);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var success = await _productService.DeleteProductAsync(id);
			if (success)
			{
				return RedirectToAction("Index");
			}
			ModelState.AddModelError(string.Empty, "Error al eliminar el producto.");
			return View();
		}


		//GET PRODUCT BY ID
		[HttpGet]
		public async Task<IActionResult> GetProductById(int id)
		{
			var product = await _productService.GetProductByIdAsync(id);
			if (product == null)
			{
				return NotFound();
			}
			return Json(product);
		}
	}
}
