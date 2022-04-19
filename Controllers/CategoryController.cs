using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewNewProject.Filters;
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
        [ValidateModel]
        [MyException]
        public IActionResult Post([FromBody]CategoryModel model)
        {
            ServiceResponse<Category> response = new ServiceResponse<Category>();

            
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
            
         //   else
         //   {
         //       response.Errors = ModelState.Values.SelectMany(m => m.Errors) //ModelState bütün hataları almak için; Values'e ulaşıldı
                                                                              //Errors'a ulaşıyourz, oradan ErrorMessage'a ulaşıp oradan bütün bilgileri oraya response.Errors'a ekledik      
         //           .Select(e => e.ErrorMessage).ToList();
        //        response.HasError = true;
        //        return BadRequest(response);
        //    }

        
          
        }
        
        // PUT: api/Category/5
        [HttpPut("{id}")]
        [ValidateModel]
        [MyException]
        public IActionResult Put(int id, [FromBody]CategoryModel model)
        {
            ServiceResponse<Category> response = new ServiceResponse<Category>();

         
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
           ServiceResponse<Category> response = new ServiceResponse<Category>();

            response.Entity = context.Categories.Find(id);
            if(response.Entity == null)
            {
                response.Errors.Add("İd'ye ait veri bulunmadı");
                response.HasError = true;
                return NotFound(response);
            }
            context.Categories.Remove(response.Entity);
            context.SaveChanges();

            response.IsSuccessFull = true;
            return Ok(response);
        }
    }
}
