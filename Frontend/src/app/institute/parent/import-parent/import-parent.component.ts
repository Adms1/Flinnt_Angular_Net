import { HttpEvent, HttpEventType } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
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
  progress: number = 0;
  

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
    this.parentDataImporting = true;

    this.parentService.importParent(formData).subscribe((event: HttpEvent<any>) => {
      switch (event.type) {
        case HttpEventType.UploadProgress:
          this.progress = Math.round(event.loaded / event.total * 100);
          console.log(`Uploaded! ${this.progress}%`);
          break;
        case HttpEventType.Response:
          const body = event.body as ApiResponse;
          sessionStorage.setItem("parent-import", JSON.stringify(body.data));
          if (body.statusCode == 200) {
            setTimeout(() => {
              this.progress = 0;
              this.route.navigate(["/institute/parent/import-parent-upload"]);
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
