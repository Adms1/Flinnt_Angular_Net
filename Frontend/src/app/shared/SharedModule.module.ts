import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { Footer404Component } from './footer404/footer404.component';
import { DataTablesModule } from "angular-datatables";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

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
  ],
  exports:[
    HeaderComponent,
    FooterComponent,
    Footer404Component,
    DataTablesModule,
    FormsModule,
    ReactiveFormsModule,
  ]
})
export class SharedModule { }
