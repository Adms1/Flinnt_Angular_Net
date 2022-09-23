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
  institue = {} as Institute;
  isStudyMediumSelected = false;
  activatedBtn = false;
  @ViewChild('stepper1', {static: false}) sRef: ElementRef;
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
    this.activatedBtn = true;
    event.currentTarget["parentElement"].classList.remove("active");
    event.currentTarget["parentElement"].classList.add("d-none");
    event.currentTarget["parentElement"].previousElementSibling.classList.add("active");
    event.currentTarget["parentElement"].previousElementSibling.classList.add("visible");
    event.currentTarget["parentElement"].previousElementSibling.classList.remove("d-none");

    let querySelector = this.sRef.nativeElement.querySelector(".step.active");
    querySelector.classList.remove("active");
    querySelector.previousElementSibling.previousElementSibling.classList.add("active");
  }
 
  showNextStep(event?: Event) {
    this.activatedBtn = false;
    
    event.currentTarget["parentElement"].classList.remove("active");
    event.currentTarget["parentElement"].classList.add("d-none");
    event.currentTarget["parentElement"].nextElementSibling.classList.add("active");
    event.currentTarget["parentElement"].nextElementSibling.classList.add("visible");
    event.currentTarget["parentElement"].nextElementSibling.classList.remove("d-none");

    let querySelector = this.sRef.nativeElement.querySelector(".step.active");
    querySelector.classList.remove("active");
    querySelector.nextElementSibling.nextElementSibling.classList.add("active");
  }

  selectActionType(event?: Event){
    const allDivs = event.currentTarget["parentElement"].children;

    for (let index = 0; index < allDivs.length; index++) {
      const element = allDivs[index]["childNodes"][0];
      element["classList"].remove("selection-type_active");
    }

    event.currentTarget["childNodes"][0].classList.add("selection-type_active");
    this.activatedBtn = true;
  }

  selectBoard(event?: Event){
    this.isStudyMediumSelected = true;
    this.selectActionType(event);
  }
}
