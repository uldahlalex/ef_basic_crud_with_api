using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Api;

[Route("")]
public class ProductController(MyDbContext context) : ControllerBase
{
    
    [HttpGet]
    [Route("api/products")]
    public ActionResult GetProducts()
    {
        return Ok(context.Products.ToList());
    }
    
    [HttpPost]
    [Route("api/products")]
    public ActionResult CreateProduct([FromBody] Product product)
    {
        context.Products.Add(product);
        context.SaveChanges();
        return Ok(product);
    }
    
    [HttpPut]
    [Route("api/products/{id}")]
    public ActionResult UpdateProduct(int id, [FromBody] Product product)
    {
        var existingProduct = context.Products.Find(id);
        if (existingProduct == null)
        {
            return NotFound();
        }
        existingProduct.ProductName = product.ProductName;
        context.SaveChanges();
        return Ok(existingProduct);
    }
    
    [HttpDelete]
    [Route("api/products/{id}")]
    public ActionResult DeleteProduct(int id)
    {
        var existingProduct = context.Products.Find(id);
        if (existingProduct == null)
        {
            return NotFound();
        }
        context.Products.Remove(existingProduct);
        context.SaveChanges();
        return Ok();
    }
}