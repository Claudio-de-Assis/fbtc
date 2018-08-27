import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

import { FileUploadRoute } from '../webapi-routes/file-upload.route';


@Injectable()
export class FileUploadService {

    constructor(
        private http: Http,
        private apiRoute: FileUploadRoute,
    ) { }

    upload(files, parameters) {
        const headers = new Headers();
        const options = new RequestOptions({ headers: headers });
        options.params = parameters;
        return  this.http.post(this.apiRoute.sendFile(), files, options)
                 .map(response => response.json())
                 .catch(error => Observable.throw(error));

    }

    getImages() {
        return this.http.get(this.apiRoute.getFile())
                   .map(response => response.json())
                   .catch(error => Observable.throw(error));
    }
}
