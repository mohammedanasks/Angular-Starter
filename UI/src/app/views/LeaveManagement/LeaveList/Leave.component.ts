import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EmployeeDto } from 'app/dtos/Master/employeeDto';
import { EmployService } from 'app/services/Master/Employ.service';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { LeaveDto } from 'app/dtos/LeaveManagement/leaveDto';
import { LeaveService } from 'app/services/Leave/Leave.service';
import { PageEvent } from '@angular/material/paginator';
@Component({
  selector: 'Leave',
  templateUrl: './Leave.component.html',
  styleUrls: ['./Leave.component.css'],
})
export class LeaveComponent implements OnInit {
  displayedColumns: string[] = ['Id', 'Name', 'Department', 'Type', 'FromDate', 'ToDate', 'Actions'];

  Leave: LeaveDto[];
  GivenData:LeaveDto[];

  constructor(private dialog: MatDialog,
     private service: EmployService,
     private leaveService : LeaveService ,
     private route: ActivatedRoute,) {}

  ngOnInit() {
  
  this.getlevelist()

    this.route.params.subscribe((par) => {
      if (par.id != null && par.id != 0 && par.id != undefined) {
        //get item detail from api
        // then bind to model
      } else {
        this.Leave = new Array<LeaveDto>();
        this.GivenData=new Array<LeaveDto>();
      }
    });
  }

  
  getlevelist(){
    this.leaveService.GetLeaves().subscribe((res) => {
      this.Leave = res.items
      this.GivenData=this.Leave.slice(0,5)
    

  
    });
  }

  DeleteLeave(id: number) {
    this.leaveService.DeleteLeave(id).subscribe(res => {
      if(res.isOk==true){
        this.getlevelist()
        this.Leave = new Array<LeaveDto>();
      }
    });
  }

  OnpageChange(event:PageEvent){
    console.log(event)
    const startIndex=event.pageIndex*event.pageSize;
    let EndIndex= startIndex+event.pageSize;
    if(EndIndex>this.Leave.length){
      EndIndex=this.Leave.length;
    }
    this.GivenData=this.Leave.slice(startIndex,EndIndex);
  }
}
