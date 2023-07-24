import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddDepartmentComponent } from './add-department/AddDepartment.component';
import { ListDepartmentsComponent } from './ListDepartments/ListDepartment.component';





const routes: Routes = [
    {
        path: 'DepartmentList',
        component:ListDepartmentsComponent
    },

    {
      path: 'AddDepartment',
      component:AddDepartmentComponent
  },
  {
    path: 'AddDepartment/:id',
    component:AddDepartmentComponent
},
 
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DepartmentRouting { }
