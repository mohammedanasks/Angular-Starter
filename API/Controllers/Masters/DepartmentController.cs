using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.Masters;
using API.Services.Masters;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Masters
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
          private readonly DepartmentServices  _service;
    
        public DepartmentController(DepartmentServices service){

              _service = service;
        }
          
        [HttpPost("AddDepartment")]
        public async Task<IActionResult>AddEmploy([FromBody]DepartmentDto Dto){    
        var Result = await _service.AddDepartment(Dto);
        return Ok(Result);
        
        }

        [HttpGet("GetDepartments")]
        public async Task<IActionResult>GetDepartments(){
            var result=await _service.GetDepartments();
            return Ok(result);
        }
        [HttpDelete("{DepartmentId}")]
        public async Task<IActionResult>DeleteDepartment([FromRoute]int DepartmentId){
        var result = await _service.DeleteDepartment(DepartmentId);
        return Ok(result);
        }

        [HttpGet("{EditId}")]
        public async Task<IActionResult>GetEditDepartment([FromRoute]int EditId){
        var result = await _service.GetEditDepartment(EditId);
        return Ok(result);
        }

        [HttpPost("UpdateDepartment")]
        public async Task<IActionResult>UpdateDepartment([FromBody]DepartmentDto model){
            var result = await this._service.UpdateDepartment(model);
            return Ok(result);
        }
    }
}