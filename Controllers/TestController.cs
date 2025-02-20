using Microsoft.AspNetCore.Mvc;

namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]//http://localhost:5177/api/get-all
    public class TestController : ControllerBase
    {
        private static List<ProductDto> _products = new List<ProductDto> {
             new ProductDto { Id=1,Name="Produyct 1",Price=10.0m},

                new ProductDto { Id=2,Name="Produyct 2",Price=11.0m},

              new ProductDto { Id=3,Name="Produyct 3",Price=12.0m}
        };

        [HttpGet]
        public IResult GetAll()
        {
            var products = _products.ToList();
            return Results.Ok(new
            {
                message = "Lấy dữ liệu thành công",
                data = products
            });//status 200
        }
        [HttpGet("{id}")]
        public IResult GetById(int id)
        {
            var product = _products.FirstOrDefault(s => s.Id == id);
            if (product == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(new
            {
                message = "Lấy dữ liệu thành công",
                data = product
            });//status 200
        }
        [HttpPost]
        public IResult Create([FromBody] ProductDto product)
        {
            var newProduct = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
            _products.Add(product);

            return Results.Ok(new
            {
                message = "Tạo thành công",
                data = newProduct
            });//status 200
        }
        [HttpPut("{id}")]
        public IResult Update([FromRoute] int id, [FromBody] ProductDto dto)
        {
            //Tìm ra sản phẩm
            var product = _products.FirstOrDefault(s => s.Id == id);
            if (product == null)
            {
                return Results.NotFound();
            }

            product.Name = dto.Name;
            product.Price = dto.Price;
            return Results.Ok(new
            {
                message = "Cập nhật dữ liệu thành công",
                data = product
            });//status 200
        }
        [HttpDelete]
        public IResult Delete(int id)
        {
            var product = _products.FirstOrDefault(s => s.Id == id);
            if (product == null)
            {
                return Results.NotFound();
            }
            _products.Remove(product);
            return Results.Ok(new
            {
                message = $"Xoá dữ liệu với sản phẩm có id : {id}",

            });//status 200 
        }

    }
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}