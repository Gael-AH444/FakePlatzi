using FakePlatzi.Models;

namespace FakePlatzi.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("FakeAPI");
        }

        public async Task<List<ProductModel>> GetProductsAsync()
        {
            var response = await _httpClient.GetAsync("products");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<ProductModel>>();
        }

		public async Task<ProductModel> CreateProductAsync(ProductModel newProduct)
		{
			if (!string.IsNullOrEmpty(newProduct.ImageUrl))
			{
				newProduct.Images = new List<string> { newProduct.ImageUrl };
			}

			var productData = new
			{
				title = newProduct.Title,
				price = newProduct.Price,
				description = newProduct.Description,
				categoryId = newProduct.CategoryId,
				images = newProduct.Images
			};

			var response = await _httpClient.PostAsJsonAsync("products", productData);

			if (response.IsSuccessStatusCode)
			{
				// Retornar el producto creado con el ID de la respuesta
				return await response.Content.ReadFromJsonAsync<ProductModel>();
			}

			return null;
		}

		public async Task<ProductModel> GetProductByIdAsync(int id)
        {
            // Realiza la solicitud GET a la API para obtener el producto por ID
            var response = await _httpClient.GetAsync($"products/{id}");

            if (response.IsSuccessStatusCode)
            {
                // Si la respuesta es exitosa, deserializa el contenido JSON en un objeto ProductModel
                return await response.Content.ReadFromJsonAsync<ProductModel>();
            }
            else
            {
                return null;
            }
        }


        public async Task<bool> UpdateProductAsync(ProductModel updatedProduct)
        {
            if (!string.IsNullOrEmpty(updatedProduct.ImageUrl))
            {
                updatedProduct.Images = new List<string> { updatedProduct.ImageUrl };
            }

            var productData = new
            {
                title = updatedProduct.Title,
                price = updatedProduct.Price,
                description = updatedProduct.Description,
                categoryId = updatedProduct.CategoryId,
                images = updatedProduct.Images 
            };

            var response = await _httpClient.PutAsJsonAsync($"products/{updatedProduct.Id}", productData);
            return response.IsSuccessStatusCode;
        }


        public async Task<bool> DeleteProductAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"products/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
