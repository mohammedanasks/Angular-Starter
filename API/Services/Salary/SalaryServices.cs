using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using API.Dtos.BaseEntity.ResponseModel;
using API.Dtos.IdTextDto;
using API.Dtos.Masters;
using API.Dtos.SalaryManagement;
using API.Entities;
using API.Entities.SalaryManagement;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Salary
{
    public class SalaryServices
    {
        private readonly DataContext _DataContext;

        public IMapper _Mapper { get; }

        public SalaryServices(DataContext DataContext, IMapper Mapper)
        {
            _Mapper = Mapper;
            _DataContext = DataContext;
        }

        public async Task<ResponseModel<SalaryPaymentDto>>
        AddSalary(SalaryPaymentDto Dto)
        {
            ResponseModel<SalaryPaymentDto> response =
                new ResponseModel<SalaryPaymentDto>();
            try
            {
                var saladata = _DataContext.SalaryPayments.Select(sal => new SalaryPaymentDto() { })
                .ToList();

                var salarydata = _DataContext.SalaryPayments.ToList();
                var status = false;
                saladata
                    .ForEach(sal =>
                    {
                        foreach (var salary in salarydata)
                        {
                            var IdData=  salary.EmployeeId==Dto.EmployeeId;
                          
                            if (IdData==true&& Dto.EmployeeId!=null)
                            {
                                sal.AllReadyPaid="Employ all ready paid";
                                response.IsOk=false;
                                status = true;
            
                            }
                            if(Dto.EmployeeId==null){
                                 status = true;
                                  
                            }
                        }
                    });  
             
                if (status == false)
                {
                    var model = _Mapper.Map<SalaryPayment>(Dto);
                    var res = await _DataContext.AddAsync(model);
                    await _DataContext.SaveChangesAsync();
                }
                response.Items=saladata;
            }
            catch (System.Exception Exception)
            {
                throw;
            }
            return response;
        }

        public async Task<ResponseModel<IdTextDto>> DeleteSalary(int SalaryId)
        {
            ResponseModel<IdTextDto> response = new ResponseModel<IdTextDto>();

            try
            {
                var Salary =
                    await _DataContext
                        .SalaryPayments
                        .AsQueryable()
                        .Where(x => x.Id == SalaryId)
                        .FirstOrDefaultAsync();
                if (Salary != null)
                {
                    _DataContext.SalaryPayments.Remove (Salary);
                    await _DataContext.SaveChangesAsync();
                }
            }
            catch (System.Exception Exception)
            {
                throw;
            }
            return response;
        }

        public async Task<ResponseModel<CurrentMonthPaymentsDto>> GetEmploys()
        {
            ResponseModel<CurrentMonthPaymentsDto> response =
                new ResponseModel<CurrentMonthPaymentsDto>();
            try
            {
                var res =
                    await _DataContext
                        .Employees
                        .Include(x => x.Department)
                        .Include(x => x.Designation)
                        .Select(emp =>
                            new CurrentMonthPaymentsDto()
                            {
                                EmployeeName =
                                    emp.FirstName + " " + emp.LastName,
                                DepartmentName = emp.Department.Name,
                                DesignationName = emp.Designation.Name,
                                EmployeeId = emp.Id,
                                BasicSalary = emp.BasicSalary,
                                Date = DateTime.Now
                            })
                        .ToListAsync();
                int Days =
                    DateTime
                        .DaysInMonth(DateTime.Today.Year, DateTime.Today.Month);
                var LastDay =
                    new DateTime(DateTime.Today.Year,
                        DateTime.Today.Month,
                        Days);
                var FirstDay =
                    new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

                var LeaveData =
                    _DataContext
                        .Leaves
                        .Include(x => x.LeaveType)
                        .Where(x =>
                            (
                            x.FromDate.Date < LastDay.Date &&
                            x.ToDate > FirstDay
                            ))
                        .ToList();

                var PymentData = _DataContext.SalaryPayments.ToList();

                res
                    .ToList()
                    .ForEach(x =>
                    {
                        double leaveCount = 0;
                        double paidLeaveCount = 0;
                        bool status = true;

                        var payment =
                            PymentData
                                .Where(p => p.EmployeeId == x.EmployeeId)
                                .ToList();
                        var leaves =
                            LeaveData
                                .Where(lv => lv.EmployeeId == x.EmployeeId)
                                .ToList();

                        string sMonth = DateTime.Now.ToString("MM");
                        var month = Convert.ToInt32(sMonth);

                        foreach (var pay in payment)
                        {
                            if (pay.EmployeeId == x.EmployeeId)
                            {
                                x.IsPaid = true;
                            }

                            // if(pay.Date.Month==month){
                            //         // res.Remove(res.FirstOrDefault(s => s.EmployeeId == pay.EmployeeId));
                            //     }
                        }

                        if (leaves != null && leaves.Count > 0)
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
                                var cnt =
                                    ((tmTo.AddDays(1)) - tmFrom).TotalDays;
                                leaveCount += cnt;
                                status = leave.LeaveType.IsPaid;
                                if (leave.LeaveType.IsPaid == true)
                                {
                                    paidLeaveCount += cnt;
                                }
                            }
                        }
                        var sal = x.BasicSalary;
                        var tax = (x.BasicSalary / 100) * 10;
                        x.Tax = tax;
                        if (status == false)
                        {
                            int red = (int) leaveCount * 500;
                            var reduction = red;
                            x.Deduction = Convert.ToDouble(red);
                            x.NetAmount = (sal - tax) - reduction;
                        }

                        if (status == true)
                        {
                            var Salary = sal;
                            var Tax = tax;
                            x.NetAmount = Salary - Tax;
                        }
                        x.LeaveCount = (int) leaveCount;

                        x.PaidLeaveCount = (int) paidLeaveCount;
                    });

                response.Items = res;
            }
            catch (System.Exception Exception)
            {
                throw;
            }
            return response;
        }

        public async Task<ResponseModel<GetEmploysForPaymentDto>>
        GetEmploysForPayment(int EmpId)
        {
            ResponseModel<GetEmploysForPaymentDto> response =
                new ResponseModel<GetEmploysForPaymentDto>();
            try
            {
                var res =
                    await _DataContext
                        .Employees
                        .Where(x => x.Id == EmpId)
                        .Include(x => x.Department)
                        .Include(x => x.Designation)
                        .Select(emp =>
                            new GetEmploysForPaymentDto()
                            {
                                EmployeeName =
                                    emp.FirstName + " " + emp.LastName,
                                DepartmentName = emp.Department.Name,
                                DesignationName = emp.Designation.Name,
                                EmployeeId = emp.Id,
                                BasicSalary = emp.BasicSalary,
                                Date = DateTime.Now,
                                LastName = emp.LastName
                            })
                        .FirstOrDefaultAsync();
                int Days =
                    DateTime
                        .DaysInMonth(DateTime.Today.Year, DateTime.Today.Month);
                var LastDay =
                    new DateTime(DateTime.Today.Year,
                        DateTime.Today.Month,
                        Days);
                var FirstDay =
                    new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

                var LeaveData =
                    _DataContext
                        .Leaves
                        .Include(x => x.LeaveType)
                        .Where(x =>
                            (
                            x.FromDate.Date < LastDay.Date &&
                            x.ToDate > FirstDay
                            ))
                        .ToList();

                var PymentData = _DataContext.SalaryPayments.ToList();

                double leaveCount = 0;
                double paidLeaveCount = 0;
                bool status = true;

                var payment =
                    PymentData
                        .Where(p => p.EmployeeId == res.EmployeeId)
                        .ToList();
                var leaves =
                    LeaveData
                        .Where(lv => lv.EmployeeId == res.EmployeeId)
                        .ToList();

                string sMonth = DateTime.Now.ToString("MM");
                var month = Convert.ToInt32(sMonth);

                foreach (var pay in payment)
                {
                    if (pay.EmployeeId == res.EmployeeId)
                    {
                        res.IsPaid = true;
                    }

                    // if(pay.Date.Month==month){
                    //         // res.Remove(res.FirstOrDefault(s => s.EmployeeId == pay.EmployeeId));
                    //     }
                }

                if (leaves != null && leaves.Count > 0)
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
                        var cnt = ((tmTo.AddDays(1)) - tmFrom).TotalDays;
                        leaveCount += cnt;
                        status = leave.LeaveType.IsPaid;
                        if (leave.LeaveType.IsPaid == true)
                        {
                            paidLeaveCount += cnt;
                        }
                    }
                }
                var sal = res.BasicSalary;
                var tax = (res.BasicSalary / 100) * 10;
                res.Tax = tax;
                if (status == false)
                {
                    int red = (int) leaveCount * 500;
                    var reduction = red;
                    res.Deduction = Convert.ToDouble(red);
                    res.NetAmount = (sal - tax) - reduction;
                }

                if (status == true)
                {
                    var Salary = sal;
                    var Tax = tax;
                    res.NetAmount = Salary - Tax;
                }
                res.LeaveCount = (int) leaveCount;

                res.PaidLeaveCount = (int) paidLeaveCount;

                response.Item = res;
            }
            catch (System.Exception Exception)
            {
                throw;
            }
            return response;
        }
    }
}
