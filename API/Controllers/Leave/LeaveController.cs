using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Services.Masters;
using API.Services.Leaves;
using API.Dtos.LeaveManagement;
using API.Dtos.Masters;

namespace API.Controllers.Leave
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaveController : ControllerBase
    {

        
          private readonly LeaveServices _service;
 
        public LeaveController (LeaveServices service){
                _service = service;

        }

          
        [HttpPost("AddLeave")]
        public async Task<IActionResult>AddEmploy([FromBody]LeaveDto model){
            var result= await this._service.AddLeave(model);
            return Ok(result);
        }


        [HttpGet("GetLeaves")]
        public async Task<IActionResult>GetLeaves(){
            var result= await this._service.GetLeaves();
            return Ok(result);
        }

        [HttpDelete("{LeaveId}")]
        public async Task<IActionResult>DeleteLeave([FromRoute]int LeaveId){
        var result = await _service.DeleteLeave(LeaveId);
        return Ok(result);
        }

        [HttpGet("{EditId}")]
        public async Task<IActionResult>GetEditLeave([FromRoute]int EditId){
        var result = await _service.GetEditLeave(EditId);
        return Ok(result);
        }

         [HttpPost("UpdateLeave")]
        public async Task<IActionResult>UpdateLeave([FromBody]LeaveDto model){
            var result = await this._service.UpdateLeave(model);
            return Ok(result);
        }

        
        [HttpGet("GetLeaveType")]
        public async Task<IActionResult>GetLeaveType(){
            var result= await this._service.GetLeavesTypes();
            return Ok(result);
        }

        
        [HttpPost("AddLeaveType")]
        public async Task<IActionResult>AddLeaveType([FromBody]LeaveTypeDto model){
            var result= await this._service.AddLeaveType(model);
            return Ok(result);
        }
    }
}

