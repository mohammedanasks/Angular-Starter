import { Component, Inject, Injectable, OnInit,Output,EventEmitter } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { EmployeeDto } from 'app/dtos/Master/employeeDto';
import { DepartmentService } from 'app/services/Master/Department.service';
import { EmployService } from 'app/services/Master/Employ.service';


@Component({
  selector: 'app-alertcomponet',
  templateUrl: './deleteconfirmation.component.html',
  styleUrls: ['./deleteconfirmatio.component.css']
})
export class DeleteConfirmationComponent implements OnInit {
   
  DeleteConfirmation:Boolean=false
  Id:any
  depId:any
  dep:Boolean=false
  emp:Boolean=false
  depmessage:string=""

  AllReadyPaidMsg:string
  AllReadyButton:Boolean=false

  message: string = "Are you sure?"
  confirmButtonText = "Yes"
  cancelButtonText = "Cancel"
  constructor(private service:EmployService,private depservice: DepartmentService,
    @Inject(MAT_DIALOG_DATA) private data: any,
    private dialogRef: MatDialogRef<DeleteConfirmationComponent >) {
      this.Id=data.Id
      this.depId=data.depId
    

    if(data.AllReadyPaidMessage){
      this.AllReadyButton=true
      this.AllReadyPaidMsg=data.AllReadyPaidMessage || this.AllReadyPaidMsg
    }
    if(data.Id){
      this.emp=true
    this.message = data.message || this.message;
    }
    if(data.depId){
      this.dep=true
      this.depmessage = data.depmessage || this.depmessage;
      }
    if (data.buttonText) {
      this.confirmButtonText = data.buttonText.ok || this.confirmButtonText;
      this.cancelButtonText = data.buttonText.cancel || this.cancelButtonText;
    }
      
  }

  onConfirmClick(): void {
    if(this.Id){
      this.service.DeleteEmploy(this.Id).subscribe(res=>{
      })
    }
  if(this.depId){
    this.depservice.DeleteDepartment(this.depId).subscribe(res=>{
    })
  }
  
    this.dialogRef.close(true);
  }

 

  // DeleteEmploy(id:number){
  //   if(this.DeleteConfirmation==true){
  //     this.service.DeleteEmploy(id).subscribe(res=>{

  //     })
  //   }
   
  // }

  ngOnInit():void {

  }

}
