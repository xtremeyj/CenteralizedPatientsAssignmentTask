import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';   
import { PatientListComponent } from '../../component/patients/patient-list/patient-list.component';

const routes: Routes = [
  {
    path: '',
    component: PatientListComponent,
    title: 'Patient List'
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PatientRoutingModule { }
