import { Component, OnInit } from '@angular/core';
import { Select2OptionData } from 'ng-select2';
import { ApiResponse } from 'src/app/_models/response';
import { CountryService } from 'src/app/_services/country.service';
import { StateService } from 'src/app/_services/state.service';

@Component({
  selector: 'app-add-parent',
  templateUrl: './add-parent.component.html',
  styleUrls: ['./add-parent.component.css']
})
export class AddParentComponent implements OnInit {
  stateData: Array<Select2OptionData>;
  countryData: Array<Select2OptionData>;
  constructor( private countryService: CountryService,
    private stateService: StateService) { }

  ngOnInit(): void {
    this.getCountries();
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
}
