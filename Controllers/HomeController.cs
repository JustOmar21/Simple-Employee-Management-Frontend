using Frontend.Models;
using Frontend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;

namespace Frontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeService service;
        public HomeController(IEmployeeService service)
        {
            this.service = service;
        }
        public async Task<IActionResult> Index()
        {
            List<Employee> employees;
            try
            {
                employees = await service.GetEmployees();
            }
            catch (Exception ex)
            {
                employees = [];
                TempData["Error"] = "Couldn't successfully retrieve employees's data";
            }
            //TempData["Error"] = "Couldn't successfully retrieve employees's data";
            //TempData["Added"] = "Employee Added";
            //TempData["Updated"] = "Employee Updated";
            //TempData["Deleted"] = "Employee Deleted";
            return View(employees);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return View("Add", employee);
            }
            try
            {
                employee.id = null;
                await service.Add(employee);
                TempData["Added"] = "Employee Added";
            }
            catch(Exception ex)
            {
                TempData["Error"] = $"{ex.Message}, Failed to Add Employee";
            }
            
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int Id)
        {
            try
            {
                var employee = await service.GetById(Id);
                EmployeeUpdate employeeUpdate = new EmployeeUpdate()
                {
                    id = (int)employee.id,
                    name = employee.name,
                    gender = employee.gender,
                    is1stAppointment = employee.is1stAppointment,
                    notes = employee.notes,
                    role = employee.role
                };
                Console.WriteLine($"{employee.startDate}");
                ViewBag.Date = employee.startDate.ToString("yyyy-MM-dd");
                return View("Edit", employeeUpdate);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                TempData["Error"] = "Failed to Get Employee Data";
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeUpdate employee)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", employee);
            }
            try
            {
                await service.Update(employee);
                TempData["Updated"] = "Employee Updated";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"{ex.Message}, Failed to Update Employee";
            }

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                await service.Delete(Id);
                TempData["Deleted"] = "Employee Deleted";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"{ex.Message}, Failed to Delete Employee";
            }
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
