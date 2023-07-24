import { CUSTOM_ELEMENTS_SCHEMA, NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';

import { MatCardContent, MatCardModule } from '@angular/material/card';
import { SearchModule } from 'app/shared/search/search.module';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import {
  MatFormField,
  MatFormFieldControl,
  MatHint,
  MatLabel,
  MAT_FORM_FIELD_DEFAULT_OPTIONS,
} from '@angular/material/form-field';

import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInput, MatInputModule } from '@angular/material/input';
import { MatSelect, MatSelectModule } from '@angular/material/select';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDialogActions, MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { EmployComponent } from './List-employs/employ.component';
import { EmployRouting } from './employ.routing';
import { AddEmployComponent } from './Add-employ/Add-employ.component';
import { MatTableModule } from '@angular/material/table';
import { MatIcon, MatIconModule } from '@angular/material/icon';
import { AlertComponent } from './alertcomponet/alertcomponet.component';
import { LandingComponent } from '../Landing-page/Landing.component';
import { MatPaginatorModule } from '@angular/material/paginator';
import { DeleteConfirmationComponent } from './Deleteconfirmation/deleteconfirmation.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { DxDataGridModule } from 'devextreme-angular';

import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { DevexComponent } from './devex/devex.component';



@NgModule({
  declarations: [EmployComponent, AddEmployComponent, AlertComponent,DeleteConfirmationComponent,DevexComponent],
  bootstrap: [DevexComponent],
  imports: [
    MatInputModule,
    MatButtonModule,
    FlexLayoutModule,
    MatFormFieldModule,
    MatCardModule,
    MatSelectModule,
    CommonModule,
    MatDatepickerModule,
    EmployRouting,
    FormsModule,
    MatDialogModule,
    ReactiveFormsModule,
    MatTableModule,
    MatIconModule,
    MatCardModule,
    MatPaginatorModule,
    DxDataGridModule

  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA],
  providers: [
    { provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: { appearance: 'outline' } },
    [DatePipe],
    { provide: MAT_DIALOG_DATA, useValue: {} },
    { provide: MatDialogRef, useValue: {} },
  ],
})
export class EmployModule {}
platformBrowserDynamic().bootstrapModule(DevexComponent);