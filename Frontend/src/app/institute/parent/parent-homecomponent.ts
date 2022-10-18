import { Component, OnInit } from '@angular/core';
import { Constants } from 'src/app/_helpers/constants';
import { UserProfile } from 'src/app/_models/user-profile';

@Component({
  selector: 'app-parent',
  templateUrl: './parent-home.component.html',
  styleUrls: ['./parent-home.component.css']
})
export class ParentHomeComponent implements OnInit {
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
