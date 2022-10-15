import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DataTableDirective } from 'angular-datatables';
import { Subject } from 'rxjs';
import { FormService } from 'src/app/core/form.service';
import { Division } from 'src/app/_models/division';
import { Group } from 'src/app/_models/group';
import { ApiResponse } from 'src/app/_models/response';
import { Standard } from 'src/app/_models/standard';
import { InstituteConfigureService } from 'src/app/_services/institute-configure.service';

@Component({
  selector: 'app-institute-division',
  templateUrl: './institute-division.component.html',
  styleUrls: ['./institute-division.component.css']
})
export class InstituteDivisionComponent implements OnInit {
  @ViewChild(DataTableDirective, {static: false})
  dtElement: DataTableDirective;

  divisionForm = {} as FormGroup;
  formSubmitted = false;
  divisions : Division[] = [];
  instituteGroups: Group[] = [];
  standards: Standard[] = [];
  dtOptions: DataTables.Settings = {};
  // We use this trigger because fetching the list of divison can be quite long,
  // thus we ensure the data is fetched before rendering
  dtTrigger: Subject<any> = new Subject<any>();
  constructor(private instituteConfigService: InstituteConfigureService,
    private formService: FormService,
    private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10
    };

    this.createDivisionForm();
    this.getInstituteGroup();
    this.getDivision();
  }

  ngAfterViewInit(){
    this.dtTrigger.next();
  }

  createDivisionForm() {
    this.divisionForm = this.formBuilder.group({
      instituteGroupId: ['', Validators.required],
      divisionName: ['', Validators.required]
    });
  }

  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    this.dtTrigger.unsubscribe();
  }

  getInstituteGroup(){
    // TODO: dynamic instituteId
    this.instituteConfigService.getGroup(14)
    .then((res: ApiResponse) => {
      if (res.statusCode == 200) {

        if(!!res.data){
          this.instituteGroups = res.data;

          this.instituteGroups.map((element)=>{
            this.standards.push(element.standardViewModel);
          })
        }
      }
    });
  }

  getDivision() {
    // TODO: instituteId dynamic
    this.instituteConfigService.getDivision(14)
      .then((res: ApiResponse) => {
        if (res.statusCode == 200) {
          this.divisions = res.data;
          this.rerender();
        }
      });
  }

  onAddDivisonSubmit(){
    this.formSubmitted = true;
    
    this.formService.markFormGroupTouched(this.divisionForm);
    if (this.divisionForm.invalid) return;
    
    let data = JSON.stringify(this.divisionForm.value);
    console.log('-----SignUp JSON Format-----');
    console.log(data);
    // APIs
    this.instituteConfigService.saveDivision(data)
      .then((res: ApiResponse) => {
        if (res.statusCode == 200) {
          this.getDivision();
          this.resetTeamForm();
        }
      });
  }

  resetTeamForm() {
    this.divisionForm.reset();
  }

  rerender(): void {
    this.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
       dtInstance.destroy();
       this.dtTrigger.next();     
    });
  }
}