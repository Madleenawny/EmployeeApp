using EmpWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmpWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //api/Employee
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDbContext EmployeeDbContext;

        public EmployeeController(EmployeeDbContext _employeeDbContext)
        {
            EmployeeDbContext = _employeeDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployee()
        {
            List<Employee> emps = EmployeeDbContext.Employees.ToList();
            return Ok(emps);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee newEmp)
        {
            if(ModelState.IsValid) 
            {
                EmployeeDbContext.Employees.Add(newEmp);
                EmployeeDbContext.SaveChanges();
                string url = "http://localhost:52701/api/Employee/" + newEmp.Id;
                return Created(url,newEmp);
            } 
            return BadRequest(ModelState);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, [FromBody] Employee newEmp)
        {
            if(ModelState!=null)
            {
                Employee oldEmp = EmployeeDbContext.Employees.FirstOrDefault(d=>d.Id==id);
                oldEmp.Name=newEmp.Name;
                oldEmp.MobileNo=newEmp.MobileNo;
                oldEmp.EmailID=newEmp.EmailID;
                EmployeeDbContext.SaveChanges();
                return StatusCode(StatusCodes.Status204NoContent, "Data Saved");
            }
            return BadRequest("Data not valid");
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteEmployee( Guid id)
        {
            Employee oldEmp = EmployeeDbContext.Employees.FirstOrDefault(d => d.Id == id);
            EmployeeDbContext.Employees.Remove(oldEmp);
            EmployeeDbContext.SaveChanges();
            return StatusCode(StatusCodes.Status204NoContent, "Data Saved");

        }
    }
}
