import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DashboardRoutingModule } from './institute-dashboard-routing.module';
import { InstituteDashboardComponent } from './institute-dashboard.component';
import { SharedModule } from 'src/app/shared/SharedModule.module';


@NgModule({
  declarations: [
    InstituteDashboardComponent
  ],
  imports: [
    CommonModule,
    DashboardRoutingModule,
    SharedModule
  ]
})
export class InstituteDashboardModule { }
