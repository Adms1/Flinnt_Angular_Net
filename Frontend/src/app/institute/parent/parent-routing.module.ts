import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddParentComponent } from './add-parent/add-parent.component';
import { ImportParentUploadComponent } from './import-parent-upload/import-parent-upload.component';
import { ImportParentComponent } from './import-parent/import-parent.component';
import { ParentHomeComponent } from './parent-home/parent-homecomponent';
import { ParentComponent } from './parent/parent.component';

const routes: Routes = [
  {
    path: "",
    component: ParentHomeComponent,
    children:[
      {
        path: "",
        component: ParentComponent
      },
      {
        path: "add-parent",
        component: AddParentComponent
      },
      {
        path: "import-parent",
        component: ImportParentComponent
      },
    ]
  }, 
  {
    path: "import-parent-upload",
    component: ParentHomeComponent,
    children:[
      {
        path: "",
        component: ImportParentUploadComponent
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ParentRoutingModule { }
