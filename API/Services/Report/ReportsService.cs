using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.BaseEntity.ResponseModel;
using API.Dtos.IdTextDto;
using API.Dtos.LeaveManagement;
using API.Dtos.Reports;
using API.Dtos.SalaryManagement;
using API.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UI.src.app.dtos.Report;

namespace API.Services.Report
{
    public class ReportsService
    {

        private readonly DataContext _DataContext;
        public IMapper _Mapper { get; }

        public ReportsService(DataContext DataContext, IMapper Mapper)
        {
            _Mapper = Mapper;
            _DataContext = DataContext;
        }


        public async Task<ResponseModel<DepartmentLeaveReportDto>> GetLeaveReport()
        {
            ResponseModel<DepartmentLeaveReportDto> response = new ResponseModel<DepartmentLeaveReportDto>();
            try
            {
                var res = await _DataContext.Employees.Include(x => x.Department).Include(x =>
                x.Designation).Select(emp => new DepartmentLeaveReportDto()
                {
                    DepartmentName = emp.Department.Name,
                    EmployeeId = emp.Id,
                    EmployName=emp.LastName,
                    DepartmentId=emp.DepartmentId
                    
                }).ToListAsync();
                int Days = DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month);
                var LastDay = new DateTime(DateTime.Today.Year, DateTime.Today.Month, Days);
                var FirstDay = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

                var LeaveData = _DataContext.Leaves.Include(x => x.LeaveType).Where(x =>
                  (x.FromDate.Date < LastDay.Date && x.ToDate > FirstDay)).ToList();

                var SalaryData = _DataContext.SalaryPayments.Include(x => x.Employee).ToList();

                var DepartmentData=_DataContext.Departments.ToList();


                var groupedCustomerList = res
                .GroupBy(u => u.DepartmentName)
                .Select(grp => grp.ToList())
                .ToList();
                
                groupedCustomerList.ForEach(dt=>{

                       double leaveCount = 0;
                        double LeaveCount = 0;
                        int EmployCount = 0;
                        int salary = 0;
                        int tax = 0;
                        int count = 0;

                foreach(var x in dt){                    

                    var leaves = LeaveData
                   .Where(lv => lv.EmployeeId == x.EmployeeId).ToList();

                    var sal = SalaryData
                    .Where(s => s.EmployeeId == x.EmployeeId).ToList();

                    var departments=DepartmentData
                    .Where(dep=>dep.Id==x.DepartmentId).ToList();

                foreach(var dep in departments){  
        
                if(sal.Count!=0){
                       sal.ForEach(s =>
                        {
                            salary = (int)(salary + s.NetAmount);
                            x.Netamount = salary;
                            tax = (int)(tax + s.tax);
                            x.TotalTax = tax;
                        });
                }
                         x.Netamount = salary;
                         x.TotalTax = tax;

                        if (count>0)
                        {
                            res.Remove(res.FirstOrDefault(s => s.DepartmentName == dep.Name));
                        }
                        count++;
                        EmployCount++;
                        x.EmployCount = EmployCount;
                        x.LeaveCount = (int?)LeaveCount;
                        foreach (var leave in leaves)
                        {
                            var tmFrom = leave.FromDate.Date;
                            var tmTo = leave.ToDate.Date;
                            if (leave.FromDate.Date < FirstDay.Date)
                            {
                                tmFrom = FirstDay.Date;
                            }
                            if (leave.ToDate.Date > LastDay.Date)
                            {
                                tmTo = LastDay.Date;
                             }
                                leaveCount += (tmTo - tmFrom).TotalDays;
                            if (leave.LeaveType.IsPaid == false)
                                {
                                LeaveCount += (tmTo - tmFrom).TotalDays;
                                x.LeaveCount = (int?)LeaveCount;
                                }
                            }

                       } 
                       
                    }  
                  
               
        
                });

                  
                  response.Items = res;

                
            }
            catch (System.Exception Exception)
            {
                throw;
            }
            return response;
        }


             public async Task<ResponseModel<SearchDepartmentLeaveReportDto>> SearchDepartmentLeaveReport(IdTextDto Dto)
        {
            ResponseModel<SearchDepartmentLeaveReportDto> response = new ResponseModel<SearchDepartmentLeaveReportDto>();
            try
            {
                var res = await _DataContext.Employees.Where(x => x.Department.Id==Dto.Id).Include(x =>
                x.Designation).Select(emp => new SearchDepartmentLeaveReportDto()
                {
                    EmployName = emp.FirstName + " " + emp.LastName,
                    Designation = emp.Designation.Name,
                    EmployeeId = emp.Id,
                     DepartmentName = emp.Department.Name,
                     DepartmentId=emp.Department.Id                  
               


                }).ToListAsync();
                int Days = DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month);
                var LastDay = new DateTime(DateTime.Today.Year, DateTime.Today.Month, Days);
                var FirstDay = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

                var LeaveData = _DataContext.Leaves.Include(x => x.LeaveType).Where(x =>
                  (x.FromDate.Date < LastDay.Date && x.ToDate > FirstDay)).ToList();


                      
                res.ForEach(x =>
                {

                    double leaveCount = 0;
                    double paidLeaveCount = 0;

                    var leaves = LeaveData.Where(lv => lv.EmployeeId == x.EmployeeId ).ToList();
                    if(x.DepartmentId==Dto.Id)
                    {
                       
                        foreach (var leave in leaves)
                        {
                         
                            var tmFrom = leave.FromDate.Date;
                            var tmTo = leave.ToDate.Date;
                            if (leave.FromDate.Date < FirstDay.Date)
                            {
                                tmFrom = FirstDay.Date;
                            }
                            if (leave.ToDate.Date > LastDay.Date)
                            {
                                tmTo = LastDay.Date;
                            }
                            leaveCount += (tmTo - tmFrom).TotalDays;
                            x.Leave= (int?)leaveCount;
                            
                        }
                    }
                });
                
                    response.Items = res;
            }
            catch (System.Exception Exception)
            {
                throw;
            }
            return response;
        }


        public async Task<ResponseModel<SalaryPaymentDto>> GetSalaryList()
        {
            ResponseModel<SalaryPaymentDto> response = new ResponseModel<SalaryPaymentDto>();
            try
            {
                var model = await _DataContext.SalaryPayments.Include(x => x.Employee).ToListAsync();

                var res = _Mapper.Map<List<SalaryPaymentDto>>(model);
                var amount = Convert.ToDouble(0);
                res.ForEach(x =>
                {
                    var net = Convert.ToInt32(x.NetAmount);
                    amount = (double)(amount + net);
                    x.Total = amount;
                });                                                     
                response.Items = res;
            }
            catch (System.Exception Exception)
            {
                throw;
            }
            return response;
        }


        public async Task<ResponseModel<SalaryPaymentDto>> GetSearchList(int month)
        {
            ResponseModel<SalaryPaymentDto> response = new ResponseModel<SalaryPaymentDto>();
            try
            {

                var model = await _DataContext.SalaryPayments.Where(x => x.Date.Month == month).Include(x => x.Employee).ToListAsync();
                var res = _Mapper.Map<List<SalaryPaymentDto>>(model);
                var amount = Convert.ToDouble(0);
                res.ForEach(x =>
                {
                    int dte = x.Date.Month;
                    var net = Convert.ToInt32(x.NetAmount);
                    amount = (double)(amount + net);
                    x.Total = amount;

                });
                response.Items = res;
            }
            catch (System.Exception Exception)
            {
                throw;
            }
            return response;
        }

        public async Task<ResponseModel<YearlyReportDto>> GetYearlyReport()
        {
            ResponseModel<YearlyReportDto> response = new ResponseModel<YearlyReportDto>();
            try
            {

                var res = await _DataContext.SalaryPayments
                .Select(rep => new YearlyReportDto
                {
                    TotalSalary = rep.NetAmount,
                    Leave = (int)rep.Leave,
                    tax = rep.tax,
                    Deduction = rep.Deduction,
                    Date = rep.Date
                }).ToListAsync();

                var amount12 = Convert.ToDouble(0);
                var Reduction12 = Convert.ToDouble(0);
                var Tax12 = 0;
                var Leave12 = 0;
                int monthcount12 = 0;


                var amount11 = Convert.ToDouble(0);
                var Reduction11 = Convert.ToDouble(0);
                var Tax11 = 0;
                var Leave11 = 0;
                int monthcount11 = 0;


                var amount10 = Convert.ToDouble(0);
                var Reduction10 = Convert.ToDouble(0);
                var Tax10 = 0;
                var Leave10 = 0;
                int monthcount10 = 0;

                var amount9 = Convert.ToDouble(0);
                var Reduction9 = Convert.ToDouble(0);
                var Tax9 = 0;
                var Leave9 = 0;
                int monthcount9 = 0;

                var amount8 = Convert.ToDouble(0);
                var Reduction8 = Convert.ToDouble(0);
                var Tax8 = 0;
                var Leave8 = 0;
                int monthcount8 = 0;

                var amount7 = Convert.ToDouble(0);
                var Reduction7 = Convert.ToDouble(0);
                var Tax7 = 0;
                var Leave7 = 0;
                int monthcount7 = 0;

                var amount6 = Convert.ToDouble(0);
                var Reduction6 = Convert.ToDouble(0);
                var Tax6 = 0;
                var Leave6 = 0;
                int monthcount6 = 0;

                var amount5 = Convert.ToDouble(0);
                var Reduction5 = Convert.ToDouble(0);
                var Tax5 = 0;
                var Leave5 = 0;
                int monthcount5 = 0;

                var amount4 = Convert.ToDouble(0);
                var Reduction4 = Convert.ToDouble(0);
                var Tax4 = 0;
                var Leave4 = 0;
                int monthcount4 = 0;

                var amount3 = Convert.ToDouble(0);
                var Reduction3 = Convert.ToDouble(0);
                var Tax3 = 0;
                var Leave3 = 0;
                int monthcount3 = 0;

                res.ToList().ForEach(x =>
                {
                    var year = x.Date.Year;
                    if (year == 2022)
                    {

                        // Show(month);
                        if (monthcount12 > 0)
                        {
                            res.Remove(res.Single(s => s.Year == 2022));
                        }
                        monthcount12++;
                        x.Year = 2022;
                        var net = Convert.ToInt32(x.TotalSalary);
                        amount12 = (double)(amount12 + net);
                        x.TotalSalary = amount12;

                        var dedu = Convert.ToInt32(x.Deduction);
                        Reduction12 = (double)(Reduction12 + dedu);
                        x.Deduction = Reduction12;

                        var tax = Convert.ToInt32(x.tax);
                        Tax12 = Tax12 + tax;
                        x.tax = Tax12;

                        var leave = Convert.ToInt32(x.Leave);
                        Leave12 = Leave12 + leave;
                        x.Leave = Leave12;
                    }

                    if (year == 2023)
                    {
                        if (monthcount11 > 0)
                        {
                            res.Remove(res.Single(s => s.Year == 2023));
                        }
                        monthcount11++;
                        x.Year = 2023;
                        var net = Convert.ToInt32(x.TotalSalary);
                        amount11 = (double)(amount11 + net);
                        x.TotalSalary = amount11;

                        var dedu = Convert.ToInt32(x.Deduction);
                        Reduction11 = (double)(Reduction11 + dedu);
                        x.Deduction = Reduction11;

                        var tax = Convert.ToInt32(x.tax);
                        Tax11 = Tax11 + tax;
                        x.tax = Tax11;

                        var leave = Convert.ToInt32(x.Leave);
                        Leave11 = Leave11 + leave;
                        x.Leave = Leave11;
                    }
                    if (year == 2024)
                    {
                        if (monthcount10 > 0)
                        {
                            res.Remove(res.Single(s => s.Year == 2024));
                        }
                        monthcount10++;
                        x.Year = 2024;
                        var net = Convert.ToInt32(x.TotalSalary);
                        amount10 = (double)(amount10 + net);
                        x.TotalSalary = amount10;

                        var dedu = Convert.ToInt32(x.Deduction);
                        Reduction10 = (double)(Reduction10 + dedu);
                        x.Deduction = Reduction10;

                        var tax = Convert.ToInt32(x.tax);
                        Tax10 = Tax10 + tax;
                        x.tax = Tax10;

                        var leave = Convert.ToInt32(x.Leave);
                        Leave10 = Leave10 + leave;
                        x.Leave = Leave10;
                    }
                    if (year == 2025)
                    {
                        if (monthcount9 > 0)
                        {
                            res.Remove(res.Single(s => s.Year == 2025));
                        }
                        monthcount9++;
                        x.Year = 2025;
                        var net = Convert.ToInt32(x.TotalSalary);
                        amount9 = (double)(amount9 + net);
                        x.TotalSalary = amount9;

                        var dedu = Convert.ToInt32(x.Deduction);
                        Reduction9 = (double)(Reduction9 + dedu);
                        x.Deduction = Reduction11;

                        var tax = Convert.ToInt32(x.tax);
                        Tax9 = Tax9 + tax;
                        x.tax = Tax9;

                        var leave = Convert.ToInt32(x.Leave);
                        Leave9 = Leave9 + leave;
                        x.Leave = Leave9;
                    }
                    if (year == 2026)
                    {
                        if (monthcount8 > 0)
                        {
                            res.Remove(res.Single(s => s.Year == 2026));
                        }
                        monthcount8++;
                        x.Year = 2026;
                        var net = Convert.ToInt32(x.TotalSalary);
                        amount8 = (double)(amount8 + net);
                        x.TotalSalary = amount11;

                        var dedu = Convert.ToInt32(x.Deduction);
                        Reduction8 = (double)(Reduction8 + dedu);
                        x.Deduction = Reduction8;

                        var tax = Convert.ToInt32(x.tax);
                        Tax8 = Tax8 + tax;
                        x.tax = Tax8;

                        var leave = Convert.ToInt32(x.Leave);
                        Leave8 = Leave8 + leave;
                        x.Leave = Leave8;
                    }
                    if (year == 2027)
                    {
                        if (monthcount7 > 0)
                        {
                            res.Remove(res.Single(s => s.Year == 2027));
                        }
                        monthcount7++;
                        x.Year = 2027;
                        var net = Convert.ToInt32(x.TotalSalary);
                        amount7 = (double)(amount7 + net);
                        x.TotalSalary = amount7;

                        var dedu = Convert.ToInt32(x.Deduction);
                        Reduction7 = (double)(Reduction7 + dedu);
                        x.Deduction = Reduction7;

                        var tax = Convert.ToInt32(x.tax);
                        Tax7 = Tax7 + tax;
                        x.tax = Tax7;

                        var leave = Convert.ToInt32(x.Leave);
                        Leave7 = Leave7 + leave;
                        x.Leave = Leave7;
                    }
                    if (year == 2028)
                    {
                        if (monthcount6 > 0)
                        {
                            res.Remove(res.Single(s => s.Year == 2028));
                        }
                        monthcount6++;
                        x.Year = 2028;
                        var net = Convert.ToInt32(x.TotalSalary);
                        amount6 = (double)(amount6 + net);
                        x.TotalSalary = amount11;

                        var dedu = Convert.ToInt32(x.Deduction);
                        Reduction6 = (double)(Reduction6 + dedu);
                        x.Deduction = Reduction6;

                        var tax = Convert.ToInt32(x.tax);
                        Tax6 = Tax6 + tax;
                        x.tax = Tax6;

                        var leave = Convert.ToInt32(x.Leave);
                        Leave6 = Leave6 + leave;
                        x.Leave = Leave6;
                    }
                    if (year == 2029)
                    {
                        if (monthcount5 > 0)
                        {
                            res.Remove(res.Single(s => s.Year == 2029));
                        }
                        monthcount5++;
                        x.Year = 2029;
                        var net = Convert.ToInt32(x.TotalSalary);
                        amount5 = (double)(amount5 + net);
                        x.TotalSalary = amount5;

                        var dedu = Convert.ToInt32(x.Deduction);
                        Reduction5 = (double)(Reduction5 + dedu);
                        x.Deduction = Reduction11;

                        var tax = Convert.ToInt32(x.tax);
                        Tax11 = Tax5 + tax;
                        x.tax = Tax5;

                        var leave = Convert.ToInt32(x.Leave);
                        Leave5 = Leave5 + leave;
                        x.Leave = Leave5;
                    }
                    if (year == 2030)
                    {
                        if (monthcount4 > 0)
                        {
                            res.Remove(res.Single(s => s.Year == 2030));
                        }
                        monthcount4++;
                        x.Year = 2030;
                        var net = Convert.ToInt32(x.TotalSalary);
                        amount4 = (double)(amount4 + net);
                        x.TotalSalary = amount4;

                        var dedu = Convert.ToInt32(x.Deduction);
                        Reduction4 = (double)(Reduction4 + dedu);
                        x.Deduction = Reduction11;

                        var tax = Convert.ToInt32(x.tax);
                        Tax4 = Tax4 + tax;
                        x.tax = Tax4;

                        var leave = Convert.ToInt32(x.Leave);
                        Leave4 = Leave4 + leave;
                        x.Leave = Leave4;
                    }
                    if (year == 2021)
                    {
                        if (monthcount3 > 0)
                        {
                            res.Remove(res.Single(s => s.Year == 2021));
                        }
                        monthcount3++;
                        x.Year = 2021;
                        var net = Convert.ToInt32(x.TotalSalary);
                        amount3 = (double)(amount3 + net);
                        x.TotalSalary = amount3;

                        var dedu = Convert.ToInt32(x.Deduction);
                        Reduction3 = (double)(Reduction3 + dedu);
                        x.Deduction = Reduction3;

                        var tax = Convert.ToInt32(x.tax);
                        Tax3 = Tax3 + tax;
                        x.tax = Tax3;

                        var leave = Convert.ToInt32(x.Leave);
                        Leave3 = Leave3 + leave;
                        x.Leave = Leave3;
                    }





                });

                response.Items = res;
            }
            catch (System.Exception Exception)
            {
                throw;
            }
            return response;
        }











        public async Task<ResponseModel<MonthlyReportDto>> GetMonthlyReport()
        {
            ResponseModel<MonthlyReportDto> response = new ResponseModel<MonthlyReportDto>();
            try
            {

                var res = await _DataContext.SalaryPayments
                .Select(rep => new MonthlyReportDto()
                {
                    TotalSalary = rep.NetAmount,
                    Leave = (int)rep.Leave,
                    tax = rep.tax,
                    Deduction = rep.Deduction,
                    Date = rep.Date
                }).ToListAsync();



                var amount12 = Convert.ToDouble(0);
                var Reduction12 = Convert.ToDouble(0);
                var Tax12 = 0;
                var Leave12 = 0;
                int monthcount12 = 0;


                var amount11 = Convert.ToDouble(0);
                var Reduction11 = Convert.ToDouble(0);
                var Tax11 = 0;
                var Leave11 = 0;
                int monthcount11 = 0;


                var amount10 = Convert.ToDouble(0);
                var Reduction10 = Convert.ToDouble(0);
                var Tax10 = 0;
                var Leave10 = 0;
                int monthcount10 = 0;

                var amount9 = Convert.ToDouble(0);
                var Reduction9 = Convert.ToDouble(0);
                var Tax9 = 0;
                var Leave9 = 0;
                int monthcount9 = 0;

                var amount8 = Convert.ToDouble(0);
                var Reduction8 = Convert.ToDouble(0);
                var Tax8 = 0;
                var Leave8 = 0;
                int monthcount8 = 0;

                var amount7 = Convert.ToDouble(0);
                var Reduction7 = Convert.ToDouble(0);
                var Tax7 = 0;
                var Leave7 = 0;
                int monthcount7 = 0;

                var amount6 = Convert.ToDouble(0);
                var Reduction6 = Convert.ToDouble(0);
                var Tax6 = 0;
                var Leave6 = 0;
                int monthcount6 = 0;

                var amount5 = Convert.ToDouble(0);
                var Reduction5 = Convert.ToDouble(0);
                var Tax5 = 0;
                var Leave5 = 0;
                int monthcount5 = 0;

                var amount4 = Convert.ToDouble(0);
                var Reduction4 = Convert.ToDouble(0);
                var Tax4 = 0;
                var Leave4 = 0;
                int monthcount4 = 0;

                var amount3 = Convert.ToDouble(0);
                var Reduction3 = Convert.ToDouble(0);
                var Tax3 = 0;
                var Leave3 = 0;
                int monthcount3 = 0;

                var amount2 = Convert.ToDouble(0);
                var Reduction2 = Convert.ToDouble(0);
                var Tax2 = 0;
                var Leave2 = 0;
                int monthcount2 = 0;

                var amount1 = Convert.ToDouble(0);
                var Reduction1 = Convert.ToDouble(0);
                var Tax1 = 0;
                var Leave1 = 0;
                int monthcount1 = 0;



                res.ToList().ForEach(x =>
                {
                    var month = x.Date.Month;
                    if (month == 12)
                    {

                        // Show(month);
                        if (monthcount12 > 0)
                        {
                            res.Remove(res.Single(s => s.Month == "December"));
                        }
                        monthcount12++;
                        x.Month = "December";
                        var net = Convert.ToInt32(x.TotalSalary);
                        amount12 = (double)(amount12 + net);
                        x.TotalSalary = amount12;

                        var dedu = Convert.ToInt32(x.Deduction);
                        Reduction12 = (double)(Reduction12 + dedu);
                        x.Deduction = Reduction12;

                        var tax = Convert.ToInt32(x.tax);
                        Tax12 = Tax12 + tax;
                        x.tax = Tax12;

                        var leave = Convert.ToInt32(x.Leave);
                        Leave12 = Leave12 + leave;
                        x.Leave = Leave12;
                    }

                    if (month == 11)
                    {
                        if (monthcount11 > 0)
                        {
                            res.Remove(res.Single(s => s.Month == "November"));
                        }
                        monthcount11++;
                        x.Month = "November";
                        var net = Convert.ToInt32(x.TotalSalary);
                        amount11 = (double)(amount11 + net);
                        x.TotalSalary = amount11;

                        var dedu = Convert.ToInt32(x.Deduction);
                        Reduction11 = (double)(Reduction11 + dedu);
                        x.Deduction = Reduction11;

                        var tax = Convert.ToInt32(x.tax);
                        Tax11 = Tax11 + tax;
                        x.tax = Tax11;

                        var leave = Convert.ToInt32(x.Leave);
                        Leave11 = Leave11 + leave;
                        x.Leave = Leave11;
                    }
                    if (month == 10)
                    {
                        if (monthcount10 > 0)
                        {
                            res.Remove(res.Single(s => s.Month == "October"));
                        }
                        monthcount10++;
                        x.Month = "October";
                        var net = Convert.ToInt32(x.TotalSalary);
                        amount10 = (double)(amount10 + net);
                        x.TotalSalary = amount10;

                        var dedu = Convert.ToInt32(x.Deduction);
                        Reduction10 = (double)(Reduction10 + dedu);
                        x.Deduction = Reduction10;

                        var tax = Convert.ToInt32(x.tax);
                        Tax10 = Tax10 + tax;
                        x.tax = Tax10;

                        var leave = Convert.ToInt32(x.Leave);
                        Leave10 = Leave10 + leave;
                        x.Leave = Leave10;
                    }
                    if (month == 9)
                    {
                        if (monthcount9 > 0)
                        {
                            res.Remove(res.Single(s => s.Month == "September"));
                        }
                        monthcount9++;
                        x.Month = "September";
                        var net = Convert.ToInt32(x.TotalSalary);
                        amount9 = (double)(amount9 + net);
                        x.TotalSalary = amount9;

                        var dedu = Convert.ToInt32(x.Deduction);
                        Reduction9 = (double)(Reduction9 + dedu);
                        x.Deduction = Reduction11;

                        var tax = Convert.ToInt32(x.tax);
                        Tax9 = Tax9 + tax;
                        x.tax = Tax9;

                        var leave = Convert.ToInt32(x.Leave);
                        Leave9 = Leave9 + leave;
                        x.Leave = Leave9;
                    }
                    if (month == 8)
                    {
                        if (monthcount8 > 0)
                        {
                            res.Remove(res.Single(s => s.Month == "August"));
                        }
                        monthcount8++;
                        x.Month = "August";
                        var net = Convert.ToInt32(x.TotalSalary);
                        amount8 = (double)(amount8 + net);
                        x.TotalSalary = amount11;

                        var dedu = Convert.ToInt32(x.Deduction);
                        Reduction8 = (double)(Reduction8 + dedu);
                        x.Deduction = Reduction8;

                        var tax = Convert.ToInt32(x.tax);
                        Tax8 = Tax8 + tax;
                        x.tax = Tax8;

                        var leave = Convert.ToInt32(x.Leave);
                        Leave8 = Leave8 + leave;
                        x.Leave = Leave8;
                    }
                    if (month == 7)
                    {
                        if (monthcount7 > 0)
                        {
                            res.Remove(res.Single(s => s.Month == "July"));
                        }
                        monthcount7++;
                        x.Month = "July";
                        var net = Convert.ToInt32(x.TotalSalary);
                        amount7 = (double)(amount7 + net);
                        x.TotalSalary = amount7;

                        var dedu = Convert.ToInt32(x.Deduction);
                        Reduction7 = (double)(Reduction7 + dedu);
                        x.Deduction = Reduction7;

                        var tax = Convert.ToInt32(x.tax);
                        Tax7 = Tax7 + tax;
                        x.tax = Tax7;

                        var leave = Convert.ToInt32(x.Leave);
                        Leave7 = Leave7 + leave;
                        x.Leave = Leave7;
                    }
                    if (month == 6)
                    {
                        if (monthcount6 > 0)
                        {
                            res.Remove(res.Single(s => s.Month == "Jun"));
                        }
                        monthcount6++;
                        x.Month = "Jun";
                        var net = Convert.ToInt32(x.TotalSalary);
                        amount6 = (double)(amount6 + net);
                        x.TotalSalary = amount11;

                        var dedu = Convert.ToInt32(x.Deduction);
                        Reduction6 = (double)(Reduction6 + dedu);
                        x.Deduction = Reduction6;

                        var tax = Convert.ToInt32(x.tax);
                        Tax6 = Tax6 + tax;
                        x.tax = Tax6;

                        var leave = Convert.ToInt32(x.Leave);
                        Leave6 = Leave6 + leave;
                        x.Leave = Leave6;
                    }
                    if (month == 5)
                    {
                        if (monthcount5 > 0)
                        {
                            res.Remove(res.Single(s => s.Month == "April"));
                        }
                        monthcount5++;
                        x.Month = "May";
                        var net = Convert.ToInt32(x.TotalSalary);
                        amount5 = (double)(amount5 + net);
                        x.TotalSalary = amount5;

                        var dedu = Convert.ToInt32(x.Deduction);
                        Reduction5 = (double)(Reduction5 + dedu);
                        x.Deduction = Reduction11;

                        var tax = Convert.ToInt32(x.tax);
                        Tax11 = Tax5 + tax;
                        x.tax = Tax5;

                        var leave = Convert.ToInt32(x.Leave);
                        Leave5 = Leave5 + leave;
                        x.Leave = Leave5;
                    }
                    if (month == 4)
                    {
                        if (monthcount4 > 0)
                        {
                            res.Remove(res.Single(s => s.Month == "April"));
                        }
                        monthcount4++;
                        x.Month = "April";
                        var net = Convert.ToInt32(x.TotalSalary);
                        amount4 = (double)(amount4 + net);
                        x.TotalSalary = amount4;

                        var dedu = Convert.ToInt32(x.Deduction);
                        Reduction4 = (double)(Reduction4 + dedu);
                        x.Deduction = Reduction11;

                        var tax = Convert.ToInt32(x.tax);
                        Tax4 = Tax4 + tax;
                        x.tax = Tax4;

                        var leave = Convert.ToInt32(x.Leave);
                        Leave4 = Leave4 + leave;
                        x.Leave = Leave4;
                    }
                    if (month == 3)
                    {
                        if (monthcount3 > 0)
                        {
                            res.Remove(res.Single(s => s.Month == "March"));
                        }
                        monthcount3++;
                        x.Month = "March";
                        var net = Convert.ToInt32(x.TotalSalary);
                        amount3 = (double)(amount3 + net);
                        x.TotalSalary = amount3;

                        var dedu = Convert.ToInt32(x.Deduction);
                        Reduction3 = (double)(Reduction3 + dedu);
                        x.Deduction = Reduction3;

                        var tax = Convert.ToInt32(x.tax);
                        Tax3 = Tax3 + tax;
                        x.tax = Tax3;

                        var leave = Convert.ToInt32(x.Leave);
                        Leave3 = Leave3 + leave;
                        x.Leave = Leave3;
                    }
                    if (month == 2)
                    {
                        if (monthcount2 > 0)
                        {
                            res.Remove(res.Single(s => s.Month == "February"));
                        }
                        monthcount2++;
                        x.Month = "February";
                        var net = Convert.ToInt32(x.TotalSalary);
                        amount2 = (double)(amount2 + net);
                        x.TotalSalary = amount2;

                        var dedu = Convert.ToInt32(x.Deduction);
                        Reduction2 = (double)(Reduction2 + dedu);
                        x.Deduction = Reduction2;

                        var tax = Convert.ToInt32(x.tax);
                        Tax2 = Tax2 + tax;
                        x.tax = Tax2;

                        var leave = Convert.ToInt32(x.Leave);
                        Leave2 = Leave2 + leave;
                        x.Leave = Leave2;
                    }
                    if (month == 1)
                    {
                        if (monthcount1 > 0)
                        {
                            res.Remove(res.Single(s => s.Month == "January"));
                        }
                        monthcount1++;
                        x.Month = "January";
                        var net = Convert.ToInt32(x.TotalSalary);
                        amount1 = (double)(amount1 + net);
                        x.TotalSalary = amount1;

                        var dedu = Convert.ToInt32(x.Deduction);
                        Reduction1 = (double)(Reduction1 + dedu);
                        x.Deduction = Reduction1;

                        var tax = Convert.ToInt32(x.tax);
                        Tax1 = Tax1 + tax;
                        x.tax = Tax1;

                        var leave = Convert.ToInt32(x.Leave);
                        Leave1 = Leave1 + leave;
                        x.Leave = Leave1;
                    }





                });

                response.Items = res;
            }
            catch (System.Exception Exception)
            {
                throw;
            }
            return response;
        }



    }
}