using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Repository.Data;

namespace Talabat.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BugsController : ControllerBase
    {
        private readonly StoreContext _dbContext;

        public BugsController(StoreContext dbContext)
        {
           _dbContext = dbContext;
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var product = _dbContext.Products.Find(100);
            if(product == null)
            {
                return NotFound( new APIResponse(404));
            }
            return Ok(product);
        }


        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var product = _dbContext.Products.Find(100);
            var productToReturn = product.ToString(); //NullRefereceException

            return Ok(productToReturn);


        }

        [HttpGet("badrequest")]
        public ActionResult GetBadRequestError()
        {

            return BadRequest();
        
        
        }

        [HttpGet("badrequest/{id}")] //h5aleh yb3t string

        public ActionResult GetBadRequestById(int id) //Validation Error
        {

          return Ok(id);

        }



    }
}
