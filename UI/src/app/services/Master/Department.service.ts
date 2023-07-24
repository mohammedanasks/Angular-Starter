import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ResponseModel } from 'app/dtos/base/responseModel';
import { DepartmentDto } from 'app/dtos/Master/departmentDto';
import { catchError, map } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { IdTextDto } from 'app/dtos/base/idTextDto';

@Injectable({
    providedIn: 'root',
  })
export class DepartmentService {

    private BaseURL='https://localhost:7087/api/'    

    constructor(private http: HttpClient) { }


    public AddDepartment(model: DepartmentDto):Observable<ResponseModel<DepartmentDto>>{
        return this.http.post(this.BaseURL+'Department/AddDepartment',model).
            pipe(map((response:ResponseModel<DepartmentDto>)=>{
            return response; 
            }),
            catchError((error) => {
                let res = new ResponseModel<DepartmentDto>();
                res.isOk = false;
                res.item = model;
                if (error.error instanceof ErrorEvent) {
                  res.message = `Error: ${error.error.message}`;
                } else {
                  res.message = `Error: ${error.message}`;
                }
                return of(res);
             })
        );
    }

  public  GetEmployNames():Observable<ResponseModel<IdTextDto>>{
    return this.http.get<ResponseModel<IdTextDto>>(this.BaseURL+'Employ/GetEmployNames')
    }
  public  GetDepartments():Observable<ResponseModel<DepartmentDto>>{
      return this.http.get<ResponseModel<DepartmentDto>>(this.BaseURL+'Department/GetDepartments')
    }
  public  DeleteDepartment(DepartmentId :number):Observable<ResponseModel<IdTextDto>>{
      return this.http.delete<ResponseModel<IdTextDto>>(this.BaseURL+`Department/${DepartmentId}`)
       }

   public GetEditDepartment (EditId: number):Observable<ResponseModel<DepartmentDto>>{
        return this.http.get<ResponseModel<DepartmentDto>>(this.BaseURL+`Department/${EditId}`)
      }
   
    public   UpdateDepartment (model:DepartmentDto):Observable<ResponseModel<DepartmentDto>>{
        return this.http.post<ResponseModel<DepartmentDto>>(this.BaseURL+'Department/UpdateDepartment',model)
      }  

    
     
}


