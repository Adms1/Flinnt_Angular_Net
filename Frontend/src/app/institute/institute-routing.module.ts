import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '../shared/SharedModule.module';
import { InstituteConfigureComponent } from './institute-configure/institute-configure.component';

const routes: Routes = [
  {
    path:"",
    component: InstituteConfigureComponent
  }
];

@NgModule({
  imports: [
    SharedModule,
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class InstituteRoutingModule { }
