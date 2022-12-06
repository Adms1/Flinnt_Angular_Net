import { Component, Input, OnInit } from '@angular/core';
import { ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Constants } from 'src/app/_helpers/constants';
import { ApiResponse } from 'src/app/_models/response';
import { UserProfile } from 'src/app/_models/user-profile';
import { StudentService } from 'src/app/_services/student.service';

@Component({
  selector: 'app-import-student-upload',
  templateUrl: './import-student-upload.component.html',
  styleUrls: ['./import-student-upload.component.css']
})
export class ImportStudentUploadComponent implements OnInit {
  closeResult: string;
  @Input() studentData: any = [];
  isValidData: boolean = true;
  userProfile = {} as UserProfile;
  instituteId = 0;
  
  constructor(private modalService: NgbModal,
    private studentService: StudentService) { }

  ngOnInit(): void {
    this.getUser();

    const _studentData = sessionStorage.getItem("student-import");
    if (_studentData) {
      const _students = JSON.parse(_studentData);

      if (_students.filter(x => x.importSummary.length > 0).length > 0) {
        this.isValidData = false;
      }
    }
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

  showError(studentItem) {
    let msgString = "";
    if (!!studentItem.importSummary
      && studentItem.importSummary.length > 0) {
      studentItem.importSummary.forEach(element => {
        msgString += element.message + "\n\n";
      });
    }

    alert(msgString);
  }

  checkIsAccountCreated(studentItem) {
    if (!!studentItem.importSummary
      && studentItem.importSummary.length > 0) {
      let _item = studentItem.importSummary.filter(x => x.FieldName == "Primary email address");

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
    this.studentData.forEach(element => {
      element.InstituteId = that.instituteId;
      element.LoggedUserId = that.userProfile.userId;
    });

    this.studentService.importFinalStudentData(JSON.stringify(this.studentData))
    .then((res: ApiResponse) => {
      if (res.statusCode == 200) {
      }
    });
  }
}
