using CRUDTaskClient.Entities;
using CRUDTaskClient.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CRUDTaskClient.Controllers
{
    public class EmployeeController : Controller
    {

        HttpClient client;
        string baseAddress;
        IConfiguration _configuration;
        IConfiguration _config;
        CRUDTaskContext _context;

        public EmployeeController(IConfiguration configuration, IConfiguration config, CRUDTaskContext context)
        {

            this.client = new HttpClient(); 
            this._configuration = configuration;
            baseAddress = _configuration["ApiAddress"];
            this.client.BaseAddress = new Uri(baseAddress);

            _config = config;
            _context = context;
        }

        public IActionResult Index()
        {
            string result =
               client.GetStringAsync(baseAddress + "Employee").Result;

            List<EmployeeModel> employees =
                JsonSerializer.Deserialize<List<EmployeeModel>>(result);

            return View(employees);
        }

        public IActionResult Details(int id)
        {
            string result = client.GetStringAsync(baseAddress + $"Employee/{id}").Result;

            EmployeeModel employee = JsonSerializer.Deserialize<EmployeeModel>(result);

            return View(employee);
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogIn(UserDetail details)
        {
            var user = _context.UserDetails.Find(details.Id);
            
            if(user.UserName == details.UserName && user.Password == details.Password)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("InvalidUser");
        }

        public IActionResult InvalidUser()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeModel employee)
        {
            string data = JsonSerializer.Serialize(employee);
            StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

        
            HttpResponseMessage response = client.PostAsync(baseAddress+"Employee",content).Result;

            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            string result = client.GetStringAsync(baseAddress + $"Employee/{id}").Result;

            EmployeeModel employee = JsonSerializer.Deserialize<EmployeeModel>(result);

            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeModel employee)
        {
            string data = JsonSerializer.Serialize(employee);
            StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PutAsync(baseAddress + "Employee", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();

        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            string result = client.GetStringAsync(baseAddress + $"Employee/{id}").Result;

            EmployeeModel employee = JsonSerializer.Deserialize<EmployeeModel>(result);

            return View(employee);
        }

        [HttpPost,ActionName ("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            HttpResponseMessage result = client.DeleteAsync(baseAddress + $"Employee?id={id}").Result;

            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
