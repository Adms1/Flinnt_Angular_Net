import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { StudentRoutingModule } from './student-routing.module';
import { StudentHomeComponent } from './student-home/student-home.component';
import { AddStudentComponent } from './add-student/add-student.component';
import { ImportStudentComponent } from './import-student/import-student.component';
import { StudentComponent } from './student/student.component';
import { SharedModule } from 'src/app/shared/SharedModule.module';


@NgModule({
  declarations: [

    StudentHomeComponent,
    AddStudentComponent,
    ImportStudentComponent,
    StudentComponent
  ],
  imports: [
    CommonModule,
    StudentRoutingModule,
    SharedModule
  ]
})
export class StudentModule { }
