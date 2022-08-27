import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { FormService } from 'src/app/core/form.service';
import { ApiResponse } from 'src/app/_models/response';
import { InstituteService } from 'src/app/_services/institute.service';

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
    private instituteService: InstituteService,
    private route: Router,
    private formService: FormService,
  ) { }

  ngOnInit(): void {
    this.createSignUpForm();
  }

  createSignUpForm() {
    this.instituteSignUpForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      mobile: ['', Validators.required],
      emailId: ['', Validators.required],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required],
      instituteName: ['', Validators.required],
      pageName: [''],
    });
  }

  onSignUpSubmit() {
    this.formSubmitted = true;
    this.formService.markFormGroupTouched(this.instituteSignUpForm);
    if (this.instituteSignUpForm.invalid) return;

    const msgValidation = this.formValidation();
    if (msgValidation != "") {
      alert(msgValidation);
      return;
    }
    
    let data = JSON.stringify(this.instituteSignUpForm.value);
    console.log('-----SignUp JSON Format-----');
    console.log(data);
    // APIs
    this.instituteService.saveInstitute(this.instituteSignUpForm.value)
      .then((res: ApiResponse) => {
        if (res.MessageType == 0) {
          alert(res.Message);
          // navigate to verification link if signUp goes well
          this.route.navigate(['institute/verify-account']);
          this.resetTeamForm();
        } else {
          alert(res.Message);
        }
      });
  }

  formValidation(): string {
    let msg = "";

    const password = this.instituteSignUpForm.controls["password"].value;
    const confirmPassword = this.instituteSignUpForm.controls["confirmPassword"].value;

    if (password != confirmPassword)
      msg = "Confirm password not matched.";
    return msg;
  }

  resetTeamForm() {
    this.instituteSignUpForm.reset();
  }
}