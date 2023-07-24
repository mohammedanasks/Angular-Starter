import { Component, OnInit, Output } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EmployeeDto } from 'app/dtos/Master/employeeDto';
import { EmployService } from 'app/services/Master/Employ.service';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { LeaveDto } from 'app/dtos/LeaveManagement/leaveDto';
import { LeaveService } from 'app/services/Leave/Leave.service';
import { SalaryPaymentDto } from 'app/dtos/SaleryManagement/salaryPaymentDto';
import { SalaryService } from 'app/services/Salary/Salary.service';
import { CurrentMonthPaymentsDto } from 'app/dtos/Report/currentMonthPaymentsDto';
import { PageEvent } from '@angular/material/paginator';
import { A } from '@angular/cdk/keycodes';
import { EventEmitter } from '@angular/core';


@Component({
  selector: 'PayList',
  templateUrl: './PayList.component.html',
  styleUrls: ['./PayList.component.css'],
})
export class PayListComponent implements OnInit {
  displayedColumns: string[] = [
    'Id',
    'Name',
    'Department',
    'Amount',
    'Tax',
    'LeaveCount',
    'paidLeaveCount',
    'Reduction',
    'NetSalary',
    'Actions'
  ];

 Employs:CurrentMonthPaymentsDto [];
 GivenData:CurrentMonthPaymentsDto[];
  staus: number;
  checkdata:string;

  All:number=0;
  Paid:number=0;
  NotPaid:number=0;


  constructor(private dialog: MatDialog, private service: SalaryService, private route: ActivatedRoute) {}

  ngOnInit() {
    this.service.GetEmploys().subscribe((res) => {
      this.Employs = res.items
      this.GivenData=this.Employs.slice(0,5)
     
    this.All=this.Employs.length;
    var paidcount=this.Employs.filter(x=>x.isPaid==true).length;
    this.Paid=paidcount;
    var Notpaidcount=this.Employs.filter(x=>x.isPaid!=true).length;
    this.NotPaid=Notpaidcount;
    });

    this.Employs = new Array<CurrentMonthPaymentsDto>();
    this.GivenData=new Array<CurrentMonthPaymentsDto>();
  }

  DeletePayment(id: number) {
    this.service.DeleteSalary(id).subscribe((res) => {
      if (res.isOk == true) {
        this.Employs = new Array<CurrentMonthPaymentsDto>();
      }
    });
  }

  OnpageChange(event:PageEvent){
    console.log(event)
    const startIndex=event.pageIndex*event.pageSize;
    let EndIndex= startIndex+event.pageSize;
    if(EndIndex>this.Employs.length){
      EndIndex=this.Employs.length;
    }
    this.GivenData=this.Employs.slice(startIndex,EndIndex);
  }
   
  SearchCheckboxValue:string ="All";
  @Output()
  FilterChange : EventEmitter <string> = new EventEmitter <string>();

  Radiochange(){
    this.FilterChange.emit(this.SearchCheckboxValue);
    if(this.SearchCheckboxValue=='Paid'){
      var paiddata=this.Employs.filter(x=>x.isPaid==true);
      this.GivenData=paiddata.slice(0,5)
    }
    if(this.SearchCheckboxValue=='All'){
      this.GivenData=this.Employs.slice(0,5)
    }
    if(this.SearchCheckboxValue=='NotPaid'){
      var NotPaid=this.Employs.filter(x=>x.isPaid!=true);
      this.GivenData=NotPaid.slice(0,5)
    }
  }

}
