import { Component, EventEmitter, Input, NgZone, OnInit, Output } from '@angular/core';
import { Group } from 'src/app/_models/group';
import { InstituteConfigureSession } from 'src/app/_models/institute-configure-session';
import { ApiResponse } from 'src/app/_models/response';
import { Standard } from 'src/app/_models/standard';
import { InstituteConfigureService } from 'src/app/_services/institute-configure.service';

@Component({
  selector: 'app-institute-standard',
  templateUrl: './institute-standard.component.html',
  styleUrls: ['./institute-standard.component.css']
})
export class InstituteStandardComponent implements OnInit {
  @Input() activatedBtn = false;
  @Input() instituteId = 0;
  standards: Standard[] = [];
  instituteGroups: Group[] = [];
  selectedStandardIds = [];
  _event: Event;
  @Output() actionTypeChange = new EventEmitter();
  @Output() showNextStepChange = new EventEmitter();
  @Output() showPreviousStepChange = new EventEmitter();
  constructor(private instituteConfigService: InstituteConfigureService,
    private ngZone: NgZone) { }

  ngOnInit(): void {
    this.getInstituteStandard();
  }

  getInstituteStandard() {
    this.instituteConfigService.getStandard()
      .then((res: ApiResponse) => {
        if (res.statusCode == 200) {
          this.standards = res.data;

          this.getInstituteGroup();
        }
      });
  }

  onStandardChange(event?: Event, standard?: Standard) {
    if (!!standard) {
      if (event.target["checked"]) {
        const index = this.selectedStandardIds.findIndex(x => x == standard.standardId);
        if (index == -1) {
          this.selectedStandardIds.push(standard.standardId);
        }
      }
      else {
        const index = this.selectedStandardIds.findIndex(x => x == standard.standardId);
        if (index > -1) {
          this.selectedStandardIds = this.selectedStandardIds.filter(x => x != standard.standardId);
        }
      }
    }
    this.activatedBtn = true;
  }

  onShowNextStep(event?: Event) {
    this.saveInstituteGroup(event);
    this.saveInstituteConfigureSession();
  }

  onShowPreviousStep(event?: Event) {
    this.showPreviousStepChange.emit(event);
  }

  getInputcheck(standardId) {

    const selectedStandards = this.instituteGroups
      .map((element) => {
        return element.standardViewModel
      });

    if (selectedStandards.filter(x => x.standardId == standardId).length > 0) {
      if (this.selectedStandardIds.filter(x => x == standardId).length == 0)
        this.selectedStandardIds.push(standardId);
      return true;
    }
    return false;
  }

  getInstituteGroup(){
    this.instituteConfigService.getGroup(this.instituteId)
    .then((res: ApiResponse) => {
      if (res.statusCode == 200) {
        if(!!res.data){
          this.activatedBtn = true;
          this.instituteGroups = res.data;
        }
      }
    });
  }

  saveInstituteGroup(event?: Event) {
    this._event = event;
    const that = this;
    let saveObj: Group = {} as Group;
    saveObj.instituteId = this.instituteId;
    saveObj.groupStructureId = this.instituteConfigService.groupStructureId;
    saveObj.mediumId = this.instituteConfigService.mediumId;
    saveObj.boardId = this.instituteConfigService.boardId;
    saveObj.standards = [];
    this.selectedStandardIds.forEach(element => {
      saveObj.standards.push({
        standardId: element
      });
    });

    this.instituteConfigService.saveInstituteGroup(JSON.stringify(saveObj))
      .then((res: ApiResponse) => {
        if (res.statusCode == 200) {
          this.showNextStepChange.emit(that._event);
        }
      });
  }

  saveInstituteConfigureSession(){
    console.log('-----Institute Session JSON Format-----');
    // APIs
    const sessionObj: InstituteConfigureSession = {
      instituteId: this.instituteId,
      boardId: this.instituteConfigService.boardId,
      mediumId: this.instituteConfigService.mediumId,
      currentStep: 5,
      groupStructureId: this.instituteConfigService.groupStructureId,
      instituteTypeId: this.instituteConfigService.instituteTypeId
    };

    this.instituteConfigService.saveInstituteConfigureSession(JSON.stringify(sessionObj))
      .then((res: ApiResponse) => {
        if (res.statusCode == 200) {
        }
      });
  }
}