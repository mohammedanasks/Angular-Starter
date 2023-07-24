import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IdTextDto } from 'app/dtos/base/idTextDto';
import { DepartmentDto } from 'app/dtos/Master/departmentDto';
import { EmployeeDto } from 'app/dtos/Master/employeeDto';
import { DepartmentService } from 'app/services/Master/Department.service';

@Component({
  selector: 'AddDepartment',
  templateUrl: './AddDepartment.component.html',
  styleUrls: ['./AddDepartment.component.css'],
})
export class AddDepartmentComponent implements OnInit {
  employs: IdTextDto[];
  department: DepartmentDto;
  tittle: boolean;
  constructor(private service: DepartmentService, private route: ActivatedRoute,private routing:Router) {}

  ngOnInit() {
    this.tittle = true;
    this.service.GetEditDepartment(this.route.snapshot.params.id).subscribe((x) => {
      this.department = x.item;
      this.tittle = false;
    });
    this.GetEmploys();
    this.route.params.subscribe((par) => {
      if (par.id != null && par.id != 0 && par.id != undefined) {
        //get item detail from api
        // then bind to model
      } else {
        this.department = new DepartmentDto();
      }
    });
  }

  GetEmploys() {
    this.service.GetEmployNames().subscribe(res=> {
      this.employs = res.items;
      console.log(this.employs)
    });
  }

  AddDepartment() {
    this.service.AddDepartment(this.department).subscribe((res) => {
      if (res.isOk == true) {
        this.department = new DepartmentDto();
      } else {
        alert('file');
      }
    });
  }

  
  UpdateDepartment(){
    this.service.UpdateDepartment(this.department).subscribe(x=>{
      if(x.isOk==true){
        this.department = new DepartmentDto ;
        this.routing.navigate(['/Masters/Departments/DepartmentList']);
      }
    })
  }  
}
