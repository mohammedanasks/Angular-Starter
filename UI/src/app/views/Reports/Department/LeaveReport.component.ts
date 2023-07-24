import { Component, OnInit } from '@angular/core';
import { IdTextDto } from 'app/dtos/base/idTextDto';
import { DepartmentDto } from 'app/dtos/Master/departmentDto';
import { CurrentMonthPaymentsDto } from 'app/dtos/Report/currentMonthPaymentsDto';

import { SearchLeveBetweenDatesDto } from 'app/dtos/Report/searchLeveBetweenDatesDto';
import { SearchDepartmentLeaveReportDto } from 'app/dtos/Report/SerachDepartmentLeaveReportDto copy';
import { DepartmentService } from 'app/services/Master/Department.service';

import { ReportsService } from 'app/services/Report/Reports.service';
import { color } from 'echarts';

@Component({
  selector: 'app-LeaveReport',
  templateUrl: './LeaveReport.component.html',
  styleUrls: ['./LeaveReport.component.css'],
})
export class DepartmentReportComponent implements OnInit {

  
  Leave: CurrentMonthPaymentsDto[];
  
  DepartmentLeaveSearch:IdTextDto
  DepartmentLeaveSearchList:SearchDepartmentLeaveReportDto[];

  SearchLeaveBetween:SearchLeveBetweenDatesDto;
  DropDepartments:DepartmentDto[];
  searchlist: SearchLeveBetweenDatesDto[];


  displayedColumns: string[] = ['Department', 'TotalLeave', 'TotalEmploys', 'TotalTax', 'TotalSalary'];
  ITdisplayedColumns: string[] = ['EmployeeName', 'Designation', 'EmployLeave', 'statusbars'];

  constructor(private service: ReportsService,private Departmentservice:DepartmentService) {}

  ngOnInit() {
    this.service.GetLeaveReport().subscribe((x) => {
      this.Leave = x.items;
    });

    this.Departmentservice.GetDepartments().subscribe(x=>{
      this.DropDepartments=x.items
    })


    this.DepartmentLeaveSearchList=new Array <SearchDepartmentLeaveReportDto>();
    this.DepartmentLeaveSearch=new IdTextDto;
    this.SearchLeaveBetween=new SearchLeveBetweenDatesDto;
    this.DropDepartments= new Array<DepartmentDto>();
    this.Leave = new Array<CurrentMonthPaymentsDto>();
  }

  SearchBetween(){
    this.service.SearchLeaveBetween(this.SearchLeaveBetween).subscribe(res=>{
    this.searchlist=res.items
    })
  }

  SearchDepartmentLeave(event){
    this.DepartmentLeaveSearch.id=event
    this.service.SearchDepartmentLeaveReport(this.DepartmentLeaveSearch).subscribe(res=>{
      this.DepartmentLeaveSearchList=res.items
      console.log(this.DepartmentLeaveSearchList)
    })
  }
}
