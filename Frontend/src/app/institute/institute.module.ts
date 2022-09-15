import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { InstituteRoutingModule } from './institute-routing.module';
import { InstituteConfigureComponent } from './institute-configure/institute-configure.component';


@NgModule({
  declarations: [
    InstituteConfigureComponent
  ],
  imports: [
    CommonModule,
    InstituteRoutingModule
  ]
})
export class InstituteModule { }
