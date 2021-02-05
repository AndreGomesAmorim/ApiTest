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
    [Route("v1/subcategory")]
    public class SubcategoryController : ControllerBase
    {
        private readonly ISubcategoryRepository _subcategoryRepository;
        public SubcategoryController(ISubcategoryRepository subcategoryRepository)
        {
            _subcategoryRepository = subcategoryRepository;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Subcategory>>> Get()
        {
            var subcategories = await _subcategoryRepository.GetAllAsync();
            return subcategories;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Subcategory>> Get(int id)
        {
            var subcategory = await _subcategoryRepository.GetByIdAsync(id);
            if (subcategory == null)
                return NotFound();

            return subcategory;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Subcategory>> Post(Subcategory subcategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _subcategoryRepository.AddAsync(subcategory);
                    return subcategory;
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
        public async Task<ActionResult<Subcategory>> Put(Subcategory  subcategory)
        {
           try
            {
                if (ModelState.IsValid)
                {
                    await _subcategoryRepository.UpdateAsync(subcategory);

                    return subcategory;
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
        public async Task<ActionResult<Subcategory>> Delete(int id)
        {
            try
            {
                var subcategory = await _subcategoryRepository.GetByIdAsync(id);
                if (subcategory == null)
                    return BadRequest();

                subcategory.Category = null;
                subcategory.Products = null;
                await _subcategoryRepository.RemoveAsync(subcategory);

                return subcategory;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
