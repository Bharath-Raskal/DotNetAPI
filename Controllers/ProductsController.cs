using Microsoft.AspNetCore.Mvc;
using sampleAPI.Models;
using sampleAPI.Services;

namespace sampleAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;


    public ProductsController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Product>> Get() => Ok(_service.GetAll());

    [HttpGet("{id}")]
    public ActionResult<Product> Get(int id)
    {
        var product = _service.GetById(id);
        return product is null ? NotFound() : Ok(product);
    }

    [HttpPost]
    public ActionResult<Product> Post(Product product)
    {
        var created = _service.Create(product);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Product product)
    {
        if (id != product.Id)
        {
            return BadRequest();
        }

        if (!_service.Update(product))
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return _service.Delete(id) ? NoContent() : NotFound();
    }
}
