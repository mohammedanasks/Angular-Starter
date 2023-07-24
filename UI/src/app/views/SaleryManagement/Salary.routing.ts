import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PayListComponent } from './ListPayments/PayList.component';
import { PaySalaryComponent } from './PaySalary/PaySalary.component';







const routes: Routes = [
    {
        path: 'PayList',
        component:PayListComponent
    },
    {
      path: 'PayList/:id',
      component:PayListComponent
  },
    {
      path: 'PaySalary/:id',
      component:PaySalaryComponent

    }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SalaryRouting { }
