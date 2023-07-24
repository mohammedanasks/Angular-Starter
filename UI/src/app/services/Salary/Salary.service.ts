import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IdTextDto } from 'app/dtos/base/idTextDto';
import { ResponseModel } from 'app/dtos/base/responseModel';
import { LeaveDto } from 'app/dtos/LeaveManagement/leaveDto';
import { EmployeeDto } from 'app/dtos/Master/employeeDto';
import { CurrentMonthPaymentsDto } from 'app/dtos/Report/currentMonthPaymentsDto';
import { GetEmploysForPaymentDto } from 'app/dtos/SaleryManagement/getEmploysForPaymentDto copy';

import { SalaryPaymentDto } from 'app/dtos/SaleryManagement/salaryPaymentDto';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class SalaryService {
  private BaseURL = 'https://localhost:7087/api/';

  constructor(private http: HttpClient) {}

  public DeleteSalary(SalaryId: number): Observable<ResponseModel<IdTextDto>> {
    return this.http.delete<ResponseModel<IdTextDto>>(this.BaseURL+`Salary/${SalaryId}`);
  }



  public GetEmploys(): Observable<ResponseModel<CurrentMonthPaymentsDto>> {
    return this.http.get<ResponseModel<CurrentMonthPaymentsDto>>(this.BaseURL+'Salary/GetEmploys');
  }

  public GetEmploForPayment(EmpID:number): Observable<ResponseModel<GetEmploysForPaymentDto>> {
    return this.http.get<ResponseModel<GetEmploysForPaymentDto>>(this.BaseURL+`Salary/${EmpID}`);
  }


  public AddSalary(model: SalaryPaymentDto): Observable<ResponseModel<SalaryPaymentDto>> {
    return this.http.post(this.BaseURL + 'Salary/AddSalary',model).pipe(
      map((response: ResponseModel<SalaryPaymentDto>) => {
        return response;
      })
    );
  }
}
