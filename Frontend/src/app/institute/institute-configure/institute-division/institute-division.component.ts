import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { Division } from 'src/app/_models/division';
import { ApiResponse } from 'src/app/_models/response';
import { InstituteConfigureService } from 'src/app/_services/institute-configure.service';

@Component({
  selector: 'app-institute-division',
  templateUrl: './institute-division.component.html',
  styleUrls: ['./institute-division.component.css']
})
export class InstituteDivisionComponent implements OnInit {
  divisions : Division[] = [];
  dtOptions: DataTables.Settings = {};
  // We use this trigger because fetching the list of divison can be quite long,
  // thus we ensure the data is fetched before rendering
  dtTrigger: Subject<any> = new Subject<any>();
  constructor(private instituteConfigService: InstituteConfigureService) { }

  ngOnInit(): void {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10
    };

    this.getDivision();
  }

  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    this.dtTrigger.unsubscribe();
  }

  getDivision() {
    this.instituteConfigService.getDivision()
      .then((res: ApiResponse) => {
        if (res.statusCode == 200) {
          this.divisions = res.data;
          this.dtTrigger.next();
        }
      });
  }
}
