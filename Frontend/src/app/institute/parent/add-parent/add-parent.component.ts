import { HttpStatusCode } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Select2OptionData } from 'ng-select2';
import { FormService } from 'src/app/core/form.service';
import { ApiResponse } from 'src/app/_models/response';
import { User } from 'src/app/_models/user';
import { CountryService } from 'src/app/_services/country.service';
import { ParentService } from 'src/app/_services/parent.service';
import { StateService } from 'src/app/_services/state.service';
import { UserService } from 'src/app/_services/user.service';
import { UtilityService } from 'src/app/_services/utility.service';

@Component({
  selector: 'app-add-parent',
  templateUrl: './add-parent.component.html',
  styleUrls: ['./add-parent.component.css']
})
export class AddParentComponent implements OnInit {
  stateData: Array<Select2OptionData>;
  countryData: Array<Select2OptionData>;
  parentForm = {} as FormGroup;
  formSubmitted = false;
  countryId: string;
  isSingleParent: boolean = false;
  isParentExist = false;
  parent1Relationship:string='';
  parent2Relationship:string='';

  constructor(
    private countryService: CountryService,
    private stateService: StateService,
    private utilityService: UtilityService,
    private formService: FormService,
    private formBuilder: FormBuilder,
    private parentService: ParentService,
    private userService: UserService) { }

  ngOnInit(): void {
    this.utilityService.showLoading();
    this.createParentForm();
    this.getCountries();
  }

  ngAfterViewInit() {
    this.utilityService.hideLoading();
  }

  createParentForm() {
    this.parentForm = this.formBuilder.group({
      Parent1FirstName: ['', Validators.required],
      Parent1LastName: ['', Validators.required],
      Parent1Relationship: ['', Validators.required],
      Parent1EmailId: [''],
      Parent1MobileNo: [''],
      SingleParent: [0],
      Parent2FirstName: [''],
      Parent2LastName: [''],
      Parent2Relationship: ['',],
      Parent2EmailId: [''],
      Parent2MobileNo: [''],
      PrimaryEmailId: ['', Validators.required],
      PrimaryMobileNo: [''],
      AddressLine1: [''],
      AddressLine2: [''],
      CityName: [''],
      StateId: [''],
      CountryId: [''],
      Pincode: ['']
    });
  }

  getParent1Relationship(event) {
    this.parent1Relationship = this.parentForm.get("Parent1Relationship").value;
    if(!!this.parent2Relationship){
      if(this.parent1Relationship == this.parent2Relationship) {
        this.parent1Relationship ="";
        alert("You already choose the same record");
        event.target.checked = false;
        return;
      }
    }
  }

  getParent2Relationship(event) {
    this.parent2Relationship = this.parentForm.get("Parent2Relationship").value;
    if(!!this.parent1Relationship){
      if(this.parent1Relationship == this.parent2Relationship) {
        this.parent2Relationship = "";
        alert("You already choose the same record");
        event.target.checked = false;
        return;
      }
    }    
  }

  getCountries() {
    this.countryService.getCountries()
      .then((res: ApiResponse) => {
        if (res.statusCode == 200) {
          if (!!res.data) {
            this.countryData = res.data.map(x => {
              return {
                id: x.countryId,
                text: x.countryName
              }
            });

            let country = this.countryData.find(x => x.text.toLowerCase() == "india");

            if (!!country) {
              this.parentForm.controls["CountryId"].setValue(country.id);
            }

          }
        }
      });
  }

  getStates(countryId) {
    this.stateService.getStateByCountryId(Number(countryId))
      .then((res: ApiResponse) => {
        if (res.statusCode == 200) {
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

  countryValueChanged(countryId: string) {
    if (!!countryId) {
      if (countryId == this.countryId) return;
      this.countryId = countryId;

      //get state data
      this.getStates(countryId);
    }
  }

  OnSingleParentChange(event) {
    this.parent2Relationship = "";
    const isChcked = event.target.checked;
    if (isChcked) {
      this.parentForm.get("SingleParent").setValue(1);
      this.isSingleParent = true;
    }
    else {
      this.parentForm.get("SingleParent").setValue(0);
      this.isSingleParent = false;
    }
  }

  onPrimaryEmailBlur(event) {
    this.isParentExist = false;
    this.userService.getUserByEmail(event.target.value)
      .then((res: ApiResponse) => {
        if (res.statusCode == HttpStatusCode.Ok) {
          // check if usertype is parent OR student

          if (!!res.data) {
            const user = res.data as User;

            if (!!user) {
              this.isParentExist = true;
              const userInstitute = user.userInstitutes;

              if (userInstitute.filter(x => x["UserTypeId"] == 2 || x["UserTypeId"] == 3).length > 0) {
                this.utilityService.showErrorToast("Record already exist.");
                return;
              }
              else{
                // bind the user contact info

              }
            }
            else{
              // it will create new record in db
            }
          }
        }
      });
  }

  onSubmit() {
    this.formSubmitted = true;
    this.formService.markFormGroupTouched(this.parentForm);
    if (this.parentForm.invalid) return;

    let data = JSON.stringify(this.parentForm.value);
    console.log('-----Add parent JSON Format-----');
    console.log(data);
    // APIs
    this.parentService.saveParent(this.parentForm.value)
      .then((res: ApiResponse) => {
        if (res.statusCode == 200) {
          // navigate to verification link if signUp goes well
          this.resetTeamForm();
        }
      });
  }

  resetTeamForm() {
    this.parentForm.reset();
  }
}
