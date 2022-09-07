import { Component, OnInit } from '@angular/core';

import $ from 'jquery';
import { Constants } from 'src/app/_helpers/constants';
import { Institute } from 'src/app/_models/institute';
import { UtilityService } from 'src/app/_services/utility.service';

@Component({
  selector: 'app-varify-account',
  templateUrl: './varify-account.component.html',
  styleUrls: ['./varify-account.component.css']
})
export class VarifyAccountComponent implements OnInit {
  roleId = 2;
  institue = {} as Institute;
  constructor(
    private utilityService : UtilityService
  ) { }

  ngOnInit(): void {
    this.getInstitute();
  }

  getInstitute(){
    const instituteObj=localStorage.getItem(Constants.INSTITUTE_PAGE.INSTITUTE_OBJ);

    if(!!instituteObj){
      this.institue = JSON.parse(instituteObj) as Institute;
    }
  }

  onVerify(){
    this.utilityService.showSuccessToast("Account is verified successfully.")
  }
}
