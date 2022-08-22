using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.DAL;

namespace Talabat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    
    public class BuggyController : ControllerBase
    {
        private readonly StoreContext context;

        public BuggyController(StoreContext context) 
        {
            this.context = context;
        }
        
        public ActionResult GetServerError()
        {
            var product = context.products.Find(100);
            //var productToReturn = product.ToString();
            if(product == null) return NotFound();

            return Ok(product);
        }
    }
}
