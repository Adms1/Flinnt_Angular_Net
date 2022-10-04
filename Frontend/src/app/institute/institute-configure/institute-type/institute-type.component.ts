import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { InstituteType } from 'src/app/_models/institute-type';
import { ApiResponse } from 'src/app/_models/response';
import { InstituteConfigureService } from 'src/app/_services/institute-configure.service';

@Component({
  selector: 'app-institute-type',
  templateUrl: './institute-type.component.html',
  styleUrls: ['./institute-type.component.css']
})
export class InstituteTypeComponent implements OnInit {
  @Input() activatedBtn = false;
  instituteTypes : InstituteType[] = [];
  @Output() actionTypeChange = new EventEmitter();
  @Output() showNextStepChange = new EventEmitter();
  
  constructor(private instituteConfigService : InstituteConfigureService) { }

  ngOnInit(): void {
    this.getInstituteType();
  }

  getInstituteType() {
    this.instituteConfigService.getInstituteType()
      .then((res: ApiResponse) => {
        if(res.statusCode == 200){
          this.instituteTypes = res.data;
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
}
