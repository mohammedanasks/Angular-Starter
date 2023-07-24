import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { DepartmentDto } from 'app/dtos/Master/departmentDto';
import { DepartmentService } from 'app/services/Master/Department.service';
import { DeleteConfirmationComponent } from '../../employe/Deleteconfirmation/deleteconfirmation.component';

@Component({
  selector: 'ListDepartment',
  templateUrl: './ListDepartment.component.html',
  styleUrls: ['./ListDepartment.component.css']
})
export class ListDepartmentsComponent implements OnInit {
  displayedColumns: string[] = ['Id','Name','Head','Contact','Actions'];

  Departments:DepartmentDto[]
    
  DeleteStatus:Boolean=false

  constructor(private service: DepartmentService, private dialog: MatDialog) { }

  ngOnInit() {
   
    this.getDepartments()

    this.Departments=new Array<DepartmentDto>();
  }

  getDepartments(){
    this.service.GetDepartments().subscribe(x=>{
      this.Departments=x.items
    })
  }

  DeleteDepartment(id:number){
    this.service.DeleteDepartment(id).subscribe(res=>{
        this.getDepartments()    
    })
  }

  openDialog(id:any) {
    const dialogRef = this.dialog.open(DeleteConfirmationComponent ,{
      data:{
        depId:id,
        depmessage: 'Are you sure to delete this Department ?',
        buttonText: {
          ok: 'Yes',
          cancel: 'No'
        }
      }
    });
    dialogRef.afterClosed().subscribe((confirmed: boolean) => {
      if (confirmed) {
      this.service.GetDepartments().subscribe(res=>{
      this.Departments=res.items
      this.DeleteStatus=true
    });
        const a = document.createElement('a');
        a.click();
        a.remove();
      }
    });
  }
  
}


