import { Injectable } from '@angular/core';
import { API } from '../_api-routes/api-routes';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  constructor(private apiService: ApiService) { }

  saveStudent(params) {
    return this.apiService.post(`${API.STUDENT_ROUTES.saveStudent}`, params, true);
  }
}
