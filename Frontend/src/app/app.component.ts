import { Component, OnInit } from '@angular/core';
import { UtilityService } from './_services/utility.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'flinnt';

  constructor(
    private utilityService: UtilityService
  ) { }

  ngOnInit(): void {
    this.utilityService.showLoading();
  }

  ngAfterViewInit(){
    this.utilityService.hideLoading();
  }
}
