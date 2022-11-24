import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { DataTableDirective } from 'angular-datatables';
import { Subject } from 'rxjs';
import { Constants } from 'src/app/_helpers/constants';
import { ParentService } from 'src/app/_services/parent.service';

@Component({
  selector: 'app-search-parent',
  templateUrl: './search-parent.component.html',
  styleUrls: ['./search-parent.component.css']
})
export class SearchParentComponent implements OnInit {
  @Output() selectedParent: EventEmitter<any> = new EventEmitter();
  @ViewChild(DataTableDirective, { static: false })
  dtElement: DataTableDirective;
  dtOptions: DataTables.Settings = {};
  // We use this trigger because fetching the list of divison can be quite long,
  // thus we ensure the data is fetched before rendering
  dtTrigger: Subject<any> = new Subject<any>();
  rowsOnPage = Constants.ROWS_ON_PAGE;
  searchSort: any;
  filterValue = [];
  columns = [
    "Parent1FirstName",
    "Parent1LastName",
    "Parent1EmailId",
    "Parent1MobileNo",
    "SingleParent",
    "Parent1Relationship"
  ];
  page: any;
  parentList = [];
  searchParentForm = {} as FormGroup;

  constructor(public activeModal: NgbActiveModal,
    private parentSevice: ParentService,
    private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.createSearchParentForm();
    this.configureSearchSort();
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10
    };
    this.filterData();
  }

  configureSearchSort() {
    this.filterValue = [];

    this.page = {
      pageNumber: 0,
      size: this.rowsOnPage,
    };
    this.searchSort = {
      Page: this.page.pageNumber + 1,
      PageSize: Constants.ROWS_ON_PAGE,
      Columns: [],
      Search: {
        Value: '',
        ColumnNameList: [],
        Regex: 'string'
      },
      Order: [{
        Column: 0,
        ColumnName: '',
        Dir: 'asc'
      }]
    }
  }

  createSearchParentForm() {
    this.searchParentForm = this.formBuilder.group({
      firstName: [''],
      lastName: [''],
      emailId: [''],
      mobileNo: [''],
      relationship: [''],
      singleParent: ['']
    });
  }

  ngAfterViewInit() {
    this.dtTrigger.next();
  }

  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    this.dtTrigger.unsubscribe();
  }

  onAccountRowSelect(event) {
    this.selectedParent.emit(event);
    this.activeModal.close();
  }

  rerender(): void {
    this.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
      dtInstance.destroy();
      this.dtTrigger.next();
    });
  }

  onSearch() {
    this.filterData();
  }

  onClearSearch() {
    this.resetTeamForm();
    this.filterData();
  }

  filterData() {
    this.filterValue.push(this.searchParentForm.get("firstName").value);
    this.filterValue.push(this.searchParentForm.get("lastName").value);
    this.filterValue.push(this.searchParentForm.get("emailId").value);
    this.filterValue.push(this.searchParentForm.get("mobileNo").value);

    this.columns.forEach((element, i) => {
      if (i < 4) {
        const obj = {
          Data: "string",
          Searchable: true,
          Orderable: true,
          Name: element,
          Search: {
            Value: this.filterValue[i],
            Regex: "string",
          },
        };
        this.searchSort.Columns.push(obj);
      }
    });

    if (!!this.searchParentForm.get("singleParent").value
      && this.searchParentForm.get("singleParent").value != "") {
      this.filterValue.push();

      const obj = {
        Data: "bool",
        Searchable: true,
        Orderable: true,
        Name: "SingleParent",
        Search: {
          Value: this.searchParentForm.get("singleParent").value,
          Regex: "string",
        },
      };
      this.searchSort.Columns.push(obj);
    }

    if (!!this.searchParentForm.get("relationship").value
      && this.searchParentForm.get("relationship").value != "") {

      const obj = {
        Data: "string",
        Searchable: true,
        Orderable: true,
        Name: "Parent1Relationship",
        Search: {
          Value: this.searchParentForm.get("relationship").value,
          Regex: "string",
        },
      };
      this.searchSort.Columns.push(obj);
    }

    this.parentSevice.dataFilter(this.searchSort).then(res => {
      this.configureSearchSort();
      if (res['data'] && res['data'].length > 0) {
        this.parentList = res['data'];
      } else { this.parentList = []; }
      this.rerender();
    }, err => {
      this.parentList = [];
      this.configureSearchSort();
    });
  }

  resetTeamForm() {
    this.searchParentForm.reset();
  }

  
  getSingleParent(item) : string {
    return item.singleParent == 0 ? "NO" : "YES";
  }
  
}
