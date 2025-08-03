import { NgModule } from '@angular/core'; 

import { PatientRoutingModule } from './patient-routing-module'; 
import { PatientListComponent } from '../../component/patients/patient-list/patient-list.component';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [PatientListComponent],
  imports: [ 
    PatientRoutingModule, SharedModule
  ]
})
export class PatientModule { }
