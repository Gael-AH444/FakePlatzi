using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FakePlatzi.Models
{
    public class ProductModel
    {
		public int Id { get; set; }
		public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Por favor ingresa una categoría válida.")]
        public int CategoryId { get; set; }
        public List<string> Images { get; set; }

        [Url(ErrorMessage = "Por favor ingrese una URL válida.")]
        public string ImageUrl { get; set; } // Campo auxiliar para la URL de imagen

	}
}
