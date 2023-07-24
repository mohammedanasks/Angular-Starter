import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LeaveComponent } from './LeaveList/Leave.component';


import { TakeLeaveComponent } from './TakeLeave/TakeLeave.component';






const routes: Routes = [
    {
        path: 'LeaveList',
        component:LeaveComponent
    },
    {
      path: 'TakeLeave',
      component:TakeLeaveComponent
  },
  {
    path: 'TakeLeave/:id',
    component:TakeLeaveComponent
},


];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LeaveRouting { }
