import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../_services/auth.guard';
import { InstituteSignupComponent } from './institute-signup/institute-signup.component';
import { LoginComponent } from './login/login.component';
import { UserSignupComponent } from './user-signup/user-signup.component';
import { VarifyAccountComponent } from './varify-account/varify-account.component';

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
  },
  { 
    path: 'institute/verify-account', 
    component: VarifyAccountComponent,
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeRoutingModule { }
