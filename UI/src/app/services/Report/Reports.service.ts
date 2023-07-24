import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IdTextDto } from 'app/dtos/base/idTextDto';
import { ResponseModel } from 'app/dtos/base/responseModel';
import { LeaveDto } from 'app/dtos/LeaveManagement/leaveDto';
import { EmployeeDto } from 'app/dtos/Master/employeeDto';
import { CurrentMonthPaymentsDto } from 'app/dtos/Report/currentMonthPaymentsDto';
import { DepartmentReportDto } from 'app/dtos/Report/DepartmentReportDto';
import { FinanceDepartmentLeaveReportDto } from 'app/dtos/Report/financeDepartmentLeaveReportDto';
import { HrDepartmentLeaveReportDto } from 'app/dtos/Report/hrDepartmentLeaveReportDto';
import { ITDepartmentLeaveReportDto } from 'app/dtos/Report/ITDepartmentLeaveReportDto';

import { MonthlyReportDto } from 'app/dtos/Report/monthlyReportsDto';
import { SearchLeveBetweenDatesDto } from 'app/dtos/Report/searchLeveBetweenDatesDto';
import { SearchDepartmentLeaveReportDto } from 'app/dtos/Report/SerachDepartmentLeaveReportDto copy';
import { YearlyReportDto } from 'app/dtos/Report/yearlyReportDto';

import { SalaryPaymentDto } from 'app/dtos/SaleryManagement/salaryPaymentDto';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class ReportsService {

    private BaseURL = 'https://localhost:7087/api/';

    constructor(private http: HttpClient) {}


    public GetEmploys(): Observable<ResponseModel<EmployeeDto>> {
        return this.http.get<ResponseModel<EmployeeDto>>(this.BaseURL + 'Reports/GetEmploys');
      }

      public GetMonthlyReport(): Observable<ResponseModel<DepartmentReportDto>> {
        return this.http.get<ResponseModel<DepartmentReportDto>>(this.BaseURL + 'Reports/GetMonthlyReport');
      }


      public GetLeaveReport(): Observable<ResponseModel<CurrentMonthPaymentsDto>> {
        return this.http.get<ResponseModel<CurrentMonthPaymentsDto>>(this.BaseURL + 'Reports/GetLeaveReport');
      }

      public GetYearlyReport(): Observable<ResponseModel<YearlyReportDto>> {
        return this.http.get<ResponseModel<YearlyReportDto>>(this.BaseURL + 'Reports/GetYearlyReport');
      }


     public GetLSalaryList():Observable<ResponseModel<SalaryPaymentDto>> {
        return this.http.get<ResponseModel<SalaryPaymentDto>>(this.BaseURL+'Reports/GetSalaryList');
      }  

      public GetSearchList(month :number|string):Observable<ResponseModel<SalaryPaymentDto>> {
        return this.http.get<ResponseModel<SalaryPaymentDto>>(this.BaseURL+`Reports/${month}`);
      }   

      
     public GetITdepartmentLeaveReport():Observable<ResponseModel<ITDepartmentLeaveReportDto>> {
      return this.http.get<ResponseModel<ITDepartmentLeaveReportDto>>(this.BaseURL+'Reports/ITdepartmentLeaveReport');
    }  

    
    public  GetHRdepartmentLeaveReport():Observable<ResponseModel<HrDepartmentLeaveReportDto>> {
      return this.http.get<ResponseModel<HrDepartmentLeaveReportDto>>(this.BaseURL+'Reports/HRdepartmentLeaveReport');
    }  

    
    public  GetFinanceDepartmentLeaveReport():Observable<ResponseModel<FinanceDepartmentLeaveReportDto>> {
      return this.http.get<ResponseModel<FinanceDepartmentLeaveReportDto>>(this.BaseURL+'Reports/FinanceDepartmentLeaveReport');
    }  


       
    public  SearchDepartmentLeaveReport(model : IdTextDto):Observable<ResponseModel<SearchDepartmentLeaveReportDto>> {
      return this.http.post<ResponseModel<SearchDepartmentLeaveReportDto>>(this.BaseURL+'Reports/SearchDepartmentLeaveReport',model);
    }  

    
  public SearchLeaveBetween(model: SearchLeveBetweenDatesDto): Observable<ResponseModel<SearchLeveBetweenDatesDto>> {
    return this.http.post(this.BaseURL + 'Reports/SearchLeaveBetween',model).pipe(
      map((response: ResponseModel<SearchLeveBetweenDatesDto>) => {
        return response;
      })
    );
  }

}
