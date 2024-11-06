using FakePlatzi.Models;

namespace FakePlatzi.Services
{
	public interface IProductService
	{
		Task<ProductModel> CreateProductAsync(ProductModel newProduct);
		Task<bool> DeleteProductAsync(int id);
		Task<ProductModel> GetProductByIdAsync(int id);
		Task<List<ProductModel>> GetProductsAsync();
		Task<bool> UpdateProductAsync(ProductModel updatedProduct);
	}
}
