import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { InstituteConfigureComponent } from './institute-configure/institute-configure.component';

const routes: Routes = [
  {
    path:"",
    component: InstituteConfigureComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class InstituteRoutingModule { }
