import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { Constants } from 'src/app/_helpers/constants';
import { ApiResponse } from 'src/app/_models/response';
import { UserProfile } from 'src/app/_models/user-profile';
import { ParentService } from 'src/app/_services/parent.service';

@Component({
  selector: 'app-import-parent-upload',
  templateUrl: './import-parent-upload.component.html',
  styleUrls: ['./import-parent-upload.component.css']
})
export class ImportParentUploadComponent implements OnInit {
  @Input() parentData: any = [];
  closeResult: string;
  isValidData: boolean = true;
  userProfile = {} as UserProfile;
  instituteId = 0;

  constructor(private modalService: NgbModal,
    private route: ActivatedRoute,
    private parentService: ParentService) {
    const _parentData = sessionStorage.getItem("parent-import");
    if (_parentData) {
      const _parents = JSON.parse(_parentData);

      if (_parents.filter(x => x.importSummary.length > 0).length > 0) {
        this.isValidData = false;
      }
    }
  }

  ngOnInit(): void {
    this.getUser();
  }

  getUser() {
    const userObj = localStorage.getItem(Constants.LOGIN_PAGE.USER_OBJ);

    if (!!userObj) {
      this.userProfile = JSON.parse(userObj) as UserProfile;
    }

    const instituteId = localStorage.getItem(Constants.LOGIN_PAGE.INSTITUTE_ID);

    this.instituteId = Number(instituteId);
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

  checkIsAccountCreated(parentItem) {
    if (!!parentItem.importSummary
      && parentItem.importSummary.length > 0) {
      let _item = parentItem.importSummary.filter(x => x.FieldName == "Primary email address");

      if (_item.length > 0) {
        _item.forEach(element => {
          if (element.message == "A parent account already exists with the provided Primary email address") {
            return true;
          }
        });
      }
    }
  }

  importData() {
    var that = this;
    this.parentData.forEach(element => {
      element.InstituteId = that.instituteId;
      element.LoggedUserId = that.userProfile.userId;
    });
    this.parentService.importFinalData(JSON.stringify(this.parentData))
      .then((res: ApiResponse) => {
        if (res.statusCode == 200) {
        }
      });
  }
}
