import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Constants } from 'src/app/_helpers/constants';
import { ApiResponse } from 'src/app/_models/response';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { UtilityService } from 'src/app/_services/utility.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm = {} as FormGroup;
  formSubmitted = false;
  constructor(
    private formBuilder: FormBuilder,
    private utilityService: UtilityService,
    private authService : AuthenticationService,
    private route: Router
  ) { }

  ngOnInit(): void {
    this.utilityService.showLoading();
    this.createLoginForm()
  }
  
  ngAfterViewInit(){
    this.utilityService.hideLoading();
  }

  createLoginForm(){
    this.loginForm = this.formBuilder.group({
			email: ['', Validators.required],
      passWord: ['', Validators.required]
		});
  }

  onLoginSubmit() {
    this.formSubmitted = true;
    if(this.loginForm.invalid) return;

		let data = JSON.stringify(this.loginForm.value);
		console.log('-----Login in JSON Format-----');
		console.log(data);
    // APIs
    this.authService.doLogin(this.loginForm.value)
      .then((res: ApiResponse) => {
        if (res.statusCode == 200) {
          // navigate to verification link if verification not done OR configuration page OR dashboard
          
          localStorage.setItem(Constants.TOKEN, JSON.stringify(res.data.token));
          localStorage.setItem(Constants.LOGIN_PAGE.USER_OBJ, JSON.stringify(res.data.applicationUser));
          if(res.data.applicationUser.isVerified){
            this.route.navigate(['institute/configure']);
          }
          else{
            this.route.navigate(['institute/verify-account']);
          }
          
          this.resetTeamForm();
        }
      });
	}
	resetTeamForm() {
		this.loginForm.reset();
	}
}
