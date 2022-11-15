import { Injectable } from '@angular/core';
import { API } from '../_api-routes/api-routes';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private apiService: ApiService) { }

  getUserByEmail(params) {
    return this.apiService.get(`${API.USER_ROUTES.getUserByEmailId}?emailid=${params}`, true, true);
  }

  getUserById(params) {
    return this.apiService.get(`${API.USER_ROUTES.getUserById}?userId=${params}`, true, true);
  }
}
