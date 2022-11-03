import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { Footer404Component } from './footer404/footer404.component';
import { DataTablesModule } from "angular-datatables";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgSelect2Module } from 'ng-select2';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';  

@NgModule({
  declarations: [
    HeaderComponent,
    FooterComponent,
    Footer404Component
  ],
  imports: [
    CommonModule,
    DataTablesModule,
    FormsModule,
    ReactiveFormsModule,
    NgSelect2Module,
    NgbModule
  ],
  exports:[
    HeaderComponent,
    FooterComponent,
    Footer404Component,
    DataTablesModule,
    FormsModule,
    ReactiveFormsModule,
    NgSelect2Module,
    NgbModule
  ]
})
export class SharedModule { }
