import { Injectable, EventEmitter } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
@Injectable({
  providedIn: 'root'
})
export class CommonService {

  eventEmit: EventEmitter<any> = new EventEmitter();
  menuAppend: EventEmitter<any> = new EventEmitter();
  private loadingCount = 0;

  constructor(private toastrService: ToastrService) {
  }

  public showError(errorMessage: string): void {
    this.toastrService.error(errorMessage, 'Error!', {
      positionClass: 'toast-top-center',
      timeOut: 3000, closeButton: true
    });
  }

  public showSuccess(successMessage: string): void {
    this.toastrService.success(successMessage, 'Success!', {
      positionClass: 'toast-top-center',
      timeOut: 3000, closeButton: true
    });
  }

  public showWarning(warningMessage: string): void {
    this.toastrService.warning(warningMessage, 'Warning!', {
      positionClass: 'toast-top-center',
      timeOut: 3000, closeButton: true
    });
  }

  public showLoading() {
    $('#loading').show();
    this.loadingCount++;
  }

  public closeLoading() {
    this.loadingCount--;
    if (this.loadingCount <= 0) {
      $('#loading').hide();
    }
  }
}
