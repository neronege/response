using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewNewProject.Models;

namespace NewNewProject.Controllers
{
    [Produces("application/json")]
    [Route("api/Category")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext context;

        public CategoryController(AppDbContext context)
        {
            this.context = context;
        }


        // GET: api/Category
        [HttpGet]
        public IActionResult Get()
        {
            ServiceResponse<Category> response = new ServiceResponse<Category>(); //ServiceResponse T'yerine category'i belirterek sınıfından response objesi oluşturyoruz
            response.Entities = context.Categories.ToList(); //Category listemizdekileri serviceResponse Entites'e eklemiş oluyoruz
            response.IsSuccessFull = true;
            return Ok(response);
        }

        // GET: api/Category/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult GetById(int id)
        {
            ServiceResponse<Category> response = new ServiceResponse<Category>(); //nesne oluşturma

            response.Entity = context.Categories.Find(id);
            if(response.Entity == null)
            {
                response.Errors.Add("Id'ye ait veri bulunamadı");
                response.HasError = true;
                return NotFound(response);
            }
            response.IsSuccessFull = true;
            return Ok(response);
        }

        // POST: api/Category
        [HttpPost]
        public IActionResult Post([FromBody]CategoryModel model)
        {
            ServiceResponse<Category> response = new ServiceResponse<Category>();
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    Title = model.Title,
                    Description = model.Description
                };
                context.Categories.Add(category);
                context.SaveChanges();

                response.Entity = category; //response.Entity kapsıyoruz
                response.IsSuccessFull = true;

                return Ok(response);
            }
            else
            {
                response.Errors = ModelState.Values.SelectMany(m => m.Errors) //ModelState bütün hataları almak için; Values'e ulaşıldı
                                                                              //Errors'a ulaşıyourz, oradan ErrorMessage'a ulaşıp oradan bütün bilgileri oraya response.Errors'a ekledik      
                    .Select(e => e.ErrorMessage).ToList();
                response.HasError = true;
                return BadRequest(response);
            }

        
          
        }
        
        // PUT: api/Category/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]CategoryModel model)
        {
            ServiceResponse<Category> response = new ServiceResponse<Category>();

            if (model==null || model.Id != id)
            {
                response.HasError = true;
                response.Errors.Add("Tutarsız id'ler");

                return BadRequest(response);
            }
            response.Entity = context.Categories.Find(id);
            if (response.Entity == null)
            {
                response.Errors.Add("Id'ye ait veri bulunamadı");
                response.HasError = true;
                return NotFound(response);
            }

            response.Entity.Title = model.Title;
            response.Entity.Description = model.Description;
            response.Entity.ModifiedOn = DateTime.Now;

            context.Categories.Update(response.Entity);
            context.SaveChanges();
            response.IsSuccessFull = true;

            return Ok(response);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = context.Categories.Find(id);
            if(item==null)
            {
                return NotFound();
            }
            context.Categories.Remove(item);
            context.SaveChanges();

            return Ok();
        }
    }
}
