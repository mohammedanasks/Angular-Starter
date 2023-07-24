import { Routes } from '@angular/router';
import { AdminLayoutComponent } from './shared/components/layouts/admin-layout/admin-layout.component';
import { AuthLayoutComponent } from './shared/components/layouts/auth-layout/auth-layout.component';
import { AuthGuard } from './shared/guards/auth.guard';

export const rootRouterConfig: Routes = [
  {
    path: '',
    redirectTo: 'others/login',
    pathMatch: 'full'
  },
  {
    path: '',
    component: AuthLayoutComponent,
    children: [
      {
        path: 'sessions',
        loadChildren: () => import('./views/sessions/sessions.module').then(m => m.SessionsModule),
        data: { title: 'Session'}
      }
    ]
  },
  {
    path: '',
    component: AdminLayoutComponent,
    canActivate: [AuthGuard],
    children: [
      {
        path: 'dashboard',
        loadChildren: () => import('./views/dashboard/dashboard.module').then(m => m.DashboardModule),
        data: { title: 'Dashboard', breadcrumb: 'DASHBOARD'}
      },
      {
        path: 'others',
        loadChildren: () => import('./views/others/others.module').then(m => m.OthersModule),
        data: { title: 'Others', breadcrumb: 'OTHERS'}
      },

      {
        path: 'search',
        loadChildren: () => import('./views/search-view/search-view.module').then(m => m.SearchViewModule)
      },
      {
      path:'Masters',
      loadChildren:()=> import('./views/Master/Master.module').then(m=>m.MasterModule)
      },
      {
      path:'LeaveManagement',
      loadChildren:()=>import('./views/LeaveManagement/Leave.module').then(m=>m.LeaveModule)
      },
      {
      path:'SalaryManagement',
      loadChildren:()=>import('./views/SaleryManagement/Salary.module').then(m=>m.SalaryModule)
      },

      {
        path:'Reports',
        loadChildren:()=>import('./views/Reports/Reports.module').then(m=>m.ReportsModule)
        },
  

    ]
  },
  {
    path: '**',
    redirectTo: 'sessions/404'
  }
];

