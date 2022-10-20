import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddStudentComponent } from './add-student/add-student.component';
import { ImportStudentComponent } from './import-student/import-student.component';
import { StudentHomeComponent } from './student-home/student-home.component';
import { StudentComponent } from './student/student.component';

const routes: Routes = [
  {
    path: "",
    component: StudentHomeComponent,
    children:[
      {
        path: "",
        component: StudentComponent
      },
      {
        path: "add-student",
        component: AddStudentComponent
      },
      {
        path: "import-students",
        component: ImportStudentComponent
      }
    ]
  }, 
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StudentRoutingModule { }
