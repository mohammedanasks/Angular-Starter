import { CUSTOM_ELEMENTS_SCHEMA, EventEmitter, NgModule, NO_ERRORS_SCHEMA } from "@angular/core";
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
import { PaySalaryComponent } from "./PaySalary/PaySalary.component";
import {SalaryRouting } from "./Salary.routing";
import {  PayListComponent } from "./ListPayments/PayList.component";
import { MatPaginator, MatPaginatorModule } from "@angular/material/paginator";
import { MatGridListModule } from "@angular/material/grid-list";
import { MatCheckboxModule } from "@angular/material/checkbox";
import { MatRadioModule } from "@angular/material/radio";








@NgModule({
  declarations: [PayListComponent,PaySalaryComponent],
  imports: [MatInputModule,MatButtonModule,FlexLayoutModule,MatFormFieldModule,MatCardModule
  ,MatSelectModule,CommonModule,MatDatepickerModule,SalaryRouting,
   FormsModule,MatDialogModule,ReactiveFormsModule,MatTableModule,MatIconModule,MatPaginatorModule,MatGridListModule,MatCheckboxModule,MatRadioModule],
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
export class SalaryModule {}



