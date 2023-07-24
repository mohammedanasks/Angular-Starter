import { Component, OnInit } from '@angular/core';
import { IdTextDto } from 'app/dtos/base/idTextDto';
import { DepartmentReportDto } from 'app/dtos/Report/DepartmentReportDto';
import { MonthlyReportDto } from 'app/dtos/Report/monthlyReportsDto';
import { YearlyReportDto } from 'app/dtos/Report/yearlyReportDto';

import { SalaryPaymentDto } from 'app/dtos/SaleryManagement/salaryPaymentDto';
import { ReportsService } from 'app/services/Report/Reports.service';
import { SalaryService } from 'app/services/Salary/Salary.service';
import { max } from 'date-fns';
import { last } from 'rxjs/operators';

interface month{
  name :string;
  id: number;

}

@Component({
  selector: 'company',
  templateUrl: './comapany.component.html',
  styleUrls: ['./company.component.css']
})

export class CompanyReportComponent implements OnInit {
  displayedColumns: string[] = ['Id','Name','Tax','Salary','Date','TotalSalary'];
  displayedCol: string[] = ['Months','Tax','Salary','Deduction','leave'];
  displayedColYear: string[] = ['Year','Tax','Salary','Deduction','leave'];

  Months:month[]=
  [
    
    {
      name:'JAN',
      id:1
    },
    {
      name:'FEB',
      id:2
    },
    {
      name:'MAR',
      id:3
    },
    {
      name:'APR',
      id:4
    },
    {
      name:'MAY',
      id:5
    },
    {
      name:'JUN',
      id:6
    },
    {
      name:'JUL',
      id:7
    },
    {
      name:'AUG',
      id:8
    },
    {
      name:'SEP',
      id:9
    },
    {
      name:'OCT',
      id:10
    },
    {
      name:'NOV',
      id:11
    },
    {
      name:'DEC',
      id:12
    }

]


  salary:SalaryPaymentDto[]
  total :number
  monthcount:number;
  serchsalary:SalaryPaymentDto;
  MonthlyRepo:DepartmentReportDto[];
  YearlyRepo:YearlyReportDto[];
  
  constructor(
    private reportService: ReportsService,

    ) {


     }

  ngOnInit() {
    this.reportService.GetMonthlyReport().subscribe(res=>{
      this.MonthlyRepo=res.items
      
    })

    this.reportService.GetYearlyReport().subscribe(res=>{
       this.YearlyRepo=res.items
      
    })

    this.reportService.GetLSalaryList().subscribe(res=>{
      this.salary=res.items
      this.total=Math.max.apply(Math, this.salary.map(function(o) { return o.total; }))
    })
    this.MonthlyRepo=new Array <DepartmentReportDto>();
    this.serchsalary=new SalaryPaymentDto
    this.salary = new Array <SalaryPaymentDto>();
    this.YearlyRepo=new Array<YearlyReportDto>();
  
  
  }
  searchByMonth(){
    debugger
     this.reportService.GetSearchList(this.monthcount).subscribe(res=>{
      this.salary=res.items
      this.total=Math.max.apply(Math, this.salary.map(function(o) { return o.total; }))
    
     })
  }

}
