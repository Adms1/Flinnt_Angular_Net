import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-import-parent',
  templateUrl: './import-parent.component.html',
  styleUrls: ['./import-parent.component.css']
})
export class ImportParentComponent implements OnInit {
  parentDataImporting: boolean = false;

  constructor(private route : Router) { }

  ngOnInit(): void {
  }

  onUploadBtn(){
    this.parentDataImporting = true;

    setTimeout(() => {
      this.route.navigate(["/institute/parent/import-parent-upload"]);
    }, 3000);
  }
}
