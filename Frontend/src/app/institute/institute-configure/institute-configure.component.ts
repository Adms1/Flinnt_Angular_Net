import { Component, OnInit } from '@angular/core';
import { of } from 'rxjs';
import { 
  NgWizardConfig, 
  NgWizardService, 
  StepChangedArgs, 
  StepValidationArgs, 
  STEP_STATE, 
  THEME, 
  TOOLBAR_BUTTON_POSITION, 
  TOOLBAR_POSITION
} from 'ng-wizard';
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
  stepStates = {
    normal: STEP_STATE.normal,
    disabled: STEP_STATE.disabled,
    error: STEP_STATE.error,
    hidden: STEP_STATE.hidden
  };

  config: NgWizardConfig = {
    selected: 0,
    theme: THEME.circles,
    toolbarSettings: {
      toolbarPosition:TOOLBAR_POSITION.bottom,
      showNextButton:true,
      showPreviousButton:true,
      toolbarExtraButtons: [
        //{ text: 'Finish', class: 'btn btn-info', event: () => { alert("Finished!!!"); } }
      ],
    }
  };

  constructor(
    private ngWizardService: NgWizardService,
    private utilityService: UtilityService,) { }

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
    this.ngWizardService.previous();
  }
 
  showNextStep(event?: Event) {
    this.ngWizardService.next();
  }
 
  resetWizard(event?: Event) {
    this.ngWizardService.reset();
  }
 
  setTheme(theme: THEME) {
    this.ngWizardService.theme(theme);
  }
 
  stepChanged(args: StepChangedArgs) {
    console.log(args.step);
  }
 
  isValidTypeBoolean: boolean = true;
 
  isValidFunctionReturnsBoolean(args: StepValidationArgs) {
    return true;
  }
 
  isValidFunctionReturnsObservable(args: StepValidationArgs) {
    return of(true);
  }
}
