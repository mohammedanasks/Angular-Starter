import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LeaveTypesComponent } from './leave-types.component';




const routes: Routes = [
    {
        path: 'AddLeaveType',
        component:LeaveTypesComponent
    }


 
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LeaveTypeRouting { }
