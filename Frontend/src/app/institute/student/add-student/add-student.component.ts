import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { FormService } from 'src/app/core/form.service';
import { ApiResponse } from 'src/app/_models/response';
import { StudentService } from 'src/app/_services/student.service';
import { SearchParentComponent } from '../search-parent/search-parent.component';

@Component({
  selector: 'app-add-student',
  templateUrl: './add-student.component.html',
  styleUrls: ['./add-student.component.css']
})
export class AddStudentComponent implements OnInit {
  formSubmitted = false;
  studentForm = {} as FormGroup;
  @Input() boardId?;
  @Input() mediumId?;
  @Input() standardId?;
  @Input() divisionId?;
  @Input() instituteId?;
  @Input() groupId?;
  
  constructor(
    private modalService: NgbModal,
    private studentService: StudentService,
    private formService: FormService,
    private formBuilder: FormBuilder,
    ) { }

  ngOnInit(): void {
   this.createStudentForm();
  }

  disableForm(){
    if(this.boardId > 0 && this.mediumId > 0 && this.standardId > 0 && this.divisionId > 0 && this.groupId> 0)
      return true;
    else
      return false;
  }
  createStudentForm() {
    this.studentForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      emailId: ['', Validators.required],
      dob: [null],
      mobileNo: [''],
      genderId: [0],
      rollNo: [''],
      grno: [''],
      parentUserId: [null],
      instituteId:[null, Validators.required],
      instituteGroupId:[null, Validators.required],
      instituteDivisionId:[null, Validators.required],
    });
  }

  onSearchAccount() {
    const modalRef = this.modalService.open(SearchParentComponent, { windowClass: 'my-class' });
    modalRef.componentInstance.selectedParent.subscribe((receivedEntry) => {
      console.log(receivedEntry);
    });
  }

  onSubmit() {
    this.formSubmitted = true;

    this.studentForm.patchValue({
      instituteId:this.instituteId,
      instituteGroupId: this.groupId,
      instituteDivisionId: this.divisionId
    });

    this.formService.markFormGroupTouched(this.studentForm);
    if (this.studentForm.invalid) return;

    if(!this.disableForm()){
      alert("Please select an appropriate group");
      return;
    }

    let data = JSON.stringify(this.studentForm.value);
    console.log('-----Add parent JSON Format-----');
    console.log(data);
    // APIs
    this.studentService.saveStudent(this.studentForm.value)
      .then((res: ApiResponse) => {
        if (res.statusCode == 200) {
          // navigate to verification link if signUp goes well
          this.resetTeamForm();
        }
      });
  }

  resetTeamForm() {
    this.studentForm.reset();
  }
}
