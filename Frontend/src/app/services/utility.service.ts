import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UtilityService {

  constructor() { }

  showSuccessToast(msg) {
    //this.toastr.success(msg);
  }

  showErrorToast(msg) {
    // this.toastr.error(msg);
  }

  showInfoToast(msg) {
    // this.toastr.info(msg);
  }

  showLoading() {
    //this.spinner.show();
  }

  hideLoading() {
    //this.spinner.hide();
  }
}
