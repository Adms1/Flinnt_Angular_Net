import { Component, OnInit } from '@angular/core';
import { Constants } from 'src/app/_helpers/constants';
import { UserProfile } from 'src/app/_models/user-profile';

@Component({
  selector: 'app-institute-dashboard',
  templateUrl: './institute-dashboard.component.html',
  styleUrls: ['./institute-dashboard.component.css']
})
export class InstituteDashboardComponent implements OnInit {
  userProfile = {} as UserProfile;
  roleId = 2;
  constructor() { }

  ngOnInit(): void {
    this.getUser();
  }

  getUser() {
    const userObj = localStorage.getItem(Constants.LOGIN_PAGE.USER_OBJ);

    if (!!userObj) {
      this.userProfile = JSON.parse(userObj) as UserProfile;
    }
  }
}
