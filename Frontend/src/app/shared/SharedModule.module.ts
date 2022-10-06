import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { Footer404Component } from './footer404/footer404.component';
import { DataTablesModule } from "angular-datatables";

@NgModule({
  declarations: [
    HeaderComponent,
    FooterComponent,
    Footer404Component
  ],
  imports: [
    CommonModule,
    DataTablesModule
  ],
  exports:[
    HeaderComponent,
    FooterComponent,
    Footer404Component,
    DataTablesModule
  ]
})
export class SharedModule { }
