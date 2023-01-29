using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ToDoItems.Dao;
using ToDoItems.Models;

namespace ToDoItems.Controllers
{
    public class PersonController : Controller
    {

        private static readonly ICosmosDbService service = CosmosDbServiceProvider.CosmosDbService;

        // GET: Person
        public async Task<ActionResult> Index()
        {
            return View(await service.GetPeopleAsync("SELECT * FROM Person"));
        }

        // GET: Person/Details/5
        public async Task<ActionResult> Details(string id) => await ShowPerson(id);
        // GET: Person/Edit/5
        public async Task<ActionResult> Edit(string id) => await ShowPerson(id);
        // GET: Person/Delete/5
        public async Task<ActionResult> Delete(string id) => await ShowPerson(id);

        
        // GET: Person/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Person/Create
        [HttpPost]
        public async Task<ActionResult> Create(Person person)
        {
            if (ModelState.IsValid)
            {
                person.Id = Guid.NewGuid().ToString();
                await service.AddPersonAsync(person);
                return RedirectToAction("Index");
            }

            return View(person);
        }

        // POST: Person/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(Person person)
        {
            if (ModelState.IsValid)
            {
                await service.UpdatePersonAsync(person);
                return RedirectToAction("Index");
            }
            return View(person);
        }

        // POST: Person/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(Person person)
        {
            await service.DeletePersonAsync(person);
            return RedirectToAction("Index");
        }

        //Show Person 
        private async Task<ActionResult> ShowPerson(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var person = await service.GetPersonAsync(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }
    }
}
