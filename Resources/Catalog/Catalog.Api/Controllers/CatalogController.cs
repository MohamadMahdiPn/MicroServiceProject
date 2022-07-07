using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Catalog.Api.Entities;
using Catalog.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Catalog.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        #region Constructor

        private readonly ICatalogRepository _catalogRepository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(ICatalogRepository catalogRepository, ILogger<CatalogController> logger)
        {
            _catalogRepository = catalogRepository;
            _logger = logger;
        }

        #endregion

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _catalogRepository.GetProducts();
            return Ok(products);
        }
     
        
        [HttpGet("{Id:length(24)}" , Name = "GetProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType( (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Product>> GetProduct(string id)
        {
            var product = await _catalogRepository.GetProduct(id);
            if (product == null)
            {
                _logger.LogError("product not Found");
                return NotFound();
            }
            return Ok(product);
        }
      
        
        [HttpGet("[action]/{category}")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category)
        {
            var product = await _catalogRepository.GetProductsByCategory(category);
            if (product == null)
            {
                _logger.LogError("product not Found");
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            await _catalogRepository.CreateProduct(product);
            return CreatedAtRoute("GetProduct", new {id = product.Id}, product);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            return Ok(await _catalogRepository.UpdateProduct(product));
        }


        [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            return Ok(await _catalogRepository.DeleteProduct(id));
        }

    }
}
