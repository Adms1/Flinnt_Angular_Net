import { Component, OnInit } from '@angular/core';
import { Constants } from 'src/app/_helpers/constants';
import { UserProfile } from 'src/app/_models/user-profile';

@Component({
  selector: 'app-student-home',
  templateUrl: './student-home.component.html',
  styleUrls: ['./student-home.component.css']
})
export class StudentHomeComponent implements OnInit {
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
