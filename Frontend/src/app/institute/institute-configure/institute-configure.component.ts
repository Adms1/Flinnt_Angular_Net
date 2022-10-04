import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Constants } from 'src/app/_helpers/constants';
import { Institute } from 'src/app/_models/institute';
import { UtilityService } from 'src/app/_services/utility.service';

@Component({
  selector: 'app-institute-configure',
  templateUrl: './institute-configure.component.html',
  styleUrls: ['./institute-configure.component.css']
})
export class InstituteConfigureComponent implements OnInit {
  roleId = 2;
  activeStep = 1;
  step1Activated = true;
  step2Activated = false;
  step3Activated = false;
  step4Activated = false;
  step5Activated = false;
  institue = {} as Institute;
  @ViewChild('stepper1', { static: false }) sRef: ElementRef;

  constructor(
    private utilityService: UtilityService) { }

  ngOnInit(): void {
    this.getInstitute();
  }

  getInstitute() {
    const instituteObj = localStorage.getItem(Constants.INSTITUTE_PAGE.INSTITUTE_OBJ);

    if (!!instituteObj) {
      this.institue = JSON.parse(instituteObj) as Institute;
    }
  }

  showPreviousStep(event?: Event) {
    const parentActiveNode = event.currentTarget["parentElement"].closest('.active');
    const nextNode = parentActiveNode.previousElementSibling;

    parentActiveNode.classList.remove("active");
    parentActiveNode.classList.add("d-none");
    nextNode.classList.add("active");
    nextNode.classList.add("visible");
    nextNode.classList.remove("d-none");

    const querySelector = this.sRef.nativeElement.querySelector(".step.active");
    querySelector.classList.remove("active");
    querySelector.previousElementSibling.previousElementSibling.classList.add("active");
    this.activeStep--;
  }

  showNextStep(event?: Event) {
    const parentActiveNode = event.currentTarget["parentElement"].closest('.active');
    const nextNode = parentActiveNode.nextElementSibling;

    parentActiveNode.classList.remove("active");
    parentActiveNode.classList.add("d-none");
    nextNode.classList.add("active");
    nextNode.classList.add("visible");
    nextNode.classList.remove("d-none");

    const querySelector = this.sRef.nativeElement.querySelector(".step.active");
    querySelector.classList.remove("active");
    querySelector.nextElementSibling.nextElementSibling.classList.add("active");
    this.activeStep++;
    this.stepperActivated(this.activeStep);
  }

  selectActionType(event?: Event) {
    const allDivs = event.currentTarget["parentElement"].children;

    for (let index = 0; index < allDivs.length; index++) {
      const element = allDivs[index]["childNodes"][0];
      element["classList"].remove("selection-type_active");
    }

    event.currentTarget["childNodes"][0].classList.add("selection-type_active");
  }

  stepperActivated(activeStep) {
    switch (activeStep) {
      case 1:
        this.step1Activated = true;
        break;
      case 2:
        this.step2Activated = true;
        break
      case 3:
        this.step3Activated = true;
        break
      case 4:
        this.step4Activated = true;
        break
      case 5:
        this.step5Activated = true;
        break
    }
  }
}
