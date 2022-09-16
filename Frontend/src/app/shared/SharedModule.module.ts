import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { Footer404Component } from './footer404/footer404.component';


@NgModule({
  declarations: [
    HeaderComponent,
    FooterComponent,
    Footer404Component
  ],
  imports: [
    CommonModule
  ],
  exports:[
    HeaderComponent,
    FooterComponent,
    Footer404Component
  ]
})
export class SharedModule { }
