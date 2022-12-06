import { HttpEvent, HttpEventType } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { ApiResponse } from 'src/app/_models/response';
import { ParentService } from 'src/app/_services/parent.service';
import { UtilityService } from 'src/app/_services/utility.service';

@Component({
  selector: 'app-import-parent',
  templateUrl: './import-parent.component.html',
  styleUrls: ['./import-parent.component.css']
})
export class ImportParentComponent implements OnInit {
  parentDataImporting: boolean = false;
  parentDataImported: boolean = false;
  progress: number = 0;
  parentData:any=[];
  resetProgressSubject$: Subject<number> = new Subject<number>();
  fileName: string = "";
  constructor(private route: Router,
    private utilityService: UtilityService,
    private parentService: ParentService) { }

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
    this.parentDataImporting = true;

    this.parentService.importParent(formData).subscribe((event: HttpEvent<any>) => {
      switch (event.type) {
        case HttpEventType.UploadProgress:
          that.progress = Math.round(event.loaded / event.total * 100);
          that.resetProgressSubject$.next(that.progress);
          console.log(`Uploaded! ${this.progress}%`);
          break;
        case HttpEventType.Response:
          const body = event.body as ApiResponse;
          sessionStorage.setItem("parent-import", JSON.stringify(body.data));
          that.progress = 100;
          that.resetProgressSubject$.next(that.progress);
          that.parentData = body.data;
          if (body.statusCode == 200) {
            setTimeout(() => {
              that.parentDataImporting = false;
              that.parentDataImported = true;
              that.progress = 0;
            }, 1000);
          }
          else {
            that.utilityService.showErrorToast(body.message);
          }
      }
    },
      err => {
        that.progress = 0;
        that.utilityService.showErrorToast('Unable to upload');
    });
  }
}
