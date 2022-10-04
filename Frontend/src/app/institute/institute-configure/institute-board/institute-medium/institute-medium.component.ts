import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Medium } from 'src/app/_models/medium';
import { ApiResponse } from 'src/app/_models/response';
import { InstituteConfigureService } from 'src/app/_services/institute-configure.service';

@Component({
  selector: 'app-institute-medium',
  templateUrl: './institute-medium.component.html',
  styleUrls: ['./institute-medium.component.css']
})
export class InstituteMediumComponent implements OnInit {
  medium: Medium[] = [];
  @Input() activatedBtn = false;
  @Output() actionTypeChange = new EventEmitter();
  @Output() showNextStepChange = new EventEmitter();
  @Output() showPreviousStepChange = new EventEmitter();
  constructor(private instituteConfigService: InstituteConfigureService) { }

  ngOnInit(): void {
    this.getMedium();
  }

  getMedium() {
    this.instituteConfigService.getMedium()
      .then((res: ApiResponse) => {
        if (res.statusCode == 200) {
          this.medium = res.data;
        }
      });
  }

  onSelectActionType(event?: Event){
    this.activatedBtn = true;
    this.actionTypeChange.emit(event);
  }

  onShowNextStep(event?: Event){
    this.showNextStepChange.emit(event);
  }

  onShowPreviousStep(event?: Event){
    this.showPreviousStepChange.emit(event);
  }
}
