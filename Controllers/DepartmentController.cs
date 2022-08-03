using API_Test.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace API_Test.Controllers
{
    public class DepartmentController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Department> departmentList = new List<Department>();
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(@"http://localhost:5143/api/departments");
            if (response.IsSuccessStatusCode)
            {
                var jstring = await response.Content.ReadAsStringAsync();
                departmentList = JsonConvert.DeserializeObject<List<Department>>(jstring);
                return View(departmentList);
            }


            return View(departmentList);
        }

        public IActionResult Add()
        {
            Department department = new Department();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Department department)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                var jsonDepartment = JsonConvert.SerializeObject(department);
                StringContent stringContent = new StringContent(jsonDepartment, Encoding.UTF8, "application/json");
                HttpResponseMessage message = await client.PostAsync(@"http://localhost:5143/api/departments", stringContent);
                if (message.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Error", "This is an API error");
                    return View(department);
                }
            }
            else
                return View(department);
        }

        public async Task<IActionResult> Update(int Id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage message = await client.GetAsync(@"http://localhost:5143/api/departments/" + Id);
            if (message.IsSuccessStatusCode)
            {
                var jstring = await  message.Content.ReadAsStringAsync();
                var department = JsonConvert.DeserializeObject<Department>(jstring);
                return View(department);
            }
            return RedirectToAction("Add");
        }
        [HttpPost]
        public async Task<IActionResult> Update(Department department)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                var json = JsonConvert.SerializeObject(department);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage message = await client.PutAsync(@"http://localhost:5143/api/departments", content);
                if (message.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                    return View(department);
            }
            else
                return View(department);
        }

        public async Task<IActionResult> Delete(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage message = await client.DeleteAsync(@"http://localhost:5143/api/departments/" + id);
            if (message.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
                return View();
        }

    }
}
