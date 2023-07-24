using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.BaseEntity.ResponseModel;
using API.Dtos.IdTextDto;
using API.Dtos.LeaveManagement;
using API.Dtos.Masters;
using API.Entities;
using API.Entities.LeaveManagement;
using API.Entities.Masters;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Leaves
{
    public class LeaveServices
    {
        private readonly DataContext _DataContext;

        public IMapper _Mapper { get; }

        public LeaveServices(DataContext DataContext, IMapper Mapper)
        {
            _Mapper = Mapper;
            _DataContext = DataContext;
        }

        public async Task<ResponseModel<LeaveDto>> AddLeave(LeaveDto Dto)
        {
            ResponseModel<LeaveDto> response = new ResponseModel<LeaveDto>();
            try
            {
                var status = false;
                var leve = new LeaveDto();
                
                        if (Dto.EmployeeId == null)
                        {
                            leve.NameRequired = "Name Required";
                            status = true;
                            response.IsOk=false;
                        }
                        if (Dto.LeaveTypeId == null)
                        {
                            leve.LeaveTypeRequired = "Leave type required";
                            status = true;
                            response.IsOk=false;
                        }
                        if (Dto.ToDate == null)
                        {
                            leve.DateRequired = "Date Required";
                            status = true;
                            response.IsOk=false;
                        }
                        if (Dto.FromDate == null)
                        {
                            leve.DateRequired = "Date Required";
                            status = true;
                            response.IsOk=false;
                        }
                        
                    
                    
                var model = _Mapper.Map<Leave>(Dto);
                var leveList = _DataContext.Leaves.ToList();
                var data1 =
                    leveList
                        .Where(x => x.EmployeeId == model.EmployeeId)
                        .ToList();
                if (status == false)
                {
                    if (data1.Count != 0)
                    {
                        var i = 0;
                        foreach (var data in data1)
                        {
                            i++;

                            if (
                                model.ToDate <= data.ToDate &&
                                model.FromDate <= data.FromDate
                            )
                            {
                                response.IsOk = false;
                                response.Message =
                                    "your already taken leave in this date";
                            }
                            else if (
                                model.FromDate > data.FromDate &&
                                model.FromDate < data.ToDate
                            )
                            {
                                var DTodate = data.ToDate.Day;
                                var Todate = data.ToDate;
                                var date = Convert.ToDateTime(Todate);
                                var DateIncrement = Todate.AddDays(1);
                                var ChangeDate =
                                    DateIncrement.ToString("MM-dd-yyyy");
                                var FFromdate = model.FromDate.Day;
                                var days = DTodate - FFromdate;

                                response.IsOk = false;
                                var msg =
                                    "your already have" +
                                    days +
                                    "\n days leave in this date change from Date to " +
                                    ChangeDate;
                                response.Message = msg;
                            }
                            else if (i == data1.Count)
                            {
                                var res = await _DataContext.AddAsync(model);
                                await _DataContext.SaveChangesAsync();
                            }
                        }
                    }
                }
                
                
                    if (status == false)
                    {
                        var res = await _DataContext.AddAsync(model);
                        await _DataContext.SaveChangesAsync();
                    }
                

                response.Item=leve;
            }
            
            catch (System.Exception Exception)
            {
                throw;
            }
            return response;
        }

        public async Task<ResponseModel<LeaveDto>> GetLeaves()
        {
            ResponseModel<LeaveDto> response = new ResponseModel<LeaveDto>();
            try
            {
                var model =
                    await _DataContext
                        .Leaves
                        .Include(x => x.LeaveType)
                        .Include(x => x.Employee)
                        .ThenInclude(x => x.Department)
                        .ToListAsync();
                var res = _Mapper.Map<List<LeaveDto>>(model);
                response.Items = res;
            }
            catch (System.Exception Exception)
            {
                throw;
            }
            return response;
        }

        public async Task<ResponseModel<LeaveTypeDto>> GetLeavesTypes()
        {
            ResponseModel<LeaveTypeDto> response =
                new ResponseModel<LeaveTypeDto>();
            try
            {
                var model = await _DataContext.LeaveTypes.ToListAsync();
                var res = _Mapper.Map<List<LeaveTypeDto>>(model);
                response.Items = res;
            }
            catch (System.Exception Exception)
            {
                throw;
            }
            return response;
        }

        public async Task<ResponseModel<IdTextDto>> DeleteLeave(int LeaveId)
        {
            ResponseModel<IdTextDto> response = new ResponseModel<IdTextDto>();

            try
            {
                var Leave =
                    await _DataContext
                        .Leaves
                        .AsQueryable()
                        .Where(x => x.Id == LeaveId)
                        .FirstOrDefaultAsync();
                if (Leave != null)
                {
                    _DataContext.Leaves.Remove (Leave);
                    await _DataContext.SaveChangesAsync();
                }
            }
            catch (System.Exception Exception)
            {
                throw;
            }
            return response;
        }

        public async Task<ResponseModel<LeaveDto>> GetEditLeave(int LeaveId)
        {
            ResponseModel<LeaveDto> response = new ResponseModel<LeaveDto>();
            try
            {
                var Leave =
                    _DataContext
                        .Leaves
                        .Where(x => x.Id == LeaveId)
                        .FirstOrDefault();
                var res = _Mapper.Map<LeaveDto>(Leave);
                response.Item = res;
            }
            catch (System.Exception Exception)
            {
                throw;
            }
            return response;
        }

        public async Task<ResponseModel<LeaveDto>> UpdateLeave(LeaveDto model)
        {
            ResponseModel<LeaveDto> response = new ResponseModel<LeaveDto>();
            try
            {
                var res = _Mapper.Map<Leave>(model);
                if (res != null)
                {
                    _DataContext.Leaves.Update (res);
                    await _DataContext.SaveChangesAsync();
                }
            }
            catch (System.Exception Exception)
            {
                throw;
            }
            return response;
        }

        public async Task<ResponseModel<LeaveTypeDto>>
        AddLeaveType(LeaveTypeDto Dto)
        {
            ResponseModel<LeaveTypeDto> response =
                new ResponseModel<LeaveTypeDto>();
            try
            {
                var model = _Mapper.Map<LeaveType>(Dto);
                var res = await _DataContext.AddAsync(model);
                await _DataContext.SaveChangesAsync();
            }
            catch (System.Exception Exception)
            {
                throw;
            }
            return response;
        }
    }
}
