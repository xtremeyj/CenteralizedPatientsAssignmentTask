import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { SelectionModel } from '@angular/cdk/collections';
import { PatientService } from '../../../service/patient.service';
import { Patient } from '../../../interface/interface';
import { PatientFormComponent } from '../../patient-form/patient-form.component';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from '../../../service/auth.service';

@Component({
  selector: 'app-patient-list',
  templateUrl: './patient-list.component.html',
  styleUrls: ['./patient-list.component.scss'],
})
export class PatientListComponent implements OnInit {
  displayedColumns: string[] = [
    'select',
    'patientId',
    'name',
    'age',
    'gender',
    'hospitalName',
    'diagnosis',
    'contactNo',
    'status',
    'createdDate',
    'actions',
  ];

  dataSource = new MatTableDataSource<Patient>([]);
  selection = new SelectionModel<Patient>(true, []);

  totalRecords = 0;
  pageSize = 50;

  filterForm: FormGroup;

  statuses = ['Active', 'Inactive', 'Deceased'];
  genders = ['Male', 'Female'];

  selectedPatientIds = new Set<string>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  @ViewChild('viewDialog') viewDialogTemplate!: TemplateRef<any>;
  hasUserEditAccess: boolean = false;

  constructor(private patientService: PatientService, private fb: FormBuilder,
    private snackBar: MatSnackBar,
    private dialog: MatDialog,
    private router: Router,
    private auth: AuthService) {
    this.filterForm = this.fb.group({
      search: [''],
      hospital: [''],
      status: [''],
      gender: [''],
    });
  }

  ngOnInit() {
    this.loadPatients();
    this.hasUserEditAccess = this.auth.hasEditAccess();
  }

  loadPatients() {
    const filters = {
      ...this.filterForm.value,
      page: (this.paginator?.pageIndex ?? 0) + 1,
      pageSize: this.paginator?.pageSize ?? this.pageSize,
      sortField: this.sort?.active ?? '',
      sortDirection: this.sort?.direction ?? '',
    };

    this.patientService.getPatients(filters).subscribe({
      next: (res) => {
        this.totalRecords = res.totalCount;
        this.dataSource.data = res.pageData;

        // Maintain selection across pages
        this.selection.clear();
        //res.data.forEach((row) => {
        //  if (this.selectedPatientIds.has(row.patientId)) {
        //    this.selection.select(row);
        //  }
        //});

        if (Array.isArray(res.pageData)) {
          res.pageData.forEach((row) => {
            if (this.selectedPatientIds.has(row.patientId)) {
              this.selection.select(row);
            }
          });
        }
      },
      error: (err) => {
        console.error('Failed to load patients', err);
      },
    });
  }

  onPageChange(event: PageEvent) {
    this.loadPatients();
  }

  onSortChange(sort: Sort) {
    this.loadPatients();
  }

  applyFilters() {
    this.paginator.firstPage();
    this.loadPatients();
  }

  toggleAllRows(event: any) {
    if (event.checked) {
      this.dataSource.data.forEach((row) => {
        this.selection.select(row);
        this.selectedPatientIds.add(row.patientId);
      });
    } else {
      this.dataSource.data.forEach((row) => {
        this.selection.deselect(row);
        this.selectedPatientIds.delete(row.patientId);
      });
    }
  }

  toggleRow(row: Patient) {
    this.selection.toggle(row);
    if (this.selection.isSelected(row)) {
      this.selectedPatientIds.add(row.patientId);
    } else {
      this.selectedPatientIds.delete(row.patientId);
    }
  }

  isAllSelected() {
    return (
      this.dataSource.data.length > 0 &&
      this.dataSource.data.every((row) => this.selection.isSelected(row))
    );
  }

  editPatient(patient: Patient) {
    console.log(patient);
    // Implement your edit logic here
    this.openEditPatientDialog(patient);
  }

  deletePatient(patient: Patient) {
    // Implement your delete logic here
    this.patientService.deletePatient(patient.patientId).subscribe({
      next: res => {
        this.snackBar.open(patient.name + 'Patient details deleted successfully!', 'Close', { duration: 3000 });
      },
      error: () => {
        this.snackBar.open(patient.name + 'Failed to Delete patient details.', 'Close', { duration: 3000 });
      }
    });
  }

  savePatient(patient: Patient) {
    if (patient.patientId) {
      // update
      this.patientService.updatePatient(patient.patientId, patient).subscribe({
        next: res => {
          this.snackBar.open('Patient details updated successfully!', 'Close', { duration: 3000 });
        },
        error: () => {
          this.snackBar.open('Failed to update patient details.', 'Close', { duration: 3000 });
        }
      });
    } else {
      // create
      this.patientService.createPatient(patient).subscribe({
        next: res => {
          this.snackBar.open('Patient details saved successfully!', 'Close', { duration: 3000 });
        },
        error: () => {
          this.snackBar.open('Failed to save patient details.', 'Close', { duration: 3000 });
        }
      });
    }
  }

  onAddPatient() {
    this.openAddPatientDialog();
  }

  onGoBack() {
    // Navigate back to dashboard, adjust route as needed
    this.router.navigate(['/dashboard']);
  }

  exportToExcel() {
    // Implement export to Excel
  }

  exportToCSV() {
    // Implement export to CSV
  }

  openAddPatientDialog() {
    const dialogRef = this.dialog.open(PatientFormComponent, {
      width: '600px',
      data: null,
      autoFocus: true,
      restoreFocus: true,
      disableClose: false // Make sure it's not trapping clicks
    });

    dialogRef.afterClosed().subscribe((result: Patient | undefined) => {
      if (result) {
        this.savePatient(result);
      }
    });
  }

  openEditPatientDialog(patient: Patient) {
    console.log(patient);
    const dialogRef = this.dialog.open(PatientFormComponent, {
      width: '600px',
      data: patient,
      autoFocus: true,
      restoreFocus: true,
      disableClose: false // Make sure it's not trapping clicks
    });

    dialogRef.afterClosed().subscribe((result: Patient | undefined) => {
      if (result) {
        this.savePatient(result);
      }
    });
  }

  onLogout() {
    this.auth.logout();
  }

  viewPatient(patient: Patient): void {
    this.dialog.open(this.viewDialogTemplate, {
      width: '400px',
      data: patient,
    });
  }
}
