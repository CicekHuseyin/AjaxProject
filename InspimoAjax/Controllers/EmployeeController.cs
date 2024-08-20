using InspimoAjax.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace InspimoAjax.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly Context _context;

        public EmployeeController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult EmployeeList()
        {
            var values = JsonConvert.SerializeObject(_context.Employees.ToList());
            return Json(values);
        }
        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
            try
            {
                _context.Employees.Add(employee);
                _context.SaveChanges();
                var values = JsonConvert.SerializeObject(employee);
                return Json(values);
            }
            catch (DbUpdateException ex)
            {

                var innerException = ex.InnerException?.Message;
                Console.WriteLine(innerException);

                // Hata mesajını JSON formatında döndürün
                return Json(new { error = innerException });
            }
            catch (Exception ex)
            {
                // Diğer olası hataları yakalayın ve loglayın
                Console.WriteLine($"General Exception: {ex.Message}");
                return Json(new { error = ex.Message });
            }

        }
        public IActionResult DeleteEmployee(int id)
        {
            var values = _context.Employees.Find(id);
            _context.Employees.Remove(values);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
