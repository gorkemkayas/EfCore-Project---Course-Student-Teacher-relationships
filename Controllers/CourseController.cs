using efCoreApp.Data;
using efCoreApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace efCoreApp.Controllers
{   
    public class CourseController : Controller
    {
            private readonly DataContext _dataContext;
        public CourseController(DataContext _context)
        {
            _dataContext = _context;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _dataContext.Courses.Include(o => o.Teacher).ToListAsync();
            if(courses == null)
            {
                return NotFound();
            }
            return View(courses);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Teachers = new SelectList(await _dataContext.Teachers.ToListAsync(),"TeacherId","TeacherFullName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseViewModel course)
        {
            if (ModelState.IsValid)
            {
                _dataContext.Courses.Add(new Course(){ CourseId = course.CourseId, CourseHeader= course.CourseHeader, TeacherId = course.TeacherId});
                await _dataContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Teachers =new SelectList(await _dataContext.Teachers.ToListAsync(), "TeacherId", "TeacherFullName");

            return View(course);
        }

        
        [HttpGet]

        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Teachers = new SelectList(await _dataContext.Teachers.ToListAsync(),"TeacherId","TeacherFullName");
            
            if(id == null)
            {
                return NotFound();
            }

            var course = await _dataContext.Courses.
            Include(m => m.CourseRegister).
            ThenInclude(m => m.Student).
            Select(k => new CourseViewModel
                        {
                            CourseId = k.CourseId,
                            CourseHeader = k.CourseHeader,
                            TeacherId = k.TeacherId,
                            CourseRegister = k.CourseRegister
                        })
                        .FirstOrDefaultAsync(k => k.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }
        
        
        [HttpPost]

        public async Task<IActionResult> Edit(int? id, CourseViewModel course)
        {
            // if (id != course.CourseId)
            // {
            //     return NotFound();
            // }

            // var _editedCourse = await _dataContext.Courses.FirstOrDefaultAsync(m => m.CourseId == id);

            // if (_editedCourse == null)
            // {
            //     return NotFound();
            // }
            // _editedCourse.CourseHeader = course.CourseHeader;
            // _dataContext.Update(new Course{CourseId = course.CourseId, CourseHeader = course.CourseHeader, TeacherId = course.TeacherId});
            // await _dataContext.SaveChangesAsync();
            // return RedirectToAction("Index");

            if(id != course.CourseId)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                try
                {
                    _dataContext.Update(new Course() { CourseId = course.CourseId, CourseHeader = course.CourseHeader, TeacherId = course.TeacherId});
                    await _dataContext.SaveChangesAsync();
                }
                catch(DbUpdateException)
                {
                    if(!_dataContext.Courses.Any(o => o.CourseId == course.CourseId))
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
            ViewBag.Teachers =new SelectList(await _dataContext.Teachers.ToListAsync(), "TeacherId", "TeacherFullName");
            return View(course);


        }

        [HttpGet]

        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var _course = await _dataContext.Courses.FirstOrDefaultAsync(m => m.CourseId==id);
            if (_course == null)
            {
                return NotFound();
            }

            return View(_course);
        }   


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {

            var _course = await _dataContext.Courses.FirstOrDefaultAsync(m => m.CourseId==id);

            if(_course == null)
            {
                return NotFound();
            }

            _dataContext.Remove(_course);
            await _dataContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        
    
    
    
    }


}