import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ParentRoutingModule } from './parent-routing.module';
import { AddParentComponent } from './add-parent/add-parent.component';
import { ImportParentComponent } from './import-parent/import-parent.component';
import { SharedModule } from 'src/app/shared/SharedModule.module';
import { ParentComponent } from './parent/parent.component';
import { ParentHomeComponent } from './parent-homecomponent';


@NgModule({
  declarations: [
    AddParentComponent,
    ParentComponent,
    ImportParentComponent,
    ParentHomeComponent
  ],
  imports: [
    CommonModule,
    ParentRoutingModule,
    SharedModule
  ]
})
export class ParentModule { }
