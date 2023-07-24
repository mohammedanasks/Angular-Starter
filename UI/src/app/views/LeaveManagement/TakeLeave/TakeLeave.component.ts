import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EmployeeDto } from 'app/dtos/Master/employeeDto';
import { EmployService } from 'app/services/Master/Employ.service';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { LeaveDto } from 'app/dtos/LeaveManagement/leaveDto';
import { LeaveService } from 'app/services/Leave/Leave.service';
import { LeaveTypeDto } from 'app/dtos/Master/LeaveTypeDto';
@Component({
  selector: 'TakeLeave',
  templateUrl: './TakeLeave.component.html',
  styleUrls: ['./TakeLeave.component.css']
})
 export class TakeLeaveComponent implements OnInit {                 

  tittle:boolean

  employs:EmployeeDto[]
  Leave : LeaveDto
  LeaveType:LeaveTypeDto[]
  
  message:Boolean=false
  messagedata:any
  audio= new Audio();

  NameRequired:string
  namereq:Boolean=false

  LeaveTypeRequired:string
  leavereq:Boolean=false

  DateRequired:string
  dateReq:Boolean=false        

  constructor( 
    private service:EmployService,
    private route: ActivatedRoute,
    private leaveService:LeaveService,
    private Routing : Router) { }

  ngOnInit() {
    this.tittle=true

    this. audio.src="../assets/erro2.mp3"
    this.audio.load()

    this. GetEditLeave()
  
    this.leaveService.GetLeaveType().subscribe(res=>{
    this.LeaveType=res.items
    })
    this.service.GetEmploys().subscribe(res=>{
      this.employs=res.items
    });


    this.route.params.subscribe(par => {
      if(par.id != null && par.id != 0 && par.id != undefined) {
        //get item detail from api
        // then bind to model
      }else {
        this.employs = new Array <EmployeeDto>();
        this.Leave =new LeaveDto;
      }
     })
  }

  GetEditLeave(){
    
    this. leaveService.GetEditLeave(this.route.snapshot.params.id).subscribe(x => {
      this.Leave = x.item;
      this.tittle = false;
    })
  }

  TakeLeave(){
    this.leaveService.AddLeave(this.Leave).subscribe(res=>{
        if(res.isOk==true){
          this.Leave =new LeaveDto;
          this.Routing.navigate(['/LeaveManagement/LeaveList']);
        }else{
          this.audio.play()
          console.log(res.item)
           var data=res.item
            if(data.nameRequired){
              this.NameRequired=data.nameRequired
              this.namereq=true
            }
            if(data.leaveTypeRequired){
              this.LeaveTypeRequired=data.leaveTypeRequired
              this.leavereq=true

            }
            if(data.dateRequired){
              this.DateRequired=data.dateRequired
              this.dateReq=true
            }
          
          
          this.message=true
          this.messagedata=res.message
        }
    })
  }

  UpdateDeLeave(){
    debugger
    this.leaveService.UpdateLeave(this.Leave).subscribe(x=>{
      if(x.isOk==true){
        this.Leave = new LeaveDto ;
        this.Routing.navigate(['/LeaveManagement/LeaveList']);
      }
    })
  }  

}
