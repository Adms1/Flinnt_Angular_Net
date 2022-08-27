import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormService } from './form.service';



@NgModule({
  declarations: [],
  imports: [
    HttpClientModule
  ],
  providers:[
    FormService
  ]
})
export class CoreModule { }
