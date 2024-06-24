using efCoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace efCoreApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly DataContext _dataContext;
        public StudentController(DataContext _context)
        {
            _dataContext = _context;
        }

        public async Task<IActionResult> Index()
        {
            var student = await _dataContext.Students.ToListAsync();
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {   
            if(ModelState.IsValid)
            {
            _dataContext.Students.Add(student);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction("Index");
            }

            return View(student);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var student = await _dataContext.Students.
            Include(m => m.CourseRegister).
            ThenInclude(m => m.Course).
            FirstOrDefaultAsync(m => m.StudentId == id);


            if(student == null)
            {
                return NotFound();
            }

            return View(student);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                try
                {
                    _dataContext.Update(student);
                    await _dataContext.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if(!_dataContext.Students.Any(m => m.StudentId == student.StudentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction("Index");
            }

            return View(student);

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var student = await _dataContext.Students.FirstOrDefaultAsync(m => m.StudentId == id);
            if(student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost]

        public async Task<IActionResult> Delete([FromForm]int id)
        {
            var deletedStudent = await _dataContext.Students.FirstOrDefaultAsync(m => m.StudentId == id);
            if(deletedStudent == null)
            {
                return NotFound();
            }
            _dataContext.Remove(deletedStudent);
            await _dataContext.SaveChangesAsync();

            return RedirectToAction("Index");

        }

    }
}