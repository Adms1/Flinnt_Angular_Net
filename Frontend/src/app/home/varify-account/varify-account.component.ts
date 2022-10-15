import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { Constants } from 'src/app/_helpers/constants';
import { ApiResponse } from 'src/app/_models/response';
import { UserProfile } from 'src/app/_models/user-profile';
import { InstituteService } from 'src/app/_services/institute.service';
import { UtilityService } from 'src/app/_services/utility.service';

@Component({
  selector: 'app-varify-account',
  templateUrl: './varify-account.component.html',
  styleUrls: ['./varify-account.component.css']
})
export class VarifyAccountComponent implements OnInit {
  roleId = 2;
  userProfile = {} as UserProfile;
  otpForm = {} as FormGroup;
  constructor(
    private formBuilder: FormBuilder,
    private utilityService: UtilityService,
    private instituteService: InstituteService,
    private route: Router
  ) { }

  ngOnInit(): void {
    this.createLoginForm();
    this.getUser();
  }
  createLoginForm() {
    this.otpForm = this.formBuilder.group({
      otp1: ['', Validators.required],
      otp2: ['', Validators.required],
      otp3: ['', Validators.required],
      otp4: ['', Validators.required]
    });
  }

  getUser() {
    const userObj = localStorage.getItem(Constants.LOGIN_PAGE.USER_OBJ);

    if (!!userObj) {
      this.userProfile = JSON.parse(userObj) as UserProfile;
    }
  }

  onVerify() {
    if (this.otpForm.invalid) {
      this.utilityService.showErrorToast("Please enter otp details.");
      return;
    }
    const userObj = JSON.parse(localStorage.getItem(Constants.LOGIN_PAGE.USER_OBJ));
    const otp1 = this.otpForm.controls["otp1"].value;
    const otp2 = this.otpForm.controls["otp2"].value;
    const otp3 = this.otpForm.controls["otp3"].value;
    const otp4 = this.otpForm.controls["otp4"].value;
    const otp = otp1 + '' + otp2 + '' + otp3 + '' + otp4;
    this.instituteService.accountVerify(userObj.userId, otp).then((res: ApiResponse) => {
      if (res.data.value) {
        this.route.navigate(['institute/configure']);
        this.utilityService.showSuccessToast("Account is verified successfully.")
      }
    });
  }
}
