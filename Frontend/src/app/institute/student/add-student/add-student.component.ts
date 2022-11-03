import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { SearchParentComponent } from '../search-parent/search-parent.component';

@Component({
  selector: 'app-add-student',
  templateUrl: './add-student.component.html',
  styleUrls: ['./add-student.component.css']
})
export class AddStudentComponent implements OnInit {

  constructor(private modalService: NgbModal) { }

  ngOnInit(): void {
  }

  onSearchAccount() {
    const modalRef = this.modalService.open(SearchParentComponent,{ windowClass: 'my-class'});
    modalRef.componentInstance.selectedParent.subscribe((receivedEntry) => {
      console.log(receivedEntry);
    });
  }
}
