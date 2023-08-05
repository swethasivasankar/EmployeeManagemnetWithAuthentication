import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { LayoutComponent } from './layout.component';
import { EmployeeComponent } from './employee.component';
import { EmployeeRoutingModule } from './employee-routing.module';
import { EmployeeBYIDComponent } from './employeeById.component';
import { EditemployeeComponent } from './editemployee.component';
import { AddemployeeComponent } from './addemployee.component';

@NgModule({
    imports: [
        CommonModule,
        ReactiveFormsModule,
        EmployeeRoutingModule,
       ],
    declarations: [
        LayoutComponent,
        EmployeeComponent,
        AddemployeeComponent,
        EmployeeBYIDComponent,
        EditemployeeComponent
    ]
})
export class EmployeeModule { }