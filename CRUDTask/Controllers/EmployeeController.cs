using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using CRUDTask.Data;

namespace CRUDTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        IConfiguration _config;
        EmployeeDB _db;
        public EmployeeController(IConfiguration config)
        {
            _config = config;
            _db = new EmployeeDB(_config["ConnectionStrings:CRUDDemo"]);
        }

        [HttpGet(Name ="Index")]
        public IActionResult Index()
        {
            var employees = _db.Employees();
            return Ok(employees);

        }

        [HttpGet("{id}")]
        public IActionResult EmployeeById(int id)
        {
            //var employee = _db.EmployeeByEmployeeId(id);
            //return Ok(employee);

            if (id <= 0)
                return BadRequest("Please enter valid EmployeeId");

            var employee = _db.EmployeeByEmployeeId(id);

            if (employee != null)
                return Ok(employee);

            return NotFound($"{id} category id not found");
        }


        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(EmployeeModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        [HttpPost]
        public IActionResult Create(EmployeeModel category)
        {
            if (ModelState.IsValid)
            {
                try
                {                   
                    _db.Insert(category);
 
                    return Created("Create", category);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
            else
            {
                return BadRequest("Please check input data");
            }
        }


        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        [HttpPut]
        public IActionResult Update(EmployeeModel employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(employee);
                    
                    return Ok(employee);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
            else
            {
                return BadRequest("Please check input data");
            }
        }


        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Please enter valid id");
      
            if (id != null)
            {
                try
                {
                    _db.Delete(id);                  
                    return Ok();
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
            else
            {
                return NotFound($"The given id {id} does not exist.");
            }
        }
    }
}
