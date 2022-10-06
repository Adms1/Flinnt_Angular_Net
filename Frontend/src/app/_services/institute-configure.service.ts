import { Injectable } from '@angular/core';
import { API } from '../_api-routes/api-routes';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class InstituteConfigureService {

  constructor(private apiService: ApiService) { }

  getInstituteType() {
    return this.apiService.get(`${API.INSTITUTE_CONFIG_ROUTE.getInstituteType}`, true, true);
  }

  getGroupStructure() {
    return this.apiService.get(`${API.INSTITUTE_CONFIG_ROUTE.getInstituteGroupStructure}`, true, true);
  }

  getBoard() {
    return this.apiService.get(`${API.INSTITUTE_CONFIG_ROUTE.getInstituteBoard}`, true, true);
  }

  getMedium() {
    return this.apiService.get(`${API.INSTITUTE_CONFIG_ROUTE.getInstituteMedium}`, true, true);
  }

  getStandard() {
    return this.apiService.get(`${API.INSTITUTE_CONFIG_ROUTE.getInstituteStandard}`, true, true);
  }

  getDivision() {
    return this.apiService.get(`${API.INSTITUTE_CONFIG_ROUTE.getInstituteDivision}`, true, true);
  }
}
