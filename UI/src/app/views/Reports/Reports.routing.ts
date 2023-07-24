import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CompanyReportComponent } from './Accounts/company.component';
import { DepartmentReportComponent } from './Department/LeaveReport.component';





const routes: Routes = [
    {
        path: 'EmployReports',
        component:CompanyReportComponent
    },
    {
      path: 'LeaveReports',
      component:DepartmentReportComponent
  }


];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReportRouting { }
