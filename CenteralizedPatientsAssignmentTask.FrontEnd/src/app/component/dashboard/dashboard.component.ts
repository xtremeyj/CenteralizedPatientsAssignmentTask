import { Component, OnInit } from '@angular/core';
import { AnalyticsService } from '../../service/analytics.service';
import { EChartsOption } from 'echarts';
import { Router } from '@angular/router';
import { AuthService } from '../../service/auth.service';
@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html'
})
export class DashboardComponent implements OnInit {
  pieChartOptions: any;
  lineChartOptions: any;
  baseLineChartOptions: EChartsOption = {
    title: { text: 'Monthly Admissions', left: 'center' },
    tooltip: { trigger: 'axis' },
    xAxis: { type: 'category', data: [] },
    yAxis: { type: 'value' },
    series: [{
      name: 'Admissions',
      type: 'line',
      data: [],
      smooth: true,
      lineStyle: {
        color: '#42A5F5',
        width: 2
      }
    }]
  };
  baseChartOptions = {
    tooltip: { trigger: 'item' },
    series: [{ type: 'pie', radius: '50%', data: [] }]
  };

  constructor(private analyticsService: AnalyticsService, private router: Router, private auth: AuthService) { }

  ngOnInit(): void {
    this.analyticsService.startConnection();

    this.analyticsService.getInitialData().subscribe(data => {

      this.updateCharts(data);

    });

    this.analyticsService.hospitalChart$.subscribe(hospitals => {
      this.updatePieChart(hospitals);
    });

    this.analyticsService.monthlyChart$.subscribe(monthly => {
      this.updateLineChart(monthly);
    });
  }

  updateCharts(data: { hospitals: any[], monthly: any[] }) {
    this.updatePieChart(data.hospitals);
    this.updateLineChart(data.monthly);
  }

  updatePieChart(hospitals: any[]) {
    setTimeout(() => {
      this.pieChartOptions = {
        title: { text: 'Patients Per Hospital', left: 'center' },
        series: [{
          data: hospitals.map(d => ({
            name: d.hospitalName,
            value: d.patientCount
          }))
        }]
      };
    }, 0);
  }

  updateLineChart(monthly: any[]) {
    setTimeout(() => {
      this.lineChartOptions = {
        xAxis: {
          data: monthly.map(d => d.month)
        },
        series: [{
          data: monthly.map(d => d.count)
        }]
      };
    }, 0);
  }

  goToPatientList() {
    this.router.navigate(['/patients']); // Adjust route path if needed
  }

  onLogout() {
    this.auth.logout();
  }
}
