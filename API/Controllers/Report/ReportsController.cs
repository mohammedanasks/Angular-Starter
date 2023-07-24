using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.IdTextDto;
using API.Dtos.Masters;
using API.Dtos.Reports;
using API.Dtos.SalaryManagement;
using API.Services.Report;
using Microsoft.AspNetCore.Mvc;
using UI.src.app.dtos.Report;

namespace API.Controllers.Report
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
            private readonly ReportsService _service;
 
        public ReportsController  (ReportsService  service){
                _service = service;

        }


        
    [HttpGet("GetSalaryList")]
        public async Task<IActionResult>GetSalaryList(){
        var result= await this._service.GetSalaryList();
            return Ok(result);
        }

             
    [HttpGet("{month}")]
        public async Task<IActionResult>GetSearchList([FromRoute]int month){
        var result= await this._service.GetSearchList(month);
            return Ok(result);
        }


       [HttpGet("GetMonthlyReport")]
        public async Task<IActionResult>GetMonthlyReport(){
        var result= await this._service.GetMonthlyReport();
            return Ok(result);
        }
           
       
       [HttpGet("GetYearlyReport")]
        public async Task<IActionResult>GetYearlyReport(){
        var result= await this._service.GetYearlyReport();
            return Ok(result);
        }

           [HttpGet("GetLeaveReport")]
        public async Task<IActionResult>GetLeaveReport(){
        var result= await this._service.GetLeaveReport();
            return Ok(result);
        }

     
        [HttpPost("SearchDepartmentLeaveReport")]
        public async Task<IActionResult>SearchDepartmentLeaveReport([FromBody]IdTextDto Dto){
        var result= await this._service.SearchDepartmentLeaveReport(Dto);
            return Ok(result);
        }

        
     
             
                  
        
    }
}