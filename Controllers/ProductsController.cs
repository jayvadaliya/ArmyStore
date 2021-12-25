using ArmyStore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ArmyStore.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _repository;
        
        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var products = await _repository.GetList();
            if (products == null)
                return NotFound();

            return Ok(products);
        }
    }
}