using InspimoAjax.DAL;
using Microsoft.AspNetCore.Mvc;
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
            _context.Employees.Add(employee);
            _context.SaveChanges();
            var values = JsonConvert.SerializeObject(employee);
            return Json(values);
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
