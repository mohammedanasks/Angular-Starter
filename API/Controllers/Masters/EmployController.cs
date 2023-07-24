using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.LeaveManagement;
using API.Dtos.Masters;
using API.Services.Masters;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Masters
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployController : ControllerBase
    {
          private readonly EmployeeServices _service;
          private readonly IServiceScopeFactory _serviceScopeFactory;    
        public EmployController (EmployeeServices service,IServiceScopeFactory  serviceScopeFactory  ){
                _service = service;
                _serviceScopeFactory=serviceScopeFactory;
        }
        [HttpPost("AddEmploy")]
        public async Task<IActionResult>AddEmploy([FromBody]EmployeeDto model){
            var result= await this._service.AddEmploy(model);
            return Ok(result);
        }

        [HttpGet("GetEmploys")]
        public async Task<IActionResult>GetEmploys(){
            var result= await this._service.GetEmploys();
            return Ok(result);
        }

        
        [HttpPost("DevGetEmploys")]
        public async Task<IActionResult>GetEmploys([FromForm] DataSourceLoadOptions loadOptions){
            var result= await this._service.DevGetEmploys(loadOptions);
            return Ok(result);
        }

        [HttpGet("GetEmployNames")]
        public async Task<IActionResult>GetEmployNames(){
            var result = await  this._service.GetEmployNames();
            return Ok(result);
        }
        [HttpDelete("{EmployId}")]
        public async Task<IActionResult>DeleteEmploy([FromRoute]int EmployId){
        var result = await _service.DeleteEmploy(EmployId);
        return Ok(result);
        }
        [HttpGet("{EditId}")]
        public async Task<IActionResult>GetEditEmploy([FromRoute]int EditId){
        var result = await _service.GetEditEmploy(EditId);
        return Ok(result);
        }

        [HttpPost("UpdateEmploy")]
        public async Task<IActionResult>UpdateEmploy([FromBody]EmployeeDto model){
            var result = await this._service.UpdateEmploy(model);
            return Ok(result);
        }


        
        [HttpGet("GetDesignations")]
        public async Task<IActionResult>GetDesignations(){
            var result= await this._service.GetDesignations();
            return Ok(result);
        }



    }   

      
}
