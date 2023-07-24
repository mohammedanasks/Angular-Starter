import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EmployeeDto } from 'app/dtos/Master/employeeDto';
import { EmployService } from 'app/services/Master/Employ.service';
import { AlertComponent } from '../alertcomponet/alertcomponet.component';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { BooleanInput } from '@angular/cdk/coercion';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DeleteConfirmationComponent } from '../Deleteconfirmation/deleteconfirmation.component';
import { count } from 'rxjs/operators';

@Component({
  selector: 'app-employ',
  templateUrl: './employ.component.html',
  styleUrls: ['./employ.component.css'],
})
export class EmployComponent implements OnInit {

  DeleteStatus: Boolean = false;
  deleteConformation: Boolean = false;
  deleteconfirmationdiv: Boolean = false;

  employs: EmployeeDto[];
  GivenData: EmployeeDto[];
  search:any[]

  SearchData:string=""
  SearchStatus:Boolean=false

  displayedColumns: string[] = ['Id', 'Name', 'Department', 'Designation', 'Actions'];

  constructor(
    private snackBar: MatSnackBar,
    private dialog: MatDialog,
    private service: EmployService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.service.GetEmploys().subscribe((res) => {
      this.employs = res.items;
      this.GivenData = this.employs.slice(0, 5);
   


    });


    this.route.params.subscribe((par) => {
      if (par.id != null && par.id != 0 && par.id != undefined) {
        //get item detail from api
        // then bind to model
      } else {
        this.employs = new Array<EmployeeDto>();
        this.GivenData = new Array<EmployeeDto>();

      }
    });
  }

  getdata(data: any[]) {
    const dialogRef = this.dialog.open(AlertComponent, {
      data: {
        datas: data,
      },
    });
  }
  SearchEvent(){
    this.GivenData.forEach(x=> {

      if(this.SearchData!=x.lastName){
        this.SearchStatus=true;
      }if(this.SearchData==x.lastName){
         
        this.SearchStatus=false
      }if(this.SearchData==""){
          this.SearchStatus=false
      }
    })
  
    
  }

  deleteconfirm() {
    this.deleteConformation = true;
  }

  OnpageChange(event: PageEvent) {
    console.log(event);
    const startIndex = event.pageIndex * event.pageSize;
    let EndIndex = startIndex + event.pageSize;
    if (EndIndex > this.employs.length) {
      EndIndex = this.employs.length;
    }
    this.GivenData = this.employs.slice(startIndex, EndIndex);
  }

  openDialog(id: any) {
    const dialogRef = this.dialog.open(DeleteConfirmationComponent, {
      data: {
        Id: id,
        message: 'Are you sure to delete this Employee ?',
        buttonText: {
          ok: 'Yes',
          cancel: 'No',
        },
      },
    });
    dialogRef.afterClosed().subscribe((confirmed: boolean) => {
      if (confirmed) {
        this.service.GetEmploys().subscribe((res) => {
          this.employs = res.items;
          this.GivenData = this.employs.slice(0, 5);
          this.DeleteStatus = true;
        });
        const a = document.createElement('a');
        a.click();
        a.remove();
      }
    });
  }
}
