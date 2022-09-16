import { NgModule, CUSTOM_ELEMENTS_SCHEMA  } from '@angular/core';
import { CommonModule } from '@angular/common';

import { InstituteRoutingModule } from './institute-routing.module';
import { InstituteConfigureComponent } from './institute-configure/institute-configure.component';
import { NgWizardModule, NgWizardConfig, THEME } from 'ng-wizard';
import { SharedModule } from '../shared/SharedModule.module';

const ngWizardConfig: NgWizardConfig = {
  theme: THEME.default
};

@NgModule({
  declarations: [
    InstituteConfigureComponent
  ],
  imports: [
    NgWizardModule.forRoot(ngWizardConfig),
    CommonModule,
    InstituteRoutingModule,
    SharedModule
  ],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ],
})
export class InstituteModule { }
