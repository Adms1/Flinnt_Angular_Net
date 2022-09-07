import { Injectable } from '@angular/core';
import { API } from '../_api-routes/api-routes';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class InstituteService {

  constructor(private apiService: ApiService) { }

  saveInstitute(params) {
    return this.apiService.post(`${API.INSTITUTE_ROUTES.saveInstitute}`, params, true);
  }
}
