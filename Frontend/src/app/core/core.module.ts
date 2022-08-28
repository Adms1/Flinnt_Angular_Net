import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { NgxSpinnerModule, NgxSpinnerService } from 'ngx-spinner';
import { FormService } from './form.service';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [],
  imports: [
    HttpClientModule,
    NgxSpinnerModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot()
  ],
  providers:[
    FormService,
    NgxSpinnerService
  ],
  exports:[
    NgxSpinnerModule
  ]
})
export class CoreModule { }
