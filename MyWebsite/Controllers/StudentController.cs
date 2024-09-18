using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebsite.Data;
using MyWebsite.Models;

namespace MyWebsite.Controllers
{
    [Authorize] 
    public class StudentController : Controller
    {
        private readonly DatabaseContext dbcontext;

        public StudentController(DatabaseContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel)
        {
            var student = new Student
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                GroupName = viewModel.GroupName,
                Address = viewModel.Address,
                Hobby = viewModel.Hobby,
            };
            await dbcontext.Students.AddAsync(student);
            await dbcontext.SaveChangesAsync();


            return RedirectToAction("List","Student");
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
           var student = await dbcontext.Students.ToListAsync();
            
            return View(student);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var student = await dbcontext.Students.FindAsync(id);
            return View(student);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(Student viewModel)
        {
            var student = await dbcontext.Students.FindAsync(viewModel.Id);
            if(student is not null)
            {
                student.FirstName = viewModel.FirstName;
                student.LastName = viewModel.LastName;
                student.Email = viewModel.Email;
                student.Phone = viewModel.Phone;
                student.GroupName = viewModel.GroupName;
                student.Address = viewModel.Address;
                student.Hobby = viewModel.Hobby;

                await dbcontext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Student");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await dbcontext.Students.FindAsync(id);
            if (student is not null)
            {
                dbcontext.Students.Remove(student);
                await dbcontext.SaveChangesAsync();
            }
            return RedirectToAction("List","Student");
        }
    }
}
