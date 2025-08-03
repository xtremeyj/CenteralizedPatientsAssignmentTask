import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { SharedModule } from './modules/shared/shared.module';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor } from './service/http.interceptor';
import { DashboardComponent } from './component/dashboard/dashboard.component';
import { NgxEchartsModule } from 'ngx-echarts';
import { PatientFormComponent } from './component/patient-form/patient-form.component';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    PatientFormComponent
  ],
  imports: [
    BrowserModule,
    SharedModule,
    NgxEchartsModule.forRoot({ echarts: () => import('echarts') }),
    AppRoutingModule
  ], providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
