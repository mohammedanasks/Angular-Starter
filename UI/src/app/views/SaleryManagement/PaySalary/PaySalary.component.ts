import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EmployeeDto } from 'app/dtos/Master/employeeDto';
import { EmployService } from 'app/services/Master/Employ.service';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { LeaveDto } from 'app/dtos/LeaveManagement/leaveDto';
import { LeaveService } from 'app/services/Leave/Leave.service';
import { SalaryPaymentDto } from 'app/dtos/SaleryManagement/salaryPaymentDto';
import { SalaryService } from 'app/services/Salary/Salary.service';
import { CurrentMonthPaymentsDto } from 'app/dtos/Report/currentMonthPaymentsDto';
import { GetEmploysForPaymentDto } from 'app/dtos/SaleryManagement/getEmploysForPaymentDto copy';
import { DeleteConfirmationComponent } from 'app/views/Master/employe/Deleteconfirmation/deleteconfirmation.component';

@Component({
  selector: 'PaySalary',
  templateUrl: './PaySalary.component.html',
  styleUrls: ['./PaySalay.component.css'],
})
export class PaySalaryComponent implements OnInit {
  status: boolean;
  Salary: SalaryPaymentDto;
  Employ: GetEmploysForPaymentDto;
  id: number;
  PayStatus: Boolean = false;
  LastName: string;

  audio = new Audio();
  errorAudio= new Audio();

  message: string;
  DialogOpen: Boolean = false;




  constructor(
    private dialog: MatDialog,
    private route: ActivatedRoute,
    private SalaryService: SalaryService,
    private Routing: Router
  ) {}

  ngOnInit() {
    this.audio.src = '../assets/mp3.wav';
    this.audio.load();
        
   this. errorAudio.src="../assets/erro2.mp3"
   this.errorAudio.load()

    this.SalaryService.GetEmploForPayment(this.route.snapshot.params.id).subscribe((x) => {
      this.Employ = x.item;
    });

    this.Salary = new SalaryPaymentDto();
    this.Employ = new GetEmploysForPaymentDto();

    this.route.params.subscribe((par) => {
      if (par.id != null && par.id != 0 && par.id != undefined) {
        //get item detail from api
        // then bind to model
      } else {
      }
    });
  }

  PaySalary() {
    this.Salary.employeeName = this.Employ.lastName;
    this.Salary.deduction = this.Employ.deduction;
    this.Salary.tax = this.Employ.tax;
    this.Salary.leave = this.Employ.leaveCount;
    this.Salary.netAmount = this.Employ.netAmount;
    this.Salary.date = this.Employ.date;
    this.Salary.employeeId = this.Employ.employeeId;
    this.Salary.basicSalary = this.Employ.basicSalary;
    this.SalaryService.AddSalary(this.Salary).subscribe((res) => {
      if (res.isOk == true) {
        this.audio.play();
        this.PayStatus = true;
        this.Employ = new GetEmploysForPaymentDto();
      } else {
        this.errorAudio.play()
        var data = res.items;
        data.forEach((x) => {
          this.message = x.allReadyPaid;
          if(x.allReadyPaid){
            this.openDialog();
          }
    
        });
      }
    });
  }

  openDialog() {
    const dialogRef = this.dialog.open(DeleteConfirmationComponent, {
      data: {
        AllReadyPaidMessage: this.message,
        buttonText: {
          ok: 'Ok',
          cancel: 'Ok',
        },
      },
    });
    dialogRef.afterClosed().subscribe((confirmed: boolean) => {
      if (confirmed) {
        this.Employ = new GetEmploysForPaymentDto();
        const a = document.createElement('a');
        a.click();
        a.remove();
      }
    });


  }
}
