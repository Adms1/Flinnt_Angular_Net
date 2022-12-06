import { HttpEvent, HttpEventType } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
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
  resetProgressSubject$: Subject<number> = new Subject<number>();
  fileName: string="";
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
        this.fileName = file.name;
        break;
      default:
        this.utilityService.showErrorToast('This file extension not supported!');
        return;
    }
  }

  onUploadBtn(files) {
    var that = this;
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
          that.progress = Math.round(event.loaded / event.total * 100);
          that.resetProgressSubject$.next(that.progress);
          
          console.log(`Uploaded! ${that.progress}%`);
          break;
        case HttpEventType.Response:
          const body = event.body as ApiResponse;
          sessionStorage.setItem("student-import", JSON.stringify(body.data));
          if (body.statusCode == 200) {
            that.progress = 100;
            that.resetProgressSubject$.next(that.progress);
            that.studentData = body.data;
            setTimeout(() => {
              that.studentDataImporting = false;
              that.studentDataImported = true;
              that.progress = 0;
            }, 1000);
          }
          else {
            that.utilityService.showErrorToast(body.message);
          }
      }
    },
      err => {
        this.progress = 0;
        this.utilityService.showErrorToast('Unable to upload');
      });
  }
}