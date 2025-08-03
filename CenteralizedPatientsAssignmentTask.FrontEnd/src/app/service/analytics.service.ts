import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AnalyticsService {
  private hubConnection!: signalR.HubConnection;
  private readonly API_URL = 'http://localhost:5277';
  constructor(private http: HttpClient) { }

  private hospitalData$ = new BehaviorSubject<any[]>([]);
  private monthlyData$ = new BehaviorSubject<any[]>([]);

  hospitalChart$ = this.hospitalData$.asObservable();
  monthlyChart$ = this.monthlyData$.asObservable();

  startConnection(): void {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${this.API_URL}/patientHub`) // Adjust backend URL
      .withAutomaticReconnect()
      .build();

    this.hubConnection
      .start()
      .catch(err => console.error('SignalR Connection Error: ', err));

    this.hubConnection.on('ReceiveChartData', (data) => {
      this.hospitalData$.next(data.hospitals);
      this.monthlyData$.next(data.monthly);
    });
  }

  getInitialData(): Observable<{ hospitals: any[], monthly: any[] }> {
    return this.http.get<{ hospitals: any[], monthly: any[] }>(`${this.API_URL}/api/Analytics/analytics`);
  }
}
