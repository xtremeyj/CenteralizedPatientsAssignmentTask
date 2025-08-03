export interface Patient {
  patientId: string;
  name: string;
  age: number;
  gender: string;
  hospitalName: string;
  diagnosis: string;
  contactNo: string;
  email: string;
  status: string;
  createdDate: string;
}

export interface PatientResponse {
  totalCount: number;
  pageData: Patient[];
}

export interface AuthUser {
  username: string;
  role: 'Admin' | 'Viewer';
}
