import { HttpEvent, HttpEventType } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ApiResponse } from 'src/app/_models/response';
import { StudentService } from 'src/app/_services/student.service';
import { UtilityService } from 'src/app/_services/utility.service';

@Component({
  selector: 'app-import-student',
  templateUrl: './import-student.component.html',
  styleUrls: ['./import-student.component.css']
})
export class ImportStudentComponent implements OnInit {
  studentDataImported: boolean = false;
  studentDataImporting: boolean = false;
  studentData:any=[];
  progress: number = 0;
  
  constructor(private utilityService: UtilityService,
    private studentService: StudentService) { 

  }

  ngOnInit(): void {
  }

  onFileChange(event) {
    let file = event.target.files[0];

    let fileType = '.' + file.name.split('.').pop();

    switch (fileType.toLocaleLowerCase()) {
      case ".xls":
      case ".xlsx":
        break;
      default:
        this.utilityService.showErrorToast('This file extension not supported!');
        return;
    }
  }

  onUploadBtn(files) {
    const file = files.files;
    if (file.length === 0) {
      return;
    }

    let fileToUpload = <File>file[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    this.studentDataImporting = true;

    this.studentService.importStudent(formData).subscribe((event: HttpEvent<any>) => {
      switch (event.type) {
        case HttpEventType.UploadProgress:
          this.progress = Math.round(event.loaded / event.total * 100);
          console.log(`Uploaded! ${this.progress}%`);
          break;
        case HttpEventType.Response:
          const body = event.body as ApiResponse;
          sessionStorage.setItem("student-import", JSON.stringify(body.data));
          if (body.statusCode == 200) {
            this.studentData = body.data;
            setTimeout(() => {
              this.studentDataImporting = false;
              this.studentDataImported = true;
              this.progress = 0;
            }, 1000);
          }
          else {
            this.utilityService.showErrorToast(body.message);
          }
      }
    },
      err => {
        this.progress = 0;
        this.utilityService.showErrorToast('Unable to upload');
      });
  }
}