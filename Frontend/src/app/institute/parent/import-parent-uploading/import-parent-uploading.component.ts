import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-import-parent-uploading',
  templateUrl: './import-parent-uploading.component.html',
  styleUrls: ['./import-parent-uploading.component.css']
})
export class ImportParentUploadingComponent implements OnInit {
  @Input() progess: 0;
  constructor() { }

  ngOnInit(): void {
  }
}
