import { Component, OnInit } from '@angular/core';
import { LeaveTypeDto } from 'app/dtos/Master/LeaveTypeDto';
import { LeaveService } from 'app/services/Leave/Leave.service';
import { EmployService } from 'app/services/Master/Employ.service';

@Component({
  selector: 'app-leave-types',
  templateUrl: './leave-types.component.html',
  styleUrls: ['./leave-types.component.css']
})
export class LeaveTypesComponent implements OnInit {
  checked = false;

  leaveType: LeaveTypeDto

  constructor(private service : LeaveService) { }

  ngOnInit() {

    this.leaveType=new LeaveTypeDto
  }

AddLeaveType(){
  this.service.AddLeaveType(this.leaveType).subscribe(res=>{
    if(res.isOk==true){
      this.leaveType=new LeaveTypeDto
    }
  })
}

}
