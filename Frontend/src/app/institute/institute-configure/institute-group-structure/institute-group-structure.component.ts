import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { GroupStructure } from 'src/app/_models/group-structure';
import { ApiResponse } from 'src/app/_models/response';
import { InstituteConfigureService } from 'src/app/_services/institute-configure.service';

@Component({
  selector: 'app-institute-group-structure',
  templateUrl: './institute-group-structure.component.html',
  styleUrls: ['./institute-group-structure.component.css']
})
export class InstituteGroupStructureComponent implements OnInit {
  @Input() activatedBtn = false;
  @Input() groupStructureId : number | null;
  groupStructure: GroupStructure[] = [];
  @Output() actionTypeChange = new EventEmitter();
  @Output() showNextStepChange = new EventEmitter();
  @Output() showPreviousStepChange = new EventEmitter();

  constructor(private instituteConfigService: InstituteConfigureService) { }

  ngOnInit(): void {
    this.getInstituteGroupStructure();
  }

  getInstituteGroupStructure() {
    this.instituteConfigService.getGroupStructure()
      .then((res: ApiResponse) => {
        if (res.statusCode == 200) {
          this.groupStructure = res.data;

          if(this.groupStructureId > 0){
            this.activatedBtn = true;
          }
        }
      });
  }

  onSelectActionType(event?: Event, groupStructure?:GroupStructure){
    this.activatedBtn = true;
    this.instituteConfigService.groupStructureId = groupStructure.groupStructureId;
    this.actionTypeChange.emit(event);
  }

  onShowNextStep(event?: Event){
    this.showNextStepChange.emit(event);
  }

  onShowPreviousStep(event?: Event){
    this.showPreviousStepChange.emit(event);
  }
}
