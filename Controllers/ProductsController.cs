using ArmyStore.Dtos;
using ArmyStore.Entities;
using ArmyStore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ArmyStore.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ProductsController : Controller
    {
        private readonly IRepository<Product> _repository;
        private readonly IProductAssembler _assembler;

        public ProductsController(
            IRepository<Product> repository,
            IProductAssembler assembler)
        {
            _repository = repository;
            _assembler = assembler;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] string searchTerm)
        {
            var products = await _repository.GetAll(searchTerm);
            if (products == null)
                return NotFound();

            return Ok(_assembler.WriteDtos(products));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetById([FromRoute] long id)
        {
            if (id < 0)
                return BadRequest("Invalid value of Id.");

            var product = await _repository.GetById(id);
            if (product == null)
                return NotFound();

            return Ok(_assembler.WriteDto(product));
        }

        [HttpOptions]
        [Route("{id}")]
        public ActionResult Options([FromRoute] int id)
        {
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] InsertProductDto dto)
        {
            var entity = _assembler.CreateEntity(dto);
            await _repository.Create(entity, false);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete([FromRoute] long id)
        {
            if (id < 0)
                return BadRequest("Invalid value of Id.");

            var product = await _repository.GetById(id);
            if (product == null)
                return NotFound();

            await _repository.Delete(id);

            return Accepted();
        }
    }
}