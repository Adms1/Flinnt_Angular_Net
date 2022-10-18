import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddParentComponent } from './add-parent/add-parent.component';
import { ImportParentComponent } from './import-parent/import-parent.component';
import { ParentHomeComponent } from './parent-homecomponent';
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
      }
    ]
  }, 
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ParentRoutingModule { }
