import { CUSTOM_ELEMENTS_SCHEMA, NgModule, NO_ERRORS_SCHEMA } from "@angular/core";
import { CommonModule, DatePipe } from "@angular/common";

import { MatCardModule } from "@angular/material/card";
import { SearchModule } from "app/shared/search/search.module";
import { NgxDatatableModule } from "@swimlane/ngx-datatable";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";

import { MatFormField, MatFormFieldControl, MatHint, MatLabel, MAT_FORM_FIELD_DEFAULT_OPTIONS } from "@angular/material/form-field";

import {MatFormFieldModule} from '@angular/material/form-field';
import { MatInput, MatInputModule } from "@angular/material/input";
import { MatSelect, MatSelectModule } from "@angular/material/select";
import { FlexLayoutModule } from "@angular/flex-layout";
import { MatButtonModule } from "@angular/material/button";
import { MatDatepickerModule } from "@angular/material/datepicker";
import { MatDialogActions, MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { MatTableModule } from "@angular/material/table";
import { MatIconModule } from "@angular/material/icon";

import { ReportRouting } from "./Reports.routing";

import { CompanyReportComponent } from "./Accounts/company.component";
import { DepartmentReportComponent } from "./Department/LeaveReport.component";


@NgModule({
  declarations: [CompanyReportComponent,DepartmentReportComponent],
  imports: [MatInputModule,MatButtonModule,FlexLayoutModule,MatFormFieldModule,MatCardModule
  ,MatSelectModule,CommonModule,MatDatepickerModule,ReportRouting,
   FormsModule,MatDialogModule,ReactiveFormsModule,MatTableModule,MatIconModule],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA,
    NO_ERRORS_SCHEMA
  ],
  providers: [
    {provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: {appearance: 'outline'}},
    [DatePipe],

      { provide: MAT_DIALOG_DATA, useValue: {} },
      { provide: MatDialogRef, useValue: {} } ,
  ]
  

})
export class ReportsModule {}



