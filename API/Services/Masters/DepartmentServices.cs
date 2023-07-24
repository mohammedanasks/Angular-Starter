using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.BaseEntity.ResponseModel;
using API.Dtos.Masters;
using API.Entities;
using API.Entities.Masters;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using API.Dtos.IdTextDto;

namespace API.Services.Masters
{
    public class DepartmentServices
    {
        private readonly DataContext _DataContext;
        public IMapper _Mapper { get; }

        public DepartmentServices(DataContext DataContext, IMapper Mapper)
        {
            _Mapper = Mapper;
            _DataContext = DataContext;

        }


        public async Task<ResponseModel<DepartmentDto>> AddDepartment(DepartmentDto Dto)
        {
            ResponseModel<DepartmentDto> response = new ResponseModel<DepartmentDto>();

            try
            {

                var model = _Mapper.Map<Department>(Dto);
                var res = await _DataContext.AddAsync(model);
                await _DataContext.SaveChangesAsync();

            }
            catch (System.Exception Exception)
            {
                throw;
            }
            return response;
        }

        public async Task<ResponseModel<DepartmentDto>> GetDepartments()
        {
            ResponseModel<DepartmentDto> response = new ResponseModel<DepartmentDto>();

            try
            {
                var departments = await _DataContext.Departments.Include(x => x.DepartmentHead).ToListAsync();
                var res = _Mapper.Map<List<DepartmentDto>>(departments);
                response.Items = res;
            }
            catch (System.Exception Exception)
            {
                throw;
            }
            return response;
        }
        public async Task<ResponseModel<IdTextDto>> DeleteDepartment(int DepartmentId)
        {
            ResponseModel<IdTextDto> response = new ResponseModel<IdTextDto>();

            try
            {
                var department = await _DataContext.Departments.AsQueryable().
                Where(x => x.Id == DepartmentId).FirstOrDefaultAsync();
                if (department != null)
                {
                    _DataContext.Departments.Remove(department);
                    await _DataContext.SaveChangesAsync();
                }
            }
            catch (System.Exception Exception)
            {
                throw;
            }
            return response;

        }

        public async Task<ResponseModel<DepartmentDto>>GetEditDepartment(int DepartmentId)
        {
            ResponseModel<DepartmentDto> response = new ResponseModel<DepartmentDto>();
            try
            {
                var department = _DataContext.Departments.Where(x => x.Id == DepartmentId).FirstOrDefault();
                var res = _Mapper.Map<DepartmentDto>(department);
                response.Item = res;
            }
            catch (System.Exception Exception)
            {
                throw;
            }
            return response;

        }

        public async Task<ResponseModel<DepartmentDto>> UpdateDepartment(DepartmentDto model)
        {
            ResponseModel<DepartmentDto> response = new ResponseModel<DepartmentDto>();
            try
            {
                var res = _Mapper.Map<Department>(model);
                if (res != null)
                {
                    _DataContext.Departments.Update(res);
                    await _DataContext.SaveChangesAsync();
                }
            }
            catch (System.Exception Exception)
            {
                throw;
            }
            return response;
        }


    }
}