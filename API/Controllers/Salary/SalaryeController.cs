using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.SalaryManagement;
using API.Services.Salary;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Salary
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalaryController : ControllerBase
    {

            private readonly SalaryServices _service;
 
        public SalaryController  (SalaryServices  service){
                _service = service;

        }

        [HttpPost("AddSalary")]
        public async Task<IActionResult>AddSalary([FromBody]SalaryPaymentDto model){
            var result= await this._service.AddSalary(model);
            return Ok(result);
        }



        [HttpDelete("{SalaryId}")]
        public async Task<IActionResult>DeleteSalary([FromRoute]int SalaryId){
        var result = await _service.DeleteSalary(SalaryId);
        return Ok(result);
        }

         [HttpGet("GetEmploys")]
        public async Task<IActionResult>GetEmploys(){
            var result= await this._service.GetEmploys();
            return Ok(result);
        }

          [HttpGet("{EmpId}")]
        public async Task<IActionResult>GetEmploysForPayment([FromRoute]int EmpId){
            var result= await this._service.GetEmploysForPayment(EmpId);
            return Ok(result);
        }

      

    }
}