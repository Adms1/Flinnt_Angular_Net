import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Select2OptionData } from 'ng-select2';
import { FormService } from 'src/app/core/form.service';
import { Constants } from 'src/app/_helpers/constants';
import { ApiResponse } from 'src/app/_models/response';
import { CountryService } from 'src/app/_services/country.service';
import { InstituteService } from 'src/app/_services/institute.service';
import { StateService } from 'src/app/_services/state.service';
import { UtilityService } from 'src/app/_services/utility.service';

@Component({
  selector: 'app-institute-signup',
  templateUrl: './institute-signup.component.html',
  styleUrls: ['./institute-signup.component.css']
})
export class InstituteSignupComponent implements OnInit {
  instituteSignUpForm = {} as FormGroup;
  formSubmitted = false;
  stateData: Array<Select2OptionData>;
  countryData: Array<Select2OptionData>;
  countryId:string;
  constructor(
    private formBuilder: FormBuilder,
    private instituteService: InstituteService,
    private route: Router,
    private formService: FormService,
    private utilityService: UtilityService,
    private countryService: CountryService,
    private stateService: StateService
  ) { }

  ngOnInit(): void {
    this.utilityService.showLoading();
    this.getCountries();
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
      instituteName: ['', Validators.required],
      address: ['', Validators.required],
      cityName: ['', Validators.required],
      stateId: ['', Validators.required],
      countryId: ['', Validators.required],
      pincode: ['']
    });
  }

  getCountries() {
    this.countryService.getCountries()
      .then((res: ApiResponse) => {
        if (res.messageType == 0) {
          if (!!res.data) {
            this.countryData = res.data.map(x => {
              return {
                id: x.countryId,
                text: x.countryName
              }
            })
          }
        }
      });
  }

  getStates(countryId) {
    this.stateService.getStateByCountryId(Number(countryId))
      .then((res: ApiResponse) => {
        if (res.messageType == 0) {
          if (!!res.data) {
            this.stateData = res.data.map(x => {
              return {
                id: x.stateId,
                text: x.stateName
              }
            })
          }
        }
      });
  }
  countryValueChanged(countryId: string){
    if(!!countryId){
      if(countryId == this.countryId) return;
      this.countryId = countryId;

      //get state data
      this.getStates(countryId);
    }
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