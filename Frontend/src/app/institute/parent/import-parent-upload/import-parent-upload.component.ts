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
  parents: any = [];
  constructor(private modalService: NgbModal,
    private route: ActivatedRoute) {
    const _parentData = sessionStorage.getItem("parent-import");
    if (_parentData) {
      this.parents = JSON.parse(_parentData);
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
}
