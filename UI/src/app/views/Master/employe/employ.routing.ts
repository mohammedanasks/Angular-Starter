import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddEmployComponent } from './Add-employ/Add-employ.component';
import { DevexComponent } from './devex/devex.component';

import { EmployComponent } from './List-employs/employ.component';




const routes: Routes = [
    {
      path: 'EmployeesList',
      component:EmployComponent
    },

    {
      path: 'DexEmployeesList',
      component:DevexComponent
    },

    {
      path: 'AddEmployee',
      component:AddEmployComponent
  },
  {
      path: 'AddEmployee/:id',
      component:AddEmployComponent
},

 
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EmployRouting { }
