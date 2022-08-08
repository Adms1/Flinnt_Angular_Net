import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomeRoutingModule } from './home-routing.module';
import { LoginComponent } from './login/login.component';
import { commonModule } from '../common-module/common-module.module';
import { UserSignupComponent } from './user-signup/user-signup.component';
import { InstituteSignupComponent } from './institute-signup/institute-signup.component';

@NgModule({
  declarations: [
    LoginComponent,
    UserSignupComponent,
    InstituteSignupComponent
  ],
  imports: [
    CommonModule,
    HomeRoutingModule,
    commonModule
  ],
  exports:[
  ]
})
export class HomeModule { }
