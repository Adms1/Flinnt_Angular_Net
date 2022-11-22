import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { UtilityService } from "./utility.service";
import { Router } from "@angular/router";
import { map } from "rxjs/operators";
import { Constants } from "../_helpers/constants";

@Injectable({
  providedIn: "root",
})
export class ApiService {
  URL = environment.APP_URL; // endpoint URL
  headers: any;
  constructor(
    private http: HttpClient,
    private utility: UtilityService,
    private router: Router
  ) { }

  async getHeaders(tokenRequired, formData?) {
    const token: string = localStorage.getItem(Constants.TOKEN);
    if (tokenRequired) {
      const headers = new HttpHeaders()
        .set("authorization", "bearer " + token)
        .set("Content-Type", "application/json");
      headers.set("Access-Control-Allow-Origin", "*");
      //headers.set("Access-Control-Allow-Methods", "GET, POST, DELETE, PUT");
      headers.set(
        "Access-Control-Allow-Headers",
        "X-Requested-With,content-type"
      );
      return headers;
    } else if (formData) {
      const headers = new HttpHeaders().set("authorization", "bearer " + token);
      return headers;
    } else {

      const headers = new HttpHeaders().set(
        "Content-Type",
        "application/json"
      );
      return headers;
    }
  }

  // this function should be used for fetch details without header token
  async get(path: any, authorize, loaderContinue?, showLoadder = true) {
    return new Promise(async (resolve, _) => {
      const success = (res) => {
        // toaster success message
        this.showToastrMsg(res);
        if (loaderContinue) {
          this.utility.hideLoading();
        }
        resolve(res);
      };
      const error = (err) => {
        return this.handleError(err);
      };
      const netowrkIsConnected = await this.getNetworkConnection();
      if (netowrkIsConnected) {
        if (loaderContinue && showLoadder) {
          this.utility.showLoading();
        }
        if (!loaderContinue && showLoadder) {
          this.utility.showLoading();
        }
        const headers = await this.getHeaders(authorize, false);
        return this.http
          .get(`${this.URL}${path}`, { headers })
          .pipe(map((res) => res))
          .subscribe(success, error);
      } else {
        this.utility.hideLoading();
        this.utility.showErrorToast(Constants.NO_INTERNET_CONNECTION_MSG);
      }
    });
  }

  // this function should be used for post details without header token
  async post(path: any, obj: any, authorize) {
    return new Promise(async (resolve, _) => {
      const success = (res) => {
        this.showToastrMsg(res);
        this.utility.hideLoading();
        resolve(res);
      };
      const error = (err) => {
        return this.handleError(err);
      };
      const netowrkIsConnected = await this.getNetworkConnection();
      if (netowrkIsConnected) {
        this.utility.showLoading();
        const headers = await this.getHeaders(authorize, false);
        return this.http
          .post<any>(`${this.URL}${path}`, obj, { headers })
          .pipe(map((res) => res))
          .subscribe(success, error);
      } else {
        this.utility.hideLoading();
        this.utility.showErrorToast(Constants.NO_INTERNET_CONNECTION_MSG);
      }
    });
  }

  async put(path: any, obj: any, authorize) {
    return new Promise(async (resolve, _) => {
      const success = (res) => {
        this.showToastrMsg(res);
        this.utility.hideLoading();
        resolve(res);
      };
      const error = (err) => {
        return this.handleError(err);
      };
      const netowrkIsConnected = await this.getNetworkConnection();
      if (netowrkIsConnected) {
        this.utility.showLoading();
        const headers = await this.getHeaders(authorize, false);
        return this.http
          .put<any>(`${this.URL}${path}`, obj, { headers })
          .pipe(map((res) => res))
          .subscribe(success, error);
      } else {
        this.utility.hideLoading();
        this.utility.showErrorToast(Constants.NO_INTERNET_CONNECTION_MSG);
      }
    });
  }
  // this function should be used for post details with header token

  async postFormDataReqWithToken(path: any, obj: FormData, authorize) {
    return new Promise(async (resolve, _) => {
      const success = (res) => {
        if (res && res.message) {
          this.showToastrMsg(res);
        }
        this.utility.hideLoading();
        resolve(res);
      };
      const error = (err) => {
        return this.handleError(err);
      };
      const netowrkIsConnected = await this.getNetworkConnection();
      if (netowrkIsConnected) {
        this.utility.showLoading();

        if (authorize) {
          this.headers = new HttpHeaders()
            .set(
              "authorization",
              "bearer " + localStorage.getItem(Constants.TOKEN)
            )
            .set("Accept", "application/json");
        }
        return this.http
          .post<any>(`${this.URL}${path}`, obj, {
            headers: this.headers,
          })
          .subscribe(success, error);
      } else {
        this.utility.hideLoading();
        this.utility.showErrorToast(Constants.NO_INTERNET_CONNECTION_MSG);
      }
    });
  }

  async uploadFileWithProgress(path: any, obj: any) {
    const netowrkIsConnected = await this.getNetworkConnection();
    if (netowrkIsConnected) {
      this.utility.showLoading();
      const headers = await this.getHeaders(false, true);
      return this.http.post<any>(`${this.URL}${path}`, obj, {
        headers,
        reportProgress: true,
        observe: "events",
      });
    } else {
      this.utility.hideLoading();
      this.utility.showErrorToast(Constants.NO_INTERNET_CONNECTION_MSG);
    }
  }

  async uploadImageWithProgress(path: any, obj: FormData) {
    return new Promise(async (resolve, _) => {
      const success = (res) => {
        if (res && res.message) {
          this.showToastrMsg(res);
        }
        this.utility.hideLoading();
        resolve(res);
      };
      const error = (err) => {
        return this.handleError(err);
      };
      const netowrkIsConnected = await this.getNetworkConnection();
      if (netowrkIsConnected) {
        this.utility.showLoading();
        return this.http
          .post<any>(`${this.URL}${path}`, obj)
          .subscribe(success, error);
      } else {
        this.utility.hideLoading();
        this.utility.showErrorToast(Constants.NO_INTERNET_CONNECTION_MSG);
      }
    });
  }

  async postExportData(path: any, obj: any) {
    return new Promise(async (resolve, _) => {
      const success = (res) => {
        if (res && res.message) {
          this.showToastrMsg(res);
        }
        this.utility.hideLoading();
        resolve(res);
      };
      const error = (err) => {
        return this.handleError(err);
      };
      this.utility.showLoading();
      const options = {
        headers: await this.getHeaders(false, true),
        responseType: "text" as "json",
      };
      return this.http
        .post<any>(this.URL + path, obj, options)
        .subscribe(success, error);
    });
  }

  async delete(path: any, authorize, isStopLoader = true) {
    return new Promise(async (resolve, _) => {
      const success = (res) => {
        // toaster success message
        this.showToastrMsg(res);
        if (isStopLoader) {
          this.utility.hideLoading();
        }
        resolve(res);
      };
      const error = (err) => {
        return this.handleError(err);
      };
      const netowrkIsConnected = await this.getNetworkConnection();
      if (netowrkIsConnected) {
        this.utility.showLoading();
        const headers = await this.getHeaders(authorize, false);
        return this.http
          .delete(`${this.URL}${path}`, { headers })
          .subscribe(success, error);
      } else {
        this.utility.hideLoading();
        this.utility.showErrorToast(Constants.NO_INTERNET_CONNECTION_MSG);
      }
    });
  }

  async getFile(url: any) {
    return new Promise<Blob>(async (resolve, _) => {
      const success = (res) => {
        // toaster success message
        if (res && res.message) {
          this.showToastrMsg(res);
        }
        resolve(res);
      };
      const error = (err) => {
        //const errMsg = this.translation.instant('productAdd.imageErrorMsg');
        this.utility.showErrorToast(err);
        return this.handleError(err);
      };
      const netowrkIsConnected = await this.getNetworkConnection();
      if (netowrkIsConnected) {
        return this.http
          .get(url, { responseType: "blob" })
          .subscribe(success, error);
      } else {
        this.utility.hideLoading();
        this.utility.showErrorToast(Constants.NO_INTERNET_CONNECTION_MSG);
      }
    });
  }

  showToastrMsg(res) {
    if (res) {
      const msg = res.message || res.msg || (res.result && res.result.msg);
      if (msg) {
        this.utility.showSuccessToast(msg);
      }
    }
  }

  getNetworkConnection() {
    const isOnline: boolean = navigator.onLine;
    if (isOnline) {
      return true;
    }
    return false;
  }

  handleError(err) {
    if (err.status === 400) {
      const error =
        err.error.error || err.error.message
          ? err.error.error || err.error.message
          : "Internal Server Error";
      this.utility.showErrorToast(error);
      this.utility.hideLoading();
    } else if (err.status === 401) {
      const error = err.error.error ? err.error.error : "Session Expired";
      this.utility.showErrorToast(error);
      this.utility.hideLoading();
      localStorage.clear();
      this.router.navigate(["/"]);
    } else if (err.status === 403) {
      const error = err.error.error ? err.error.error : "Insufficient Rights";
      this.utility.showErrorToast(error);
      this.utility.hideLoading();
      localStorage.clear();
      this.router.navigate(["/"]);
    } else if (err.status === 404) {
      const error = err.error.error ? err.error.error : "Internal Server Error";
      this.utility.showErrorToast(error);
      this.utility.hideLoading();
    } else if (err.status === 500) {
      const error = err.error.error ? err.error.error : "Internal Server Error";
      this.utility.showErrorToast(error);
      this.utility.hideLoading();
    } else {
      this.utility.hideLoading();
    }
  }
}
