import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { InstituteDashboardComponent } from './institute-dashboard.component';

const routes: Routes = [
  {
    path:"",
    component: InstituteDashboardComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardRoutingModule { }
