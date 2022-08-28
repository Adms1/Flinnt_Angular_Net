import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UtilityService } from 'src/app/_services/utility.service';

@Component({
  selector: 'app-user-signup',
  templateUrl: './user-signup.component.html',
  styleUrls: ['./user-signup.component.css']
})
export class UserSignupComponent implements OnInit {
  signUpForm = {} as FormGroup;
  formSubmitted = false;
  constructor(
    private formBuilder: FormBuilder,
    private utilityService: UtilityService
  ) { }

  ngOnInit(): void {
    this.utilityService.showLoading();
    this.createSignUpForm();
  }

  ngAfterViewInit(){
    this.utilityService.hideLoading();
  }

  createSignUpForm(){
    this.signUpForm = this.formBuilder.group({
			firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      emailId: ['', Validators.required],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required]
		});
  }

  onSignUpSubmit() {
    this.formSubmitted = true;
    if(this.signUpForm.invalid) return;

		let data = JSON.stringify(this.signUpForm.value);
		console.log('-----SignUp JSON Format-----');
		console.log(data);
    // APIs

    this.resetTeamForm();
	}
	resetTeamForm() {
		this.signUpForm.reset();
	}
}
