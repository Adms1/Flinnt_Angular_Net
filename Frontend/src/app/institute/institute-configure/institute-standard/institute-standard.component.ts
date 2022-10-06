import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Group } from 'src/app/_models/group';
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
  standards: Standard[] = [];
  selectedStandardIds = [];
  @Output() actionTypeChange = new EventEmitter();
  @Output() showNextStepChange = new EventEmitter();
  @Output() showPreviousStepChange = new EventEmitter();
  constructor(private instituteConfigService: InstituteConfigureService) { }

  ngOnInit(): void {
    this.getInstituteStandard();
  }

  getInstituteStandard() {
    this.instituteConfigService.getStandard()
      .then((res: ApiResponse) => {
        if (res.statusCode == 200) {
          this.standards = res.data;
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
  }

  onShowPreviousStep(event?: Event) {
    this.showPreviousStepChange.emit(event);
  }

  saveInstituteGroup(event?: Event) {
    this.selectedStandardIds.forEach(element => {
      let saveObj: Group = {
        instituteId: 14,
        boardId: this.instituteConfigService.boardId,
        mediumId: this.instituteConfigService.mediumId,
        standardId: element
      };
      this.instituteConfigService.saveInstituteGroup(JSON.stringify(saveObj))
        .then((res: ApiResponse) => {
          this.showNextStepChange.emit(event);
        });
    });
  }
}