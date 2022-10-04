import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
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

  onStandardChange(event?: Event) {
    this.activatedBtn = true;
  }

  onShowNextStep(event?: Event) {
    this.showNextStepChange.emit(event);
  }

  onShowPreviousStep(event?: Event) {
    this.showPreviousStepChange.emit(event);
  }
}
