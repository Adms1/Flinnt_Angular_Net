import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './_services/auth.guard';

const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./home/home.module').then(m=>m.HomeModule)
  },
  {
    path:"institute/configure",
    loadChildren: () => import('./institute/institute-configure/institute-configure.module').then(m=>m.InstituteModule),
    canActivate: [AuthGuard]
  },
  {
    path:"institute/dashboard",
    loadChildren: () => import('./institute/institute-dashboard/institute-dashboard.module').then(m=>m.InstituteDashboardModule),
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
