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
        var newProduct = new Product { Id = id, ProductName = product.ProductName };
        context.Products.Update(newProduct);
        context.SaveChanges();
        return Ok(newProduct);
        
    }
    
    [HttpDelete]
    [Route("api/products/{id}")]
    public ActionResult DeleteProduct(int id)
    {
        var result = context.Products
            .Where(p => p.Id == id)
            .ExecuteDelete();
        if(result == 0)
        {
            return NotFound();
        }
        return Ok();
    }
}