import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  @Input() roleId:number = 0;
  @Input() userObj:any = {};

  constructor() { }

  ngOnInit(): void {
  }

}
