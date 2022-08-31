import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { FormService } from 'src/app/core/form.service';
import { Constants } from 'src/app/_helpers/constants';
import { ApiResponse } from 'src/app/_models/response';
import { InstituteService } from 'src/app/_services/institute.service';
import { UtilityService } from 'src/app/_services/utility.service';

@Component({
  selector: 'app-institute-signup',
  templateUrl: './institute-signup.component.html',
  styleUrls: ['./institute-signup.component.css']
})
export class InstituteSignupComponent implements OnInit {
  instituteSignUpForm = {} as FormGroup;
  formSubmitted = false;
  //exampleData: Array<Select2OptionData>;
  constructor(
    private formBuilder: FormBuilder,
    private instituteService: InstituteService,
    private route: Router,
    private formService: FormService,
    private utilityService: UtilityService
  ) { }

  ngOnInit(): void {
    this.utilityService.showLoading();
    // this.exampleData = [
    //   {
    //     id: 'basic1',
    //     text: 'Basic 1'
    //   },
    //   {
    //     id: 'basic2',
    //     disabled: true,
    //     text: 'Basic 2'
    //   },
    //   {
    //     id: 'basic3',
    //     text: 'Basic 3'
    //   },
    //   {
    //     id: 'basic4',
    //     text: 'Basic 4'
    //   }
    // ];
    this.createSignUpForm();
  }

  ngAfterViewInit(){
    this.utilityService.hideLoading();
  }

  createSignUpForm() {
    this.instituteSignUpForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      mobileNo: ['', Validators.required],
      emailId: ['', Validators.required],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required],
      instituteName: ['', Validators.required]
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
        if (res.messageType == 0) {
          // navigate to verification link if signUp goes well
          localStorage.setItem(Constants.INSTITUTE_PAGE.INSTITUTE_OBJ, JSON.stringify(res.data));
          this.route.navigate(['institute/verify-account']);
          this.resetTeamForm();
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