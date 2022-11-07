import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { Constants } from 'src/app/_helpers/constants';
import { Institute } from 'src/app/_models/institute';
import { InstituteConfigureSession } from 'src/app/_models/institute-configure-session';
import { ApiResponse } from 'src/app/_models/response';
import { UserProfile } from 'src/app/_models/user-profile';
import { InstituteConfigureService } from 'src/app/_services/institute-configure.service';
import { InstituteService } from 'src/app/_services/institute.service';
import { UtilityService } from 'src/app/_services/utility.service';

@Component({
  selector: 'app-institute-configure',
  templateUrl: './institute-configure.component.html',
  styleUrls: ['./institute-configure.component.css']
})
export class InstituteConfigureComponent implements OnInit {
  userProfile = {} as UserProfile;
  roleId = 2;
  currentStep = 1;
  activeStep = 1;
  instituteId = 0;

  instituteTypeId=null;
  groupStructureId=null;
  boardId=null;
  mediumId=null;

  step1Activated = false;
  step2Activated = false;
  step3Activated = false;
  step4Activated = false;
  step5Activated = false;
  instituteConfigureSession = {} as InstituteConfigureSession;
  @ViewChild('stepper1', { static: false }) sRef: ElementRef;

  constructor(
    private router: Router,
    private utilityService: UtilityService,
    private instituteConfigService: InstituteConfigureService,
    private instituteService: InstituteService) { }

  ngOnInit(): void {
    this.getUser();
    
    this.getInstituteConfigureSession();
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

  getInstituteConfigureSession(){
    this.instituteConfigService.getInstituteConfigureSession(this.instituteId)
    .then((res: ApiResponse) => {
      if (res.statusCode == 200) {
        if(!!res.data){
          this.instituteConfigureSession = res.data;

          const typeId = this.instituteConfigureSession.instituteTypeId;
          const groupStructureId = this.instituteConfigureSession.groupStructureId;
          const boardId = this.instituteConfigureSession.boardId;
          const mediumId = this.instituteConfigureSession.mediumId;
          const currentStep = this.instituteConfigureSession.currentStep;

          this.instituteTypeId = this.instituteConfigService.instituteTypeId = !!typeId ? typeId : null;
          this.groupStructureId = this.instituteConfigService.groupStructureId = !!groupStructureId ? groupStructureId : null;
          this.boardId = this.instituteConfigService.boardId = !!boardId ? boardId : null;
          this.mediumId = this.instituteConfigService.mediumId = !!mediumId ? mediumId : null;
          this.currentStep = this.activeStep = !!currentStep ? currentStep : 1;
        }
      }
      this.stepperActivated(this.currentStep);
    });
  }

  showPreviousStep(event?: Event) {
    // const parentActiveNode = event.target["parentElement"].closest('.active');
    // const nextNode = parentActiveNode.previousElementSibling;

    // parentActiveNode.classList.remove("active");
    // parentActiveNode.classList.add("d-none");
    // nextNode.classList.add("active");
    // nextNode.classList.add("visible");
    // nextNode.classList.remove("d-none");

    // const querySelector = this.sRef.nativeElement.querySelector(".step.active");
    // querySelector.classList.remove("active");
    // querySelector.previousElementSibling.previousElementSibling.classList.add("active");
    this.activeStep--;
    this.stepperActivated(this.activeStep);
  }

  showNextStep(event?: Event) {
    // const parentActiveNode = event.target["parentElement"].closest('.active');
    // const nextNode = parentActiveNode.nextElementSibling;

    // parentActiveNode.classList.remove("active");
    // parentActiveNode.classList.add("d-none");
    // nextNode.classList.add("active");
    // nextNode.classList.add("visible");
    // nextNode.classList.remove("d-none");

    // const querySelector = this.sRef.nativeElement.querySelector(".step.active");
    // querySelector.classList.remove("active");
    // querySelector.nextElementSibling.nextElementSibling.classList.add("active");
    this.activeStep++;
    this.stepperActivated(this.activeStep);
  }

  selectActionType(event?: Event) {
    const _event = event;
    const allDivs = _event.currentTarget["parentElement"].children;

    for (let index = 0; index < allDivs.length; index++) {
      const element = allDivs[index]["childNodes"][0];
      element["classList"].remove("selection-type_active");
    }

    _event.currentTarget["childNodes"][0].classList.add("selection-type_active");
    this.saveInstituteConfigureSession();
  }

  getCurrentStep() {
    if (this.activeStep == 3) {
      if (!!this.instituteConfigService.mediumId && this.instituteConfigService.mediumId > 0) {
        return 4;
      }
      else
        return 3;
    }
    return this.activeStep + 1;
  }
  
  saveInstituteConfigureSession(){
    console.log('-----Institute Session JSON Format-----');
    // APIs
    const sessionObj: InstituteConfigureSession = {
      instituteId: this.instituteId,
      boardId: this.instituteConfigService.boardId,
      mediumId: this.instituteConfigService.mediumId,
      currentStep: this.getCurrentStep(),
      groupStructureId: this.instituteConfigService.groupStructureId,
      instituteTypeId: this.instituteConfigService.instituteTypeId
    };

    this.instituteConfigService.saveInstituteConfigureSession(JSON.stringify(sessionObj))
      .then((res: ApiResponse) => {
        if (res.statusCode == 200) {
        }
      });
  }

  onFinishConfigure(){
    console.log('-----Institute JSON Format-----');
    // APIs
    const sessionObj: Institute = {
      instituteId: this.instituteId,
      groupStructureId: this.instituteConfigService.groupStructureId,
      instituteTypeId: this.instituteConfigService.instituteTypeId
    };

    this.instituteService.updateInstitute(JSON.stringify(sessionObj))
      .then((res: ApiResponse) => {
        if (res.statusCode == 200) {
          this.router.navigate([""]);
        }
      });
  }

  stepperActivated(activeStep) {
    switch (activeStep) {
      case 1:
        this.step1Activated = true;
        this.step2Activated = false;
        this.step3Activated = false;
        this.step4Activated = false;
        this.step5Activated = false;
        break;
      case 2:
        this.step1Activated = false;
        this.step2Activated = true;
        this.step3Activated = false;
        this.step4Activated = false;
        this.step5Activated = false;
        break
      case 3:
        this.step1Activated = false;
        this.step2Activated = false;
        this.step3Activated = true;
        this.step4Activated = false;
        this.step5Activated = false;
        break
      case 4:
        this.step1Activated = false;
        this.step2Activated = false;
        this.step3Activated = false;
        this.step4Activated = true;
        this.step5Activated = false;
        break
      case 5:
        this.step1Activated = false;
        this.step2Activated = false;
        this.step3Activated = false;
        this.step4Activated = false;
        this.step5Activated = true;
        break
    }
  }
}
