import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import { Observable } from 'rxjs';
import { ServerError } from './server-error';
import { Exception } from '../exception/exception';
import { PermissonException } from '../exception/permission.exception';
import { ValidationException } from '../exception/validation.exception';
import { ConcurrencyException } from '../exception/concurrency.exception';
import { ServerTimeoutException } from '../exception/server-timeout.exception';
import { UnhandledException } from '../exception/unhandled.exception';
import { contentHeaders, formDataHeaders } from './headers';
import { CommonService } from './common.service';
import { CacheService } from './cache.service';

const servicePath = '/SyTemplater/';
// const servicePath = '/BaseData/';


@Injectable()
export class ServiceBase {
    retryCount = 1; // Get this data from client configuration file
    retriedCount = 0;
    timeout = 60000; // Get this data from client configuration file
 
    constructor(private http: Http,
        private commonService: CommonService,
        private cacheService: CacheService) {
    }

    invokeService(url: string, data: any): Observable<any> {
        this.commonService.showLoading();
        const invokeUrl = servicePath + url;
        const jsondata = data == null ? null : JSON.stringify(data);

        return this.http.post(invokeUrl, jsondata, { headers: contentHeaders })
            .map(res => this.extractData(res))
            .catch((error) => {
                return this.handleError(error);
            });
    }

    invokeGetService(url: string): Observable<any> {
        this.commonService.showLoading();
        const invokeUrl = servicePath + url;

        return this.http.get(invokeUrl, { headers: contentHeaders })
            .map(res => this.extractData(res))
            .catch(error => this.handleError(error));
    }

    invokeResouces(url: string, data: any): Observable<any> {
        this.commonService.showLoading();
        const invokeUrl = servicePath + url;
        const jsondata = data == null ? null : JSON.stringify(data);
        return this.http.post(invokeUrl, jsondata, { headers: contentHeaders, responseType: 3 })
            .map(res => {
                this.commonService.closeLoading();
                return res.json();
            })
            .catch(error => this.handleError(error));
    }

    invokeGetResouces(url: string): Observable<any> {
        this.commonService.showLoading();
        return this.http.get(url)
            .map(res => {
                this.commonService.closeLoading();
                return res.json();
            })
            .catch(error => this.handleError(error));
    }

    invokeFormData(url: string, formData: FormData): Observable<any> {
        this.commonService.showLoading();
        const invokeUrl = servicePath + url;
        return this.http.post(invokeUrl, formData, { headers: formDataHeaders })
            .map(res => {
                this.commonService.closeLoading();
                return res.json();
            })
            .catch(error => this.handleError(error));
    }

    invokErrorTestService(url: string, data: any): Observable<any> {
        const invokeUrl = servicePath + url;
        const jsondata = data == null ? null : JSON.stringify(data);

        // console.log(jsondata);
        return this.http.post(invokeUrl, jsondata, { headers: contentHeaders })
            // .retryWhen(this.retry)
            // .timeout(this.timeout)
            .map(this.extractData)
            .catch(this.handleError);
    }

    private retry(errors) {
        if (this.retriedCount > this.retryCount) {
            return Observable.throw('Network error...');
        } else {

            this.retriedCount++;
            return errors.delay(500);
        }
    }

    private extractData(res: Response) {
        const resJson = res.json();
        // check if there is any server error ocurred.
        if (resJson.errorType) {
            console.log('resJson=' + JSON.stringify(res.json()));
            const serverError: ServerError = new ServerError();
            serverError.faultCode = resJson.faultCode;
            serverError.errorType = resJson.errorType;
            serverError.reason = resJson.reason;

            // error throw for each errorType
            let exception: Exception;
            switch (serverError.errorType) {
                case 'CosmosPrivilegePermissionFault':
                    exception = new PermissonException();
                    break;
                case 'ValidationFault':
                    exception = new ValidationException();
                    break;
                case 'ConcurrencyFault':
                    exception = new ConcurrencyException();
                    break;
                case 'UnhandledFault':
                    exception = new ServerTimeoutException();
                    break;
                case 'E01':
                    exception = new UnhandledException();
                    break;
                default:
                    exception = new UnhandledException();
                    break;

            }

            exception.errorCode = serverError.faultCode;
            exception.reason = serverError.reason;
            throw exception;

        }
        this.commonService.closeLoading();
        if (!resJson.isSuccess && resJson.errorMessage) {
            this.commonService.showError(resJson.errorMessage);
            const e = new Exception();
            e._body = JSON.stringify(resJson);
            throw e;
        } else {
            return resJson.data;
        }
    }

    /**
   * Handle HTTP error
   */
    private handleError(error: any) {
        // In a real world app, we might use a remote logging infrastructure
        // We'd also dig deeper into the error to get a better message
        // let errMsg = (error.message) ? error.message :
        //    error.status ? `${error.status} - ${error.statusText}` : 'Server error';
        // console.error('error=' + JSON.stringify(error)); // log to console instead
        this.commonService.closeLoading();
        $('.modal-backdrop').hide();
        const body = JSON.parse(error._body) as any;
        if (body.errorCode === '5000') {
            this.cacheService.setLocalCaches('originalUrl', window.location.href);
            window.location.href = './#/Login';
        } else if (body.errorCode === '6000') {
            window.location.href = './#/Main/nopermission';
        }
        return Observable.throw(error).toPromise();
    }
}
