using DapperMvc_Demo.Data.Repository;
using DapperMVC_Demo.Data.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace DapperMVC_Demo.UI.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonReposository _personReop;
        public PersonController(IPersonReposository personReop)
        {
            _personReop = personReop;
        }
        public async Task<IActionResult> DisplayAll()
        {
            var people = await _personReop.GetAllAsync();
            return View(people);
        }
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Person person)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return View(person);

                }
                bool addPersonResult= await _personReop.AddAsync(person);
                if (addPersonResult) { TempData["msg"] = "Succesfully Added"; }
                
                return View();
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Error While Editing";
            }
            return RedirectToAction(nameof(Add));
            
        }

        public async Task<IActionResult> Delete(int id)
        {
            var deleteResult = await _personReop.DeleteAsync(id);
            return RedirectToAction(nameof(DisplayAll));
        }


        public async Task<IActionResult> Edit(int id)
        {
            var person=await _personReop.GetByIdAsync(id);
            if(person == null) { return NotFound(); }
            return View(person);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Person person)
        {
            try
            {
                if(!ModelState.IsValid) 
                { 
                    return View(person); 
                }
                var updateResult=await _personReop.UpdateAsync(person);
                if (updateResult) 
                { 
                    TempData["msg"] = "Update Succesfully";
                    return RedirectToAction(nameof(DisplayAll));
                }
                else 
                { 
                    TempData["msg"] = "Could Not Update";
                    return View(person);
                }

            }
            catch (Exception ex)
            {
                TempData["msg"] = "Could Not Update";
                return View(person);
            }
            return View();
        }
        //[HttpPost]
        //public async Task<IActionResult> Get(Person person)
        //{
        //    return View();
        //}


    }
}
