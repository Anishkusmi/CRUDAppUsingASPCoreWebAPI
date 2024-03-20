﻿using CRUDAppUsingASPCoreWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace CRUDAppUsingASPCoreWebAPI.Controllers
{
    public class StudentController : Controller
    {
        private string url = "https://localhost:7218/api/StudentAPI/";
        private HttpClient client = new HttpClient();
        [HttpGet]
        public IActionResult Index()
        {
            List<Student> students = new List<Student>();
            HttpResponseMessage respone = client.GetAsync(url).Result;
            if (respone.IsSuccessStatusCode) 
            {
                string result = respone.Content.ReadAsStringAsync().Result;
                //var data = JsonConvert.DeserializeObject<Student>(result);
                var data = JsonConvert.DeserializeObject<List<Student>>(result);
                if (data != null )
                {
                    students = data;
                }
            }
            return View(students);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Student std)
        {
            var data = JsonConvert.SerializeObject(std);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage respone = client.PostAsync(url, content).Result;
            if (respone.IsSuccessStatusCode)
            {
                TempData["success"] = "Student Added...";
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Student std = new Student();
            HttpResponseMessage response = client.GetAsync(url + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Student>(result);
                if (data != null )
                {
                    std = data;
                }
            }
            return View(std);
        }
        [HttpPost]
        public IActionResult Edit(Student std)
        {
            var data = JsonConvert.SerializeObject(std);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage respone = client.PutAsync(url + std.id, content).Result;
            if (respone.IsSuccessStatusCode)
            {
                TempData["success"] = "Student Updated...";
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            Student std = new Student();
            HttpResponseMessage response = client.GetAsync(url + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Student>(result);
                if (data != null)
                {
                    std = data;
                }
            }
            return View(std);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Student std = new Student();
            HttpResponseMessage response = client.GetAsync(url + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Student>(result);
                if (data != null)
                {
                    std = data;
                }
            }
            return View(std);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            HttpResponseMessage response = client.DeleteAsync(url + id).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["success"] = "Student Successfully Deleted...";
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
