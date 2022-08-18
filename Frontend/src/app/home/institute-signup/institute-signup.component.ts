import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-institute-signup',
  templateUrl: './institute-signup.component.html',
  styleUrls: ['./institute-signup.component.css']
})
export class InstituteSignupComponent implements OnInit {
  instituteSignUpForm = {} as FormGroup;
  formSubmitted = false;
  
  constructor(
    private formBuilder: FormBuilder,
    private accountService: AccountService
  ) { }

  ngOnInit(): void {
    this.createSignUpForm();
  }

  createSignUpForm(){
    this.instituteSignUpForm = this.formBuilder.group({
			firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      emailId: ['', Validators.required],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required],
      instituteName: ['', Validators.required],
      pageName: ['', Validators.required],
		});
  }

  onSignUpSubmit() {
    this.formSubmitted = true;
    if(this.instituteSignUpForm.invalid) return;

		let data = JSON.stringify(this.instituteSignUpForm.value);
		console.log('-----SignUp JSON Format-----');
		console.log(data);
    // APIs

    this.resetTeamForm();
	}
	resetTeamForm() {
		this.instituteSignUpForm.reset();
	}
}
