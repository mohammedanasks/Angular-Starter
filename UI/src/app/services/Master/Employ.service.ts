import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IdTextDto } from 'app/dtos/base/idTextDto';
import { ResponseModel } from 'app/dtos/base/responseModel';
import { LeaveDto } from 'app/dtos/LeaveManagement/leaveDto';
import { DesignationDto } from 'app/dtos/Master/desiginationDto';
import { EmployeeDto } from 'app/dtos/Master/employeeDto';
import { LeaveTypeDto } from 'app/dtos/Master/LeaveTypeDto';
import { Observable, ObservableInput, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class EmployService {
  private BaseURL = 'https://localhost:7087/api/';

  constructor(private http: HttpClient) {}

  public AddEmploy(model: EmployeeDto): Observable<ResponseModel<EmployeeDto>> {
    return this.http.post(this.BaseURL + 'Employ/AddEmploy', model).pipe(
      map((response: ResponseModel<EmployeeDto>) => {
        return response;
      }),
      catchError((error) => {
        let res = new ResponseModel<EmployeeDto>();
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

  public GetEmploys(): Observable<ResponseModel<EmployeeDto>> {
    return this.http.get<ResponseModel<EmployeeDto>>(this.BaseURL + 'Employ/Getemploys');
  }
  public DeleteEmploy(EmployId: number): Observable<ResponseModel<IdTextDto>> {
    return this.http.delete<ResponseModel<IdTextDto>>(this.BaseURL + `Employ/${EmployId}`);
  }
  public GetEditEmploy(EditId: number): Observable<ResponseModel<EmployeeDto>> {
    return this.http.get<ResponseModel<EmployeeDto>>(this.BaseURL + `Employ/${EditId}`);
  }
  public UpdateEmploy(model: EmployeeDto): Observable<ResponseModel<EmployeeDto>> {
    return this.http.post<ResponseModel<EmployeeDto>>(this.BaseURL + 'Employ/UpdateEmploy', model);
  }
  public GetDesignations(): Observable<ResponseModel<DesignationDto>> {
    return this.http.get<ResponseModel<DesignationDto>>(this.BaseURL + 'Employ/GetDesignations');
  }

   
}
