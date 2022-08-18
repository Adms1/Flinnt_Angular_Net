import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomeRoutingModule } from './home-routing.module';
import { LoginComponent } from './login/login.component';
import { UserSignupComponent } from './user-signup/user-signup.component';
import { InstituteSignupComponent } from './institute-signup/institute-signup.component';
import { AccountService } from '../services/account.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../shared/SharedModule.module';

@NgModule({
  declarations: [
    LoginComponent,
    UserSignupComponent,
    InstituteSignupComponent
  ],
  imports: [
    CommonModule,
    HomeRoutingModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule
  ],
  exports:[
    
  ],
  providers:[
    AccountService
  ]
})
export class HomeModule { }
