import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute,  Router, } from '@angular/router';
import { DepartmentDto } from 'app/dtos/Master/departmentDto';
import { DesignationDto } from 'app/dtos/Master/desiginationDto';
import { EmployeeDto } from 'app/dtos/Master/employeeDto';
import { DepartmentService } from 'app/services/Master/Department.service';
import { EmployService } from 'app/services/Master/Employ.service';
import { is } from 'date-fns/locale';
import { EmployComponent } from '../List-employs/employ.component';


interface department{
  id: number;
  name : string;
}

@Component({
  selector: 'Add-employ',
  templateUrl: './Add-employ.component.html',
  styleUrls: ['./Add-employ.component.css']
})

export class AddEmployComponent implements OnInit {

  departments:DepartmentDto[]
  designations:DesignationDto[]
  employ:EmployeeDto
  tittle:boolean
  selectedfile:File=null;

  errorAudio= new Audio();

  FirstNameRequired:string;
  first:Boolean=false

  LastNameRequired:string;
  last :Boolean=false

  DesignationRequired:string;
  desig:Boolean=false

  ContactRequired:String;
  con:Boolean=false

  SalaryRequired:string;
  sal:Boolean=false

  DepartmentRequired:string;
  dep:Boolean=false

  constructor(private service  : EmployService, private Dservice: DepartmentService, 
    private routing : Router, private route: ActivatedRoute) { }

  ngOnInit() {

    
   this. errorAudio.src="../assets/erro2.mp3"
   this.errorAudio.load()
    this.tittle=true
    this.service.GetEditEmploy(this.route.snapshot.params.id).subscribe(x=>{
      this.employ=x.item
      this.tittle=false;
    })

    this.Dservice.GetDepartments().subscribe(res=>{
      this.departments=res.items
    })
    this.service.GetDesignations().subscribe(res=>{
      this.designations=res.items
    })

 
    this.route.params.subscribe(par => {
      if(par.id != null && par.id != 0 && par.id != undefined) {
        //get item detail from api
        // then bind to model
      }else {
        this.employ = new EmployeeDto;
        this.departments=new Array<DepartmentDto>();
        this.designations=new Array<DesignationDto>();
      }
     })


  }

  onfileselect(event){
      const file = event.target.files[0];
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = (event:any) => {
        this.employ.fileName=event.target.result
  };
    
  }

  AddEmploy(){
    this.service.AddEmploy(this.employ).subscribe(res=>{
      
      if(res.isOk==true){
        this.employ = new EmployeeDto;    
      }if(res.isOk==false){
        this.errorAudio.play()
        var data=res.items
        data.forEach(x=>{
          if(x.firstNameRequired){
            this.first=true
             this.FirstNameRequired=x.firstNameRequired
          }
          if(x.lastNameRequired){
            this.last=true
             this.LastNameRequired=x.lastNameRequired
          }
          if(x.designationRequired){
            this.desig=true
             this.DesignationRequired=x.designationRequired
          }
          if(x.contactRequired){
            this.con=true
             this.ContactRequired=x.contactRequired
          }
          if(x.departmentNameRequired){
            this.dep=true
             this.DepartmentRequired=x.departmentNameRequired
    
          }
          if(x.salaryRequired){
            this.sal=true
             this.SalaryRequired=x.salaryRequired
          }
        });
      }
    })
  }

  UpdateEmploy(){
    this.service.UpdateEmploy(this.employ).subscribe(x=>{
      if(x.isOk==true){
        this.employ = new EmployeeDto;
        this.routing.navigate(['/Masters/Employees/EmployeesList']);
      }
   
      
    })
  }

}
