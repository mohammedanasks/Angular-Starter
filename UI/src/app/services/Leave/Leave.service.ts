import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IdTextDto } from 'app/dtos/base/idTextDto';
import { ResponseModel } from 'app/dtos/base/responseModel';
import { LeaveDto } from 'app/dtos/LeaveManagement/leaveDto';
import { LeaveTypeDto } from 'app/dtos/Master/LeaveTypeDto';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class LeaveService {
  private BaseURL = 'https://localhost:7087/api/';

  constructor(private http: HttpClient) {}

  public DeleteLeave(LeaveId: number): Observable<ResponseModel<IdTextDto>> {
    return this.http.delete<ResponseModel<IdTextDto>>(this.BaseURL+`Leave/${LeaveId}`);
  }

  public GetLeaves(): Observable<ResponseModel<LeaveDto>> {
    return this.http.get<ResponseModel<LeaveDto>>(this.BaseURL+'Leave/GetLeaves');
  }

  public GetLeaveType(): Observable<ResponseModel<LeaveTypeDto>> {
    return this.http.get<ResponseModel<LeaveTypeDto>>(this.BaseURL+'Leave/GetLeaveType');
  }

  public GetEditLeave(EditId: number): Observable<ResponseModel<LeaveDto>> {
    return this.http.get<ResponseModel<LeaveDto>>(this.BaseURL+`Leave/${EditId}`);
  }

  public AddLeave(model: LeaveDto): Observable<ResponseModel<LeaveDto>> {
    return this.http.post(this.BaseURL + 'Leave/AddLeave',model).pipe(
      map((response: ResponseModel<LeaveDto>) => {
        return response;
      })
    );
  }

  public UpdateLeave(model: LeaveDto): Observable<ResponseModel<LeaveDto>> {
    return this.http.post<ResponseModel<LeaveDto>>(this.BaseURL + 'Leave/UpdateLeave', model);
  }

  public AddLeaveType(model: LeaveTypeDto): Observable<ResponseModel<LeaveTypeDto>> {
    return this.http.post(this.BaseURL + 'Leave/AddLeaveType', model).pipe(
      map((response: ResponseModel<LeaveTypeDto>) => {
        return response;
      }),
    )} 
}
