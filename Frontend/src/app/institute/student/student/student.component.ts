import { Component, OnInit } from '@angular/core';
import { Constants } from 'src/app/_helpers/constants';
import { Board } from 'src/app/_models/board';
import { Division } from 'src/app/_models/division';
import { Group } from 'src/app/_models/group';
import { Medium } from 'src/app/_models/medium';
import { Standard } from 'src/app/_models/standard';
import { UserProfile } from 'src/app/_models/user-profile';
import { InstituteConfigureService } from 'src/app/_services/institute-configure.service';
import { UtilityService } from 'src/app/_services/utility.service';
import { Router } from '@angular/router';
import { ApiResponse } from 'src/app/_models/response';

@Component({
  selector: 'app-student',
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.css']
})
export class StudentComponent implements OnInit {
  userProfile = {} as UserProfile;
  boards: Board[] = [];
  standards: Standard[] = [];
  mediums: Medium[] = [];
  divisions: Division[] = [];
  instituteId = 0;
  boardId = 0;
  standardId = 0;
  mediumId = 0;
  divisionId =0;
  constructor(
    private router: Router,
    private instituteConfigService: InstituteConfigureService,
    private utilityService: UtilityService
  ) { }

  ngOnInit(): void {
    this.getUser();
    this.getBoard();
    this.getMedium();
    this.getInstituteStandard();
  }

  getUser() {
    const userObj = localStorage.getItem(Constants.LOGIN_PAGE.USER_OBJ);

    if (!!userObj) {
      this.userProfile = JSON.parse(userObj) as UserProfile;
    }

    const instituteId = localStorage.getItem(Constants.LOGIN_PAGE.INSTITUTE_ID);

    if(instituteId == undefined || instituteId == null)
    {
      this.utilityService.showErrorToast("something went wrong. Please login again!");
      localStorage.clear();
      this.router.navigate(['']);
      return;
    }
    this.instituteId = Number(instituteId);
  }

  getBoard() {
    this.instituteConfigService.getBoard()
      .then((res: ApiResponse) => {
        if (res.statusCode == 200) {
          this.boards = res.data;
        }
      });
  }

  getMedium() {
    this.instituteConfigService.getMedium()
      .then((res: ApiResponse) => {
        if (res.statusCode == 200) {
          this.mediums = res.data;
        }
      });
  }

  getSpecificInstituteGroup() {
    const boardId = this.boardId;
    const mediumId = this.mediumId;
    const standardId = this.standardId;

    if(boardId == 0 && mediumId == 0 && standardId == 0) return;

    this.instituteConfigService.getSpecificInstituteGroup(this.instituteId, boardId, mediumId, standardId)
      .then((res: ApiResponse) => {
        if (res.statusCode == 200) {
          const group = res.data[0] as Group;

          if (!!group) {
            const groupId = group.instituteGroupId;
            this.getDivisionByGroupId(groupId);
          }
        }
      });
  }

  getDivisionByGroupId(groupId) {

    this.instituteConfigService.getDivisionByInstituteGroupId(this.instituteId, groupId)
      .then((res: ApiResponse) => {
        this.divisions = res.data as Array<Division>;
      });
  }

  getInstituteStandard() {
    this.instituteConfigService.getStandard()
      .then((res: ApiResponse) => {
        if (res.statusCode == 200) {
          this.standards = res.data;
        }
      });
  }

  onBoardSelect(boardId) {
    this.boardId = boardId;
    this.getSpecificInstituteGroup();
  }

  onStandardSelect(standardId) {
    this.standardId = standardId;
    this.getSpecificInstituteGroup();
  }

  onMediumSelect(mediumId) {
    this.mediumId = mediumId;
    this.getSpecificInstituteGroup();
  }

  onDivisionSelect(divisionId) {
    this.divisionId = divisionId;
    this.getSpecificInstituteGroup();
  }
}
