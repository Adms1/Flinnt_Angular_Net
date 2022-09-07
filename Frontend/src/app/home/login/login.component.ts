import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
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
    private utilityService: UtilityService
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
			userName: ['', Validators.required],
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

    this.resetTeamForm();
	}
	resetTeamForm() {
		this.loginForm.reset();
	}
}
