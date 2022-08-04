import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomeRoutingModule } from './home-routing.module';
import { LoginComponent } from './login/login.component';
import { LoaderModule } from '../loader-module/loader-module.module';

@NgModule({
  declarations: [
    LoginComponent
  ],
  imports: [
    CommonModule,
    HomeRoutingModule,
    LoaderModule
  ],
  exports:[
  ]
})
export class HomeModule { }
