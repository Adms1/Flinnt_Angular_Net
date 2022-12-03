import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-import-parent-upload',
  templateUrl: './import-parent-upload.component.html',
  styleUrls: ['./import-parent-upload.component.css']
})
export class ImportParentUploadComponent implements OnInit {
  closeResult: string;
  isValidData: boolean = true;
  parents: any = [];
  constructor(private modalService: NgbModal,
    private route: ActivatedRoute) {
    const _parentData = sessionStorage.getItem("parent-import");
    if (_parentData) {
      this.parents = JSON.parse(_parentData);

      if(this.parents.filter(x=>x.importSummary.length > 0).length > 0){
        this.isValidData = false;
      }
    }
  }

  ngOnInit(): void {

  }

  open(content) {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' }).result.then((result) => {
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
      return `with: ${reason}`;
    }
  }

  showError(parentItem) {
    let msgString = "";
    if (!!parentItem.importSummary
      && parentItem.importSummary.length > 0) {
      parentItem.importSummary.forEach(element => {
        msgString += element.message + "\n\n";
      });
    }

    alert(msgString);
  }

  checkIsAccountCreated(parentItem){
    if(!!parentItem.importSummary 
      && parentItem.importSummary.length > 0){
        let _item = parentItem.importSummary.filter(x=>x.FieldName == "Primary email address");

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
