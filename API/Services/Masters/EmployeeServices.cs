using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Dtos.BaseEntity.ResponseModel;
using API.Dtos.IdTextDto;
using AutoMapper;
using API.Entities.Masters;
using API.Dtos.Masters;
using Microsoft.EntityFrameworkCore;
using API.Dtos.LeaveManagement;
using API.Entities.LeaveManagement;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;

namespace API.Services.Masters
{
    public class EmployeeServices
    {
        private readonly DataContext _DataContext;
        public IMapper _Mapper { get; }

        public EmployeeServices(DataContext DataContext, IMapper Mapper)
        {
            _Mapper = Mapper;
            _DataContext = DataContext;
        }


        public async Task<ResponseModel<EmployeeDto>> AddEmploy(EmployeeDto Dto)
        {
            ResponseModel<EmployeeDto> response = new ResponseModel<EmployeeDto>();
            try
            {  
                var status=false;
                var count=0;
                var employ=_DataContext.Employees.Select(x=> new EmployeeDto(){}).ToList();
                employ.ToList().ForEach(emp=>{
                    if(Dto.FirstName==null){
                        emp.FirstNameRequired="FirstName Required";
                        status=true;
                        response.IsOk=false;
                    }
                      if(Dto.LastName==null){
                        emp.LastNameRequired="LastName Required";
                        status=true;
                        response.IsOk=false;
                    }
                      if(Dto.DesignationId==null){
                        emp.DesignationRequired="Designation Required";
                        status=true;
                        response.IsOk=false;
                    }
                      if(Dto.Contact==null){
                        emp.ContactRequired="Contact Required";
                        status=true;
                        response.IsOk=false;

                    }  if(Dto.DepartmentId==null){
                        emp.DepartmentNameRequired="Department Required";
                        status=true;
                        response.IsOk=false;
                    }
                      if(Dto.BasicSalary==null){
                        emp.SalaryRequired="Salary Required";
                        status=true;
                        response.IsOk=false;
                    }
                    if(count>0){
                        employ.Remove(employ.FirstOrDefault(x=>x.FirstNameRequired==null||
                        x.LastNameRequired==null||x.ContactRequired==null||x.DesignationRequired==null||x.SalaryRequired==null));
                    }
                    count++;
                  
                });
                 if(status==false){
                     var model = _Mapper.Map<Employee>(Dto);
                    var res = await _DataContext.AddAsync(model);
                    await _DataContext.SaveChangesAsync();
                 } 
              response.Items=employ;  
            }
        
            catch (System.Exception Exception)
            {
                throw;
            }
            return response;
        }



        public async Task<ResponseModel<EmployeeDto>> GetEmploys()
        {
            ResponseModel<EmployeeDto> response = new ResponseModel<EmployeeDto>();
            try
            {
                var model = await _DataContext.Employees.Include(x => x.Department).Include(x =>
                x.Designation).ToListAsync();
                var res = _Mapper.Map<List<EmployeeDto>>(model);
                response.Items = res;
            }
            catch (System.Exception Exception)
            {
                throw;
            }
            return response;
        }
         public async Task<LoadResult> DevGetEmploys(DataSourceLoadOptionsBase loadOptions)
        {
            
            try
            {
                var model =  _DataContext.Employees.Include(x => x.Department).Include(x =>
                x.Designation).Select(x=>new EmployeeDto(){
                    fileName=x.FirstName,
                    Id=x.Id
                });
                var res = await DataSourceLoader.LoadAsync(model,loadOptions);
                return res;
            }
            catch (System.Exception Exception)
            {
                throw;
            }
            
        }

        public async Task<ResponseModel<DesignationDto>> AddDesignation(DesignationDto Dto)
        {
            ResponseModel<DesignationDto> response = new ResponseModel<DesignationDto>();

            try
            {

                var model = _Mapper.Map<Designation>(Dto);
                var res = await _DataContext.AddAsync(model);
                await _DataContext.SaveChangesAsync();

            }
            catch (System.Exception Exception)
            {
                throw;
            }
            return response;
        }

        public async Task<ResponseModel<IdTextDto>> GetEmployNames()
        {
            ResponseModel<IdTextDto> response = new ResponseModel<IdTextDto>();
            try
            {
                var model = _DataContext.Employees.Select(x => new IdTextDto
                {
                    Id = x.Id,
                    Text = x.LastName
                }).ToList();
                response.Items = model;
                return response;
            }
            catch (System.Exception Exception)
            {
                throw;
            }
        }

        public async Task<ResponseModel<IdTextDto>> DeleteEmploy(int EmployId)
        {
            ResponseModel<IdTextDto> response = new ResponseModel<IdTextDto>();

            try
            {
                var employ = await _DataContext.Employees.AsQueryable().
                Where(x => x.Id == EmployId).
                Include(x => x.Department).Include(x => x.Designation).FirstOrDefaultAsync();
                if (employ != null)
                {
                    _DataContext.Employees.Remove(employ);
                    await _DataContext.SaveChangesAsync();
                }
            }
            catch (System.Exception Exception)
            {
                throw;
            }
            return response;

        }

        public async Task<ResponseModel<EmployeeDto>> GetEditEmploy(int EmployId)
        {
            ResponseModel<EmployeeDto> response = new ResponseModel<EmployeeDto>();
            try
            {
                var employ = _DataContext.Employees.Where(x => x.Id == EmployId).FirstOrDefault();
                var res = _Mapper.Map<EmployeeDto>(employ);
                response.Item = res;
            }
            catch (System.Exception Exception)
            {
                throw;
            }
            return response;

        }

        public async Task<ResponseModel<EmployeeDto>> UpdateEmploy(EmployeeDto model)
        {
            ResponseModel<EmployeeDto> response = new ResponseModel<EmployeeDto>();
            try
            {
                var res = _Mapper.Map<Employee>(model);
                if (res != null)
                {
                    _DataContext.Employees.Update(res);
                    await _DataContext.SaveChangesAsync();
                }
            }
            catch (System.Exception Exception)
            {
                throw;
            }
            return response;
        }


        
        public async Task<ResponseModel<DesignationDto>>GetDesignations()
        {
            ResponseModel<DesignationDto> response = new ResponseModel<DesignationDto>();
            try
            {
                var model = await _DataContext.Designation.ToListAsync();
                var res = _Mapper.Map<List<DesignationDto>>(model);
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
