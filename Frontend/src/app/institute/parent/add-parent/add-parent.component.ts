import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Select2OptionData } from 'ng-select2';
import { FormService } from 'src/app/core/form.service';
import { ApiResponse } from 'src/app/_models/response';
import { CountryService } from 'src/app/_services/country.service';
import { ParentService } from 'src/app/_services/parent.service';
import { StateService } from 'src/app/_services/state.service';
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
  isSingleParent:boolean = false;

  constructor(
    private countryService: CountryService,
    private stateService: StateService,
    private utilityService: UtilityService,
    private formService: FormService,
    private formBuilder: FormBuilder,
    private parentService: ParentService) { }

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
      SingleParent: [false],
      Parent2FirstName: ['', Validators.required],
      Parent2LastName: ['', Validators.required],
      Parent2Relationship: ['', Validators.required],
      Parent2EmailId: [''],
      Parent2MobileNo: [''],
      PrimaryEmailId: ['', Validators.required],
      PrimaryMobileNo: ['']
    });
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
              this.parentForm.controls["countryId"].setValue(country.id);
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

  OnSingleParentChange(event){
    const isChcked = event.target.checked;
    if(isChcked){
      this.isSingleParent = true;
    }
    else{
      this.isSingleParent = false;
    }
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
