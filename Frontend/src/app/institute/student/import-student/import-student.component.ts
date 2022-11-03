import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-import-student',
  templateUrl: './import-student.component.html',
  styleUrls: ['./import-student.component.css']
})
export class ImportStudentComponent implements OnInit {
  studentDataImported: boolean = false;
  studentDataImporting: boolean = false;
  constructor() { }

  ngOnInit(): void {
  }

  onUploadBtn() {
    this.studentDataImporting = true;

    setTimeout(() => {
      this.studentDataImporting = false;
      this.studentDataImported = true;
    }, 3000);
  }
}