import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './component/dashboard/dashboard.component';
import { authGuard } from './service/auth.guard';

const routes: Routes = [
  {
    path: 'patients',
    loadChildren: () => import('./modules/patient/patient-module').then((m) => m.PatientModule),
    canActivate: [authGuard],
  },
  {
    path: 'login',
    loadComponent: () => import('./component/login/login.component').then(m => m.LoginComponent)
  },
  {
    path: 'dashboard', component: DashboardComponent,
    canActivate: [authGuard]
  },
  {
    path: '**',
    redirectTo: '/login'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
