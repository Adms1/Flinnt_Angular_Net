import { Component, Input, OnInit } from '@angular/core';
import { ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-import-student-upload',
  templateUrl: './import-student-upload.component.html',
  styleUrls: ['./import-student-upload.component.css']
})
export class ImportStudentUploadComponent implements OnInit {
  closeResult: string;
  @Input() studentData:any = [];
  isValidData: boolean = true;
  constructor(private modalService: NgbModal) { }

  ngOnInit(): void {
  }

  open(content) {
    this.modalService.open(content, {ariaLabelledBy: 'modal-basic-title'}).result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return  `with: ${reason}`;
    }
  }

  checkIsAccountCreated(studentItem){
    if(!!studentItem.importSummary 
      && studentItem.importSummary.length > 0){
        let _item = studentItem.importSummary.filter(x=>x.FieldName == "Primary email address");

        if(_item.length > 0){
          _item.forEach(element => {
            if(element.message == "A parent account already exists with the provided Primary email address"){
              return true;
            }
          });
        }
    }
  }
}
