import { NgModule, CUSTOM_ELEMENTS_SCHEMA  } from '@angular/core';
import { CommonModule } from '@angular/common';

import { InstituteRoutingModule } from './institute-routing.module';
import { InstituteConfigureComponent } from './institute-configure/institute-configure.component';
import { NgWizardModule, NgWizardConfig, THEME } from 'ng-wizard';
import { SharedModule } from '../shared/SharedModule.module';
import { InstituteTypeComponent } from './institute-configure/institute-type/institute-type.component';
import { InstituteStandardComponent } from './institute-configure/institute-standard/institute-standard.component';
import { InstituteBoardComponent } from './institute-configure/institute-board/institute-board.component';
import { InstituteMediumComponent } from './institute-configure/institute-board/institute-medium/institute-medium.component';
import { InstituteGroupStructureComponent } from './institute-configure/institute-group-structure/institute-group-structure.component';
import { InstituteDivisionComponent } from './institute-configure/institute-division/institute-division.component';
import { InstituteConfigureService } from '../_services/institute-configure.service';

const ngWizardConfig: NgWizardConfig = {
  theme: THEME.default
};

@NgModule({
  declarations: [
    InstituteConfigureComponent,
    InstituteTypeComponent,
    InstituteStandardComponent,
    InstituteBoardComponent,
    InstituteMediumComponent,
    InstituteGroupStructureComponent,
    InstituteDivisionComponent
  ],
  imports: [
    NgWizardModule.forRoot(ngWizardConfig),
    CommonModule,
    InstituteRoutingModule,
    SharedModule
  ],
  providers:[
    InstituteConfigureService
  ],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ],
})
export class InstituteModule { }
