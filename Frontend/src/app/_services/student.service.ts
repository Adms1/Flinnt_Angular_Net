import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { API } from '../_api-routes/api-routes';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  constructor(private apiService: ApiService,
    private http: HttpClient) { }

  saveStudent(params) {
    return this.apiService.post(`${API.STUDENT_ROUTES.saveStudent}`, params, true);
  }

  importFinalStudentData(params) {
    return this.apiService.post(`${API.STUDENT_ROUTES.importFinalData}`, params, true);
  }

  importStudent(params): Observable<any> {
    return this.http.post
      (environment.APP_URL + `${API.STUDENT_ROUTES.importStudent}`, params, {
        reportProgress: true,
        observe: 'events'
      })
      .pipe(map(this.extractData))
      .pipe(catchError(this.handleError.bind(this)));
  }

  private extractData(res: any) {
    const body = res;
    if (body === 0) return 0;
    return body || {};
  }

  private handleError(error: any) {
    const errMsg = (error.message) ? error.message :
      error.status ? `${error.status} - ${error.statusText}` : 'Server error';
    console.error(errMsg);
    return throwError(errMsg);
  }
}
