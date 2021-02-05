using Api.Data;
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
    [Route("v1/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;
        public CategoryController(ICategoryRepository categoryRepository, ISubcategoryRepository subcategoryRepository)
        {
            _categoryRepository = categoryRepository;
            _subcategoryRepository = subcategoryRepository;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Category>>> Get()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Category>> Get(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            return category;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Category>> Post(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Subcategory subcategory = new Subcategory();

                    await _categoryRepository.AddAsync(category);

                    subcategory.IdCategory = category.IdCategory;
                    subcategory.Name = "Sem subcategoria";
                    subcategory.DateCreate = DateTime.Now;

                    await _subcategoryRepository.AddAsync(subcategory);

                    return category;
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
        public async Task<ActionResult<Category>> Put(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _categoryRepository.UpdateAsync(category);

                    return category;
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
        public async Task<ActionResult<Category>> Delete(int id)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(id);
                if (category == null)
                    return BadRequest();

                category.Subcategories = null;
                await _categoryRepository.RemoveAsync(category);

                return category;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
