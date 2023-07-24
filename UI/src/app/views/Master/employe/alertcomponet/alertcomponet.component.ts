import { Component, Inject, Injectable, OnInit,Output,EventEmitter } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { EmployeeDto } from 'app/dtos/Master/employeeDto';


@Component({
  selector: 'app-alertcomponet',
  templateUrl: './alertcomponet.component.html',
  styleUrls: ['./alertcomponet.component.css']
})
export class AlertComponent implements OnInit {

  
  message: string;
  cancelButtonText = "Cancel";

  employ:EmployeeDto=new EmployeeDto();
 

  // @Output() event = new EventEmitter<string>()

  constructor(private dialog: MatDialog,  private route: ActivatedRoute,
   
    @Inject(MAT_DIALOG_DATA) private data: any,
    private dialogRef: MatDialogRef<AlertComponent>) {
  
      Object.assign(this.employ=data.datas) 
      console.log(this.employ)
      if (data.buttonText) {
        this.cancelButtonText = data.buttonText.cancel || this.cancelButtonText;
      }
    
    this.dialogRef.updateSize('300vw','300vw')
  }

  ngOnInit():void {
    this.route.params.subscribe((par) => {
      if (par.id != null && par.id != 0 && par.id != undefined) {
        //get item detail from api
        // then bind to model
      } else {
        
      }
    });

  }
  onConfirmClick(): void {
    this.dialogRef.close(true);
  }

}
