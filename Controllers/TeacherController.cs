using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using efCoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace efCoreApp.Controllers
{

    public class TeacherController : Controller
    {
        private readonly DataContext _dataContext;

        public TeacherController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IActionResult> Index()
        {
            var teacher = await _dataContext.Teachers.ToListAsync();
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]

        public async Task<IActionResult> Create(Teacher teacher)
        {
            
            if(ModelState.IsValid)
            {
                _dataContext.Teachers.Add(teacher);
                await _dataContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(teacher);

        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var teacher = await _dataContext.Teachers.
            FirstOrDefaultAsync(m => m.TeacherId == id);


            if(teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Teacher teacher)
        {
            if (id != teacher.TeacherId)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                try
                {
                    _dataContext.Update(teacher);
                    await _dataContext.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if(!_dataContext.Teachers.Any(m => m.TeacherId == teacher.TeacherId))
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

            return View(teacher);

        }
    }
}