import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { InstituteSignupComponent } from './institute-signup/institute-signup.component';
import { LoginComponent } from './login/login.component';
import { UserSignupComponent } from './user-signup/user-signup.component';

const routes: Routes = [
  { 
    path: '', 
    component: LoginComponent
  },
  { 
    path: 'user/signup', 
    component: UserSignupComponent
  },
  { 
    path: 'institute/signup', 
    component: InstituteSignupComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeRoutingModule { }
