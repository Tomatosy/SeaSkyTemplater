import { Injectable } from '@angular/core';

@Injectable()
export class Exception {
    errorCode: string;
    reason: string;
    _body: string;
    // mappings: any;

    constructor() { }

}
