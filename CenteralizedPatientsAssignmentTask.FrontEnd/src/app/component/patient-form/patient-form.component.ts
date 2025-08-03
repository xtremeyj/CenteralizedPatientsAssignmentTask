import { Component, Inject, OnInit } from '@angular/core';
import { EmailValidator, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Patient } from '../../interface/interface';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-patient-form',
  templateUrl: './patient-form.component.html',
  styleUrls: ['./patient-form.component.scss'],
})
export class PatientFormComponent implements OnInit {
  patientForm!: FormGroup;
  statuses = ['Active', 'Discharged', 'Deceased'];
  genders = ['Male', 'Female', 'Other'];

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<PatientFormComponent>,
    @Inject(MAT_DIALOG_DATA) public patient: Patient
  ) { }

  ngOnInit() {
    console.log(this.patient);
    this.patientForm = this.fb.group({
      patientId: [this.patient?.patientId],
      name: [this.patient?.name || '', [Validators.required, Validators.minLength(3)]],
      email: [this.patient?.email || '', [Validators.required, Validators.email]],
      age: [this.patient?.age || '', [Validators.required, Validators.min(0), Validators.max(150)]],
      gender: [this.patient?.gender || '', Validators.required],
      hospitalName: [this.patient?.hospitalName || '', Validators.required],
      diagnosis: [this.patient?.diagnosis || ''],
      contactNo: [this.patient?.contactNo || '', [Validators.required, Validators.pattern('^[0-9]{10}$')]],
      status: [this.patient?.status || '', Validators.required],
      createdDate: [this.patient?.createdDate || new Date(), Validators.required],
    });
  }

  onSubmit() {
    if (this.patientForm.invalid) {
      this.patientForm.markAllAsTouched();
      return;
    }
    this.dialogRef.close(this.patientForm.value); // send data back and close
  }

  onCancel() {
    this.dialogRef.close(); // close without sending data
  }

  get isEditMode(): boolean {
    return !!this.patientForm.get('patientId')?.value;
  }
}
