import { Component, OnInit } from '@angular/core';

import $ from 'jquery';

@Component({
  selector: 'app-varify-account',
  templateUrl: './varify-account.component.html',
  styleUrls: ['./varify-account.component.css']
})
export class VarifyAccountComponent implements OnInit {
  roleId = 2;
  constructor() { }

  ngOnInit(): void {
  }

}