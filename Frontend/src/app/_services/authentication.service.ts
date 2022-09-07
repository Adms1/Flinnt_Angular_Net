import { Injectable } from '@angular/core';
import { API } from '../_api-routes/api-routes';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private apiService: ApiService) { }

  doLogin(params) {
    return this.apiService.post(`${API.LOGIN_ROUTES.login}`, params, true);
  }
}
