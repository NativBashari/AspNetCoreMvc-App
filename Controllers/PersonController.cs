using API_Test.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace API_Test.Controllers
{
    public class PersonController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Person> persons = new List<Person>();
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(@"http://localhost:5143/api/persons");
            if (response.IsSuccessStatusCode)
            {
                var jstring = await response.Content.ReadAsStringAsync();
                persons = JsonConvert.DeserializeObject<List<Person>>(jstring);
                return View(persons);
            }

            return View(persons);
        }
        public IActionResult Add()
        {
            Person person = new Person();
            return View(person);
        }
        [HttpPost]
        public async Task<IActionResult> Add(Person person)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                var json = JsonConvert.SerializeObject(person);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage message = await client.PostAsync(@"http://localhost:5143/api/persons", content);
                if (message.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Error", "This is an API error");
                    return View(person);
                }

            }
            else
                return View(person);
        }

        public async Task<IActionResult> Update(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage message = await client.GetAsync(@"http://localhost:5143/api/persons/" + id);
            if (message.IsSuccessStatusCode)
            {
                var jstring = await message.Content.ReadAsStringAsync();
                Person person = JsonConvert.DeserializeObject<Person>(jstring);
                return View(person);
            }
            else
                return RedirectToAction("Add");
        }

        [HttpPost]
        public async Task<IActionResult> Update(Person person)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                var json = JsonConvert.SerializeObject(person);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage message = await client.PutAsync(@"http://localhost:5143/api/persons", content);
                if (message.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                    return View(person);
            }
            else
                return View(person);
        }

        public async Task<IActionResult> Delete(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage message = await client.DeleteAsync(@"http://localhost:5143/api/persons/" + id);
            if (message.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
                return View();
        }
    }

}
