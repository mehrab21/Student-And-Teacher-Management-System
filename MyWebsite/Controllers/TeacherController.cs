using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebsite.Data;
using MyWebsite.Models;

namespace MyWebsite.Controllers
{
    [Authorize]
    public class TeacherController : Controller
    {
        private readonly DatabaseContext dbcontext;

        public TeacherController(DatabaseContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddTeacherViewModel viewModel)
        {
            var teacher = new Teacher()
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                CourseName = viewModel.CourseName,
                Address = viewModel.Address,
            };
            await dbcontext.Teachers.AddAsync(teacher);
            await dbcontext.SaveChangesAsync();

            return RedirectToAction("List","Teacher");
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var teacher = await dbcontext.Teachers.ToListAsync();

            return View(teacher);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var teacher = await dbcontext.Teachers.FindAsync(id);
            return View(teacher);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Teacher viewModel)
        {
            var teacher = await dbcontext.Teachers.FindAsync(viewModel.Id);
            if (teacher is not null)
            {
                viewModel.FirstName = teacher.FirstName;
                viewModel.LastName = teacher.LastName;
                viewModel.Email = teacher.Email;
                viewModel.Phone = teacher.Phone;
                viewModel.CourseName = teacher.CourseName;
                viewModel.Address = teacher.Address;

                await dbcontext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Teacher");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var teacher = await dbcontext.Teachers.FindAsync(id);
            if (teacher != null)
            {
                dbcontext.Teachers.Remove(teacher);
                await dbcontext.SaveChangesAsync();
            }
            return View("List", "Teacher");
        }
    }
}
