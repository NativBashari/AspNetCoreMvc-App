using API_Test.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace API_Test.Controllers
{
    public class SalaryController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Salary> salaries = new List<Salary>();

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://localhost:5143/api/salaries");
            if (response.IsSuccessStatusCode)
            {
                var jstring = await response.Content.ReadAsStringAsync();
                salaries = JsonConvert.DeserializeObject<List<Salary>>(jstring);
                return View(salaries);
            }
            else
                return View(salaries);
        }
        public IActionResult Add()
        {
            Salary salary = new Salary();
            return View(salary);
        }
        [HttpPost]
        public async Task<IActionResult> Add(Salary salary)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                var json = JsonConvert.SerializeObject(salary);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage message = await client.PostAsync(@"http://localhost:5143/api/salaries", content);
                if (message.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Error", "This is an API error");
                    return View(salary);
                }

            }
            else
                return View(salary);
        }


        public async Task<IActionResult> Update(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage message = await client.GetAsync(@"http://localhost:5143/api/salaries/" + id);
            if (message.IsSuccessStatusCode)
            {
                var jstring = await message.Content.ReadAsStringAsync();
                var salary = JsonConvert.DeserializeObject<Salary>(jstring);
                return View(salary);
            }
            else
                return RedirectToAction("Add");
        }
        [HttpPost]
        public async Task<IActionResult> Update(Salary salary)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                var json = JsonConvert.SerializeObject(salary);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage message = await client.PutAsync(@"http://localhost:5143/api/salaries/", content);
                if (message.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                    return View(salary);
            }
            else
                return View(salary);
        }

        public async Task<IActionResult> Delete(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage message = await client.DeleteAsync(@"http://localhost:5143/api/salaries/" + id);
            if (message.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else 
                return View();
        }
    }
}
