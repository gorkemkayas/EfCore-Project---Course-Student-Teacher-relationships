using efCoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace efCoreApp.Controllers
{
    public class CourseRegisterController : Controller
    {
        private readonly DataContext _dataContext;
        public CourseRegisterController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]

        public async Task<IActionResult> Index()
        {   
            var courseRegisters = await _dataContext.CourseRegisters.Include(m => m.Course).
            Include(m => m.Student).
            ToListAsync();
            return View(courseRegisters);
        }

        [HttpGet]
        public async Task<IActionResult> CreateRegister()
        {
            ViewBag.Courses = new SelectList(await _dataContext.Courses.ToListAsync(),"CourseId","CourseHeader");
            ViewBag.Students = new SelectList(await _dataContext.Students.ToListAsync(),"StudentId","StudentFullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRegister(CourseRegister courseRegister)
        {
            if(courseRegister == null)
            {
                return NotFound();
            }
            if((courseRegister.CourseId != 0) && (courseRegister.StudentId != 0))
            {
            courseRegister.RegisterDate = DateTime.Now;
            await _dataContext.CourseRegisters.AddAsync(courseRegister);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction("Index");
            }
            
            ViewBag.Courses = new SelectList(await _dataContext.Courses.ToListAsync(),"CourseId","CourseHeader");
            ViewBag.Students = new SelectList(await _dataContext.Students.ToListAsync(),"StudentId","StudentFullName");
            return View(courseRegister);
        }

        [HttpGet]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var courseRegister = await _dataContext.CourseRegisters.FirstOrDefaultAsync(m => m.RegisterId == id);
            if (courseRegister == null)
            {
                return NotFound();
            }
            
            return View(courseRegister);

        }


        [HttpPost]
        public async Task<IActionResult> Delete(int? id, CourseRegister _courseRegister)
        {
            if (id != _courseRegister.RegisterId)
            {
                return NotFound();
            }
            var courseRegister = await _dataContext.CourseRegisters.FirstOrDefaultAsync(m => m.RegisterId == _courseRegister.RegisterId);
            if (courseRegister == null)
            {
                return NotFound();
            }
            _dataContext.Remove(courseRegister);
            await _dataContext.SaveChangesAsync();
            
            return RedirectToAction("Index");

        }
    }
}