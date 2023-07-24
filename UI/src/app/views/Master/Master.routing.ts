import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddDepartmentComponent } from './department/add-department/AddDepartment.component';
import { ListDepartmentsComponent } from './department/ListDepartments/ListDepartment.component';
import { EmployComponent } from './employe/List-employs/employ.component';

export const MasterRoutes: Routes = [
  {
    path: 'Departments',
   loadChildren:()=>import('./department/Department.module').then(m=>m.DepartmentModule)
  },
  {
    path: 'Employees',
    loadChildren:()=>import('./employe/employ.module').then(m=>m.EmployModule)
  },
  {
    path:'leaveType',
    loadChildren:()=>import('./leave-types/leave-type.module').then(m=>m.LeaveTypeModule)
  },

];
