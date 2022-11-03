import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { StudentRoutingModule } from './student-routing.module';
import { StudentHomeComponent } from './student-home/student-home.component';
import { AddStudentComponent } from './add-student/add-student.component';
import { ImportStudentComponent } from './import-student/import-student.component';
import { StudentComponent } from './student/student.component';
import { SharedModule } from 'src/app/shared/SharedModule.module';
import { ImportStudentUploadComponent } from './import-student-upload/import-student-upload.component';
import { InviteStudentsComponent } from './invite-students/invite-students.component';
import { ImportStudentUploadingComponent } from './import-student-uploading/import-student-uploading.component';


@NgModule({
  declarations: [

    StudentHomeComponent,
    AddStudentComponent,
    ImportStudentComponent,
    StudentComponent,
    ImportStudentUploadComponent,
    InviteStudentsComponent,
    ImportStudentUploadingComponent
  ],
  imports: [
    CommonModule,
    StudentRoutingModule,
    SharedModule
  ]
})
export class StudentModule { }
