import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatMenuModule } from '@angular/material/menu';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';
import { RouterModule } from '@angular/router';
import { FlexLayoutModule } from '@angular/flex-layout';
import { ChartsModule } from 'ng2-charts';
import { NgxEchartsModule } from 'ngx-echarts';
import * as echarts from 'echarts';

import { NgApexchartsModule } from 'ng-apexcharts';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { SharedPipesModule } from 'app/shared/pipes/shared-pipes.module';

import { DashboardRoutes } from './dashboard.routing';
import { AnalyticsComponent } from './analytics/analytics.component';
import { DashboardDarkComponent } from './dashboard-dark/dashboard-dark.component';
import { CryptocurrencyComponent } from './cryptocurrency/cryptocurrency.component';
import { DefaultDashboardComponent } from './default-dashboard/default-dashboard.component';
import { LearningManagementComponent } from './learning-management/learning-management.component';
import { AnalyticsAltComponent } from './analytics-alt/analytics-alt.component';
import { LandingComponent } from '../Master/Landing-page/Landing.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { LoginComponent } from '../Master/Login-page/login.component';

@NgModule({
  imports: [
    CommonModule,
    MatIconModule,
    MatCardModule,
    MatMenuModule,
    MatProgressBarModule,
    MatExpansionModule,
    MatButtonModule,
    MatChipsModule,
    MatListModule,
    MatTabsModule,
    MatTableModule,
    MatGridListModule,
    FlexLayoutModule,
    ChartsModule,
    NgbModule,
    NgxEchartsModule.forRoot({
      echarts
    }),
    NgApexchartsModule,
    NgxDatatableModule,
    SharedPipesModule,
    RouterModule.forChild(DashboardRoutes)
  ],
  declarations: [
    AnalyticsComponent,
    DashboardDarkComponent,
    CryptocurrencyComponent,
    DefaultDashboardComponent,
    LearningManagementComponent,
    LandingComponent,
    LoginComponent,
    AnalyticsAltComponent],
  exports: []
})
export class DashboardModule {

}