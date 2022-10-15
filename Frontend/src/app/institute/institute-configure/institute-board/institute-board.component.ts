import { Component, ElementRef, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Board } from 'src/app/_models/board';
import { ApiResponse } from 'src/app/_models/response';
import { InstituteConfigureService } from 'src/app/_services/institute-configure.service';

@Component({
  selector: 'app-institute-board',
  templateUrl: './institute-board.component.html',
  styleUrls: ['./institute-board.component.css']
})
export class InstituteBoardComponent implements OnInit {
  @Input() isStudyMediumSelected = false;
  @Input() boardId :  number | null;
  @Input() mediumId :  number | null;
  boards: Board[] = [];
  @Output() actionTypeChange = new EventEmitter();
  @Output() showNextStepChange = new EventEmitter();
  @Output() showPreviousStepChange = new EventEmitter();
  
  activatedBtn = false;
  constructor(private instituteConfigService: InstituteConfigureService) { }

  ngOnInit(): void {
    this.getBoard();
  }

  getBoard() {
    this.instituteConfigService.getBoard()
      .then((res: ApiResponse) => {
        if (res.statusCode == 200) {
          this.boards = res.data;

          if (this.mediumId > 0 || (!!this.boardId && this.boardId > 0)) {
            this.isStudyMediumSelected = true;
          }
        }
      });
  }

  onSelectActionType(event?: Event, board?: Board){
    this.isStudyMediumSelected = true;
    if(!!board)
      this.instituteConfigService.boardId = board.boardId;
    this.actionTypeChange.emit(event);
  }

  onShowNextStep(event?: Event){
    this.showNextStepChange.emit(event);
  }

  onShowPreviousStep(event?: Event){
    this.showPreviousStepChange.emit(event);
  }
}
