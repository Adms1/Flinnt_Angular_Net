import { Injectable } from '@angular/core';
import { API } from '../_api-routes/api-routes';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class InstituteService {

  constructor(private apiService: ApiService) { }

  getInstitute(instituteId) {
    return this.apiService.get(`${API.INSTITUTE_ROUTES.getInstitute}/${instituteId}`, true);
  }

  saveInstitute(params) {
    return this.apiService.post(`${API.INSTITUTE_ROUTES.saveInstitute}`, params, true);
  }

  accountVerify(userId, otp){
    return this.apiService.post(`${API.INSTITUTE_ROUTES.accountVerify}/${userId}/${otp}`,null, true);
  }
}
