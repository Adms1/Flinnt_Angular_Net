import { Injectable } from '@angular/core';
import { API } from '../_api-routes/api-routes';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class ParentService {

  constructor(private apiService: ApiService) { }

  saveParent(params) {
    return this.apiService.post(`${API.PARENT_ROUTES.saveParent}`, params, true);
  }
}