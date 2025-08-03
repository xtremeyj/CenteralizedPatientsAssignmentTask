import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Patient, PatientResponse } from '../interface/interface';

@Injectable({ providedIn: 'root' })
export class PatientService {
  private apiUrl = 'http://localhost:5277/api/Patient';

  constructor(private http: HttpClient) { }

  getPatients(filters: any): Observable<PatientResponse> {
    let params = new HttpParams()
      .set('page', filters.page)
      .set('pageSize', filters.pageSize);

    if (filters.search) params = params.set('search', filters.search);
    if (filters.hospital) params = params.set('hospital', filters.hospital);
    if (filters.gender) params = params.set('gender', filters.gender);
    if (filters.status) params = params.set('status', filters.status);
    if (filters.sortField) params = params.set('sortField', filters.sortField);
    if (filters.sortDirection) params = params.set('sortDirection', filters.sortDirection);

    return this.http.get<PatientResponse>(this.apiUrl, { params });
  }


  createPatient(patient: Patient): Observable<Patient> {
    return this.http.post<Patient>(this.apiUrl, patient);
  }

  updatePatient(id: string, patient: Patient): Observable<boolean> {
    return this.http.put<boolean>(`${this.apiUrl}/${id}`, patient);
  }

  deletePatient(id: string): Observable<boolean> {
    return this.http.delete<boolean>(`${this.apiUrl}/${id}`);
  }

  //exportPatients(filters: any): Observable<any[]> {
  //  return this.http.post<any[]>(`${this.apiUrl}/export`, filters);
  //}
}
