import { Injectable } from '@angular/core';
import { API } from '../_api-routes/api-routes';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class InstituteConfigureService {
  instituteTypeId=null;
  groupStructureId=null;
  boardId=null;
  mediumId=null;
  constructor(private apiService: ApiService) { }

  getInstituteConfigureSession(instituteId) {
    return this.apiService.get(`${API.INSTITUTE_CONFIG_ROUTE.getInstituteConfigureSession}/${instituteId}`, true, true);
  }

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

  getGroup(instituteId) {
    return this.apiService.get(`${API.INSTITUTE_CONFIG_ROUTE.getInstituteGroup}/${instituteId}`, true, true);
  }

  getSpecificInstituteGroup(instituteId, boardId, mediumId, standardId) {
    return this.apiService.get(`${API.INSTITUTE_CONFIG_ROUTE.getSpecificInstituteGroup}?instituteId=${instituteId}&boardId=${boardId}&mediumId=${mediumId}&standardId=${standardId}`, true, true);
  }

  getDivision(instituteId) {
    return this.apiService.get(`${API.INSTITUTE_CONFIG_ROUTE.getInstituteDivision}/${instituteId}`, true, true);
  }

  getDivisionByInstituteGroupId(instituteId, instituteGroupId) {
    return this.apiService.get(`${API.INSTITUTE_CONFIG_ROUTE.getDivisionByInstituteGroupId}/${instituteId}/${instituteGroupId}`, true, true);
  }

  saveInstituteGroup(params){
    return this.apiService.post(`${API.INSTITUTE_CONFIG_ROUTE.saveInstituteGroup}`, params, true);
  }

  saveDivision(params){
    return this.apiService.post(`${API.INSTITUTE_CONFIG_ROUTE.saveInstituteDivision}`, params, true);
  }

  deleteDivision(id){
    return this.apiService.delete(`${API.INSTITUTE_CONFIG_ROUTE.deleteInstituteDivision}/${id}`, true);
  }

  saveInstituteConfigureSession(params){
    return this.apiService.post(`${API.INSTITUTE_CONFIG_ROUTE.saveInstituteConfigureSession}`, params, true);
  }
}
