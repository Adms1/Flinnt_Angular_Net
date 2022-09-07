import { Injectable } from '@angular/core';
import { API } from '../_api-routes/api-routes';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class CountryService {

  constructor(private apiService: ApiService) { }

  getCountries() {
    return this.apiService.get(`${API.COUNTRY_ROUTES.getCountries}`, true, true);
  }
}
