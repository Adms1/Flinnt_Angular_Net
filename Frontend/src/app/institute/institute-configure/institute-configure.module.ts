import { NgModule, CUSTOM_ELEMENTS_SCHEMA  } from '@angular/core';
import { CommonModule } from '@angular/common';

import { InstituteRoutingModule } from './institute-configure-routing.module';
import { InstituteConfigureComponent } from './institute-configure.component';
import { NgWizardModule, NgWizardConfig, THEME } from 'ng-wizard';
import { SharedModule } from '../../shared/SharedModule.module';
import { InstituteTypeComponent } from './institute-type/institute-type.component';
import { InstituteStandardComponent } from './institute-standard/institute-standard.component';
import { InstituteBoardComponent } from './institute-board/institute-board.component';
import { InstituteMediumComponent } from './institute-board/institute-medium/institute-medium.component';
import { InstituteGroupStructureComponent } from './institute-group-structure/institute-group-structure.component';
import { InstituteDivisionComponent } from './institute-division/institute-division.component';
import { InstituteConfigureService } from '../../_services/institute-configure.service';

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
