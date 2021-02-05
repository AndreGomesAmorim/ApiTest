using Api.Models;
using Api.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("v1/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Product>>> Get()
        {
            var prodducts = await _productRepository.GetAllAsync();
            return prodducts;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var prodduct = await _productRepository.GetByIdAsync(id);
            if (prodduct == null)
                return NotFound();

            return prodduct;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Product>> Post(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _productRepository.AddAsync(product);
                    return product;
                }

                return BadRequest();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut]
        [Route("")]
        public async Task<ActionResult<Product>> Put(Product product)
        {
           try
            {
                if (ModelState.IsValid)
                {
                    await _productRepository.UpdateAsync(product);

                    return product;
                }

                return BadRequest();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<Product>> Delete(int id)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(id);
                if (product == null)
                    return BadRequest();

                product.Subcategory = null;
                await _productRepository.RemoveAsync(product);

                return product;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
