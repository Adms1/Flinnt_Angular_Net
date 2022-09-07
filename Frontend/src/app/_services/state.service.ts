import { Injectable } from '@angular/core';
import { API } from '../_api-routes/api-routes';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class StateService {

  constructor(private apiService: ApiService) { }

  getStateByCountryId(countryId) {
    return this.apiService.get(`${API.STATE_ROUTES.getStateByCountryId}/${countryId}`, true, true);
  }
}
